using BL;
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

        public override string ToString()
        {
            string str ="order id:"+OrderID + '\n'+
                               "order status:" + OrderStatus + '\n'+
                                "order description:"+  '\n';
            foreach(Tuple<DateTime?, string> t in StatusDescription)
            {
               str += t.Item2 + '\n';
            }
            return str;
          //  return this.ToStringProperty();
        }


    }
}