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
    public class BrandSeed : IEntityTypeConfiguration<Brand>
    {
        public void Configure(EntityTypeBuilder<Brand> builder)
        {
            builder.HasData(new Brand
            {
                Id = 1,
                CreatedDate = DateTime.Now,
                Name = "Adidas"
            });
        }
    }
}
