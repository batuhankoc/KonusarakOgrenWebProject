using AutoMapper;
using KonusarakOgren.Core.DTOs;
using KonusarakOgren.Core.Models;
using KonusarakOgren.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Net;

namespace KonusarakOgren.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IService<Product> _service;
        private readonly IMapper _mapper;

        public ProductController(IService<Product> service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [Authorize(Roles = "Customer,SysAdmin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _service.GetByIdAsync(id);
            return Ok(CustomResponseContract.Success(product, HttpStatusCode.OK));
        }
        [Authorize(Roles = "Customer,SysAdmin")]
        [HttpGet]
        [Route("GetAllProduct")]
        public async Task<IActionResult> GetAll()
        {
            var product = await _service.GetAllAsync();
            return Ok(CustomResponseContract.Success(product, HttpStatusCode.OK));
        }

        [Authorize(Roles = "Admin,SysAdmin")]
        [HttpPost]
        public async Task<IActionResult> Add(ProductDto productDto)
        {
            var product = _mapper.Map<Product>(productDto);
            await _service.AddAsync(product);
            return Ok(CustomResponseContract.Success(null, HttpStatusCode.OK));
        }

        [Authorize(Roles = "Admin,SysAdmin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            var product = await _service.GetByIdAsync(id);
            await _service.RemoveAsync(product);
            return Ok(CustomResponseContract.Success(null, HttpStatusCode.OK));
        }

        [Authorize(Roles = "Admin,SysAdmin")]
        [HttpPut]
        public async Task<IActionResult> Update(ProductDto productDto)
        {
            var product = _mapper.Map<Product>(productDto);
            await _service.UpdateAsync(product);
            return Ok(CustomResponseContract.Success(null, HttpStatusCode.OK));
        }
    }
}
