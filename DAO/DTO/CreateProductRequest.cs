using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.DTO
{
    public class CreateProductRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal BasePrice { get; set; }
        public string DefaultImageUrl { get; set; }
        public bool IsActive { get; set; }

        public List<CreateProductColorRequest> Colors { get; set; }
        public List<string> Images { get; set; }
    }

    public class CreateProductColorRequest
    {
        public string ColorName { get; set; }
        public decimal ExtraPrice { get; set; }
        public string ImageUrl { get; set; }
    }

}
