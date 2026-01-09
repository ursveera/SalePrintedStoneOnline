using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Models
{
    public class ProductColor
    {
        public int ColorId { get; set; }
        public int ProductId { get; set; }
        public string ColorName { get; set; }
        public decimal ExtraPrice { get; set; }
        public string? ImageUrl { get; set; }
    }

}
