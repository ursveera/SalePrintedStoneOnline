using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.DTO
{
    public class ProductResponse
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal BasePrice { get; set; }
        public string DefaultImageUrl { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }

        public List<ProductColorResponse> Colors { get; set; } = new();
        public List<string> Images { get; set; } = new();
    }
    public class ProductColorResponse
    {
        public int ColorId { get; set; }
        public int ProductId { get; set; }
        public string ColorName { get; set; }
        public decimal ExtraPrice { get; set; }
        public string ImageUrl { get; set; }
    }

}
