using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{

    public class OrderTracking
    {
        public int OrderID { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? ShipDate { get; set; }
        public DateTime? DeleveryDate { get; set; }

    }
}