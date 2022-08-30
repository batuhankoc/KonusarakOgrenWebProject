using AutoMapper;
using KonusarakOgren.Core.DTOs;
using KonusarakOgren.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KonusarakOgren.Service.Mapping
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<Comment, AddCommentDto>().ReverseMap();
            CreateMap<Basket, BasketDto>().ReverseMap();
            CreateMap<Brand, BrandDto>().ReverseMap();
            CreateMap<Product, ProductDto>().ReverseMap();
        }
    }
}
