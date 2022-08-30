using KonusarakOgren.Core.DTOs;
using KonusarakOgren.Core.Models;
using KonusarakOgren.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace KonusarakOgren.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IConfiguration _configuration;

        public UserController(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> Signin(RegisterDto appUser)
        {
            var userExist = await _userManager.FindByNameAsync(appUser.UserName);
            if (userExist != null)
            {
                return BadRequest(CustomResponseContract.Fail("Bu kullanıcı ismi daha önce alınmıştır", HttpStatusCode.BadRequest));
            }

            AppUser user = new()
            {
                Email = appUser.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = appUser.UserName
            };
            var result = await _userManager.CreateAsync(user, appUser.Password);
            if (!result.Succeeded)
            {
                return BadRequest(CustomResponseContract.Fail("User creation failed! Please check user details and try again.", HttpStatusCode.InternalServerError));
            }
            await _roleManager.CreateAsync(new AppRole(appUser.Role));
            await _userManager.AddToRoleAsync(user, appUser.Role);
            return Ok(CustomResponseContract.Success(null, HttpStatusCode.OK));
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login( LoginDto model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };
                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }
                var token = GetToken(authClaims);
                return Ok(CustomResponseContract.Success(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                }, HttpStatusCode.OK));
            }
            return Unauthorized(CustomResponseContract.Fail("Kullanıcı adınız veya parolanız yanlış!", HttpStatusCode.OK));
        }
        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }
    }
}
