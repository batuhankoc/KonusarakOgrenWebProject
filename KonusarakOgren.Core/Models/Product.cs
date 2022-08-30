using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KonusarakOgren.Core.Models
{
    public class Product : BaseEntity
    {
        public string Name { get; set; } = null!;
        public int Stock { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public string Color { get; set; }
        public int BrandId { get; set; }
        public Category Category { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public Brand Brand { get; set; }
        public ICollection<Basket> Baskets { get; set; }
    }
}
