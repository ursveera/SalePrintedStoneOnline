using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int ColorId { get; set; }

        public string PrintedLabel { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
        public string OrderStatus { get; set; }
        public DateTime CreatedAt { get; set; }

        public User User { get; set; }
        public Product Product { get; set; }
        public ProductColor ProductColor { get; set; }
    }

}
