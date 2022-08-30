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
    public class BrandController : ControllerBase
    {
        private readonly IService<Brand> _service;
        private readonly IMapper _mapper;

        public BrandController(IService<Brand> service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [Authorize(Roles = "Admin,SysAdmin")]
        [HttpPost]
        [Route("AddBrand")]
        public async Task<IActionResult> AddBrand(BrandDto brandDto)
        {
            var isBrandNull = await _service.AnyAsnyc(x => x.Name == brandDto.Name);
            if (!isBrandNull)
            {
                var brand = _mapper.Map<Brand>(brandDto);
                await _service.AddAsync(brand);
                return Ok(CustomResponseContract.Success(null, HttpStatusCode.OK));
            }
            return BadRequest(CustomResponseContract.Fail("Böyle bir marka mevcut!",HttpStatusCode.BadRequest));
        }

        [Authorize(Roles = "Admin,SysAdmin")]
        [HttpPost]
        [Route("DeleteBrand")]
        public async Task<IActionResult> DeleteBrand(BrandDto brandDto)
        {
            var brand = await _service.Where(x => x.Name == brandDto.Name);
            await _service.RemoveAsync(brand.FirstOrDefault());
            return Ok(CustomResponseContract.Success(null, HttpStatusCode.OK));
        }
    }
}
