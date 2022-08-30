using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KonusarakOgren.Core.Models
{
    public class AppUser : IdentityUser<int>
    {
        [ForeignKey("BasketId")]
        public int BasketId { get; set; }
        public Basket Basket { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}
