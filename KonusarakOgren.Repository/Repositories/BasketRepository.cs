using KonusarakOgren.Core.Models;
using KonusarakOgren.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KonusarakOgren.Repository.Repositories
{
    public class BasketRepository : GenericRepository<Basket>, IBasketRepository
    {
        public BasketRepository(IdentityAppDbContext context) : base(context)
        {
        }

        public async Task AddProductToBasket(Basket basket)
        {
            await _context.AddAsync(basket);
        }
    }
}
