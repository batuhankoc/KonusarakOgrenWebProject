using KonusarakOgren.Core.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KonusarakOgren.Repository.Seeds
{
    public class BasketSeed : IEntityTypeConfiguration<Basket>
    {
        public void Configure(EntityTypeBuilder<Basket> builder)
        {
            builder.HasData(new Basket
            {
                Id = 1,
                CreatedDate = DateTime.Now,
                UserId = 1,
                ProductId = 1,
            });
        }
    }
}
