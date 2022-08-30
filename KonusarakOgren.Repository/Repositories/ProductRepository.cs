using KonusarakOgren.Core.Models;
using KonusarakOgren.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KonusarakOgren.Repository.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(IdentityAppDbContext context) : base(context)
        {
        }
    }
}
