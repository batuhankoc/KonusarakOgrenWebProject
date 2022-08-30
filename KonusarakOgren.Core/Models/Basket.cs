using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KonusarakOgren.Core.Models
{
    public class Basket : BaseEntity
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public ICollection<Product> Products { get; set; }
        [ForeignKey("UserId")]
        public AppUser AppUser { get; set; }
    }
}
