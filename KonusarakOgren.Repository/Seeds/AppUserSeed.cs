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
    public class AppUserSeed : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.HasData(new AppUser
            {
                Id = 1,
                UserName = "Batuhan",
                Email = "batuhan@gmail.com",
                PasswordHash = "asdasd123.",
                BasketId = 1,
            });
        }
    }
}
