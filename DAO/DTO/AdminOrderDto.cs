using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.DTO
{
    public class AdminOrderDto
    {
        public int OrderId { get; set; }
        public string CustomerName { get; set; }
        public string ColorName { get; set; }
        public string PrintedLabel { get; set; }
        public decimal TotalPrice { get; set; }
        public string OrderStatus { get; set; }
        public DateTime OrderDate { get; set; }
    }

}
