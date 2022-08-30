using KonusarakOgren.Core.DTOs;
using KonusarakOgren.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KonusarakOgren.Core.Repositories
{
    public interface IBasketRepository : IGenericRepository<Basket>
    {
        public Task AddProductToBasket(Basket basket);
    }
}
