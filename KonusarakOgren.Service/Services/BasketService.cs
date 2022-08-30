using AutoMapper;
using KonusarakOgren.Core.DTOs;
using KonusarakOgren.Core.Models;
using KonusarakOgren.Core.Repositories;
using KonusarakOgren.Core.Services;
using KonusarakOgren.Service.Exceptions;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KonusarakOgren.Service.Services
{
    public class BasketService : Service<Basket>, IBasketService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;
        public BasketService(IGenericRepository<Basket> repository, IBasketRepository basketRepository, IMapper mapper, UserManager<AppUser> userManager, IProductRepository productRepository) : base(repository)
        {
            _basketRepository = basketRepository;
            _mapper = mapper;
            _userManager = userManager;
            _productRepository = productRepository;
        }

        public async Task AddProductToBasket(Basket basket)
        {
            var user = await _userManager.FindByIdAsync(basket.UserId.ToString());
            var productDb = await _productRepository.Where(x => x.Id == basket.ProductId);
            var product = productDb.FirstOrDefault();
            if (product.Stock > 0)
            {
                await _basketRepository.AddAsync(basket);
                product.Stock -= 1;
                _productRepository.Update(product);
                user.BasketId = basket.Id;
                await _userManager.UpdateAsync(user);
            }
            throw new ClientSideException("Stock maalesef kalmamıştır");
        }
    }
}
