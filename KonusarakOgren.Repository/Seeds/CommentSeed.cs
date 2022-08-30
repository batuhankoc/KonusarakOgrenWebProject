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
    public class CommentSeed : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasData(new Comment
            {
                Id = 1,
                ProductId = 1,
                UserId = 1,
                Content = "deneme",
                CreatedDate = DateTime.Now
            });
        }
    }
}
