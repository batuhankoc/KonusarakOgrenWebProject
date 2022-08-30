using AutoMapper;
using KonusarakOgren.Core.DTOs;
using KonusarakOgren.Core.Models;
using KonusarakOgren.Core.Services;
using KonusarakOgren.Service.Services;
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
    public class CommentController : ControllerBase
    {
        private readonly IService<Comment> _service;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public CommentController(IService<Comment> service, IMapper mapper, UserManager<AppUser> userManager)
        {
            _service = service;
            _mapper = mapper;
            _userManager = userManager;
        }

        [Authorize(Roles = "Customer,SysAdmin")]
        [HttpPost]
        public async Task<IActionResult> Add(AddCommentDto commentDto)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var user = await _userManager.FindByNameAsync(claimsIdentity.Name); 
            var comment = _mapper.Map<Comment>(commentDto);
            comment.UserId = user.Id;
            await _service.AddAsync(comment);
            return Ok(CustomResponseContract.Success(null, HttpStatusCode.OK));
        }
    }
}
