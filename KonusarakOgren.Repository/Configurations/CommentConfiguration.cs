using KonusarakOgren.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KonusarakOgren.Repository.Configurations
{
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.Content).IsRequired().HasMaxLength(1000);
            builder.ToTable("Comments");
            builder.HasOne(x => x.Product).WithMany(x => x.Comments).HasForeignKey(x => x.ProductId);
            builder.HasOne(x => x.AppUser).WithMany(x => x.Comments).HasForeignKey(x => x.UserId);

        }
    }
}
