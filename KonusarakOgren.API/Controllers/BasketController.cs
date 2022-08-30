using AutoMapper;
using KonusarakOgren.Core.DTOs;
using KonusarakOgren.Core.Models;
using KonusarakOgren.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Net;
using System.Security.Claims;

namespace KonusarakOgren.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketService _service;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public BasketController(IBasketService service, UserManager<AppUser> userManager, IMapper mapper)
        {
            _service = service;
            _userManager = userManager;
            _mapper = mapper;
        }
        [Authorize(Roles = "Customer,SysAdmin")]
        [HttpPost]
        public async Task<IActionResult> AddProductToBasket(BasketDto basketDto)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var user = await _userManager.FindByNameAsync(claimsIdentity.Name);
            var basket = _mapper.Map<Basket>(basketDto);
            basket.UserId = user.Id;
            await _service.AddProductToBasket(basket);
            return Ok(CustomResponseContract.Success(null, HttpStatusCode.OK));
        }
    }
}
