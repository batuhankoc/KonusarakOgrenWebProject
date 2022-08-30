using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KonusarakOgren.Core.DTOs
{
    public class AddCommentDto
    {
        public int ProductId { get; set; }
        public string Content { get; set; }
    }
}
