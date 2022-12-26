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
       
        public List< Tuple<DateTime?, string>>?  StatusDescription { get; set; }
        

    }
}