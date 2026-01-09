using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.DTO
{
    public class AddToCartRequest
    {
        public string SessionId { get; set; }   // guest session
        public int ProductId { get; set; }      // selected product
        public int ColorId { get; set; }        // selected color
        public int Quantity { get; set; }       // + / -
        public string? PrintedLabel { get; set; } // custom text
    }
}
