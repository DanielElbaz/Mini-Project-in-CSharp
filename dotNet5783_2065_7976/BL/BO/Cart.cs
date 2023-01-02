using BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Cart
    {
        public string? CustomerName { get; set; }
        public string? CustomerAddress { get; set; }
        public string? CustomerEmail { get; set; }
        public List<OrderItem>? Items { get; set; }
        public double TotalPrice { get; set; }

        public override string ToString()
        {

            string str = "Customer Name: " + CustomerName + '\n' +
           "Customer Email: " + CustomerEmail + '\n' +
           "Customer address: " + CustomerAddress + '\n' +
               "Total price: " + TotalPrice + '\n' ;
            if(Items!= null )
            foreach (OrderItem item in Items)
            {
                str += item.ProductName + " X" + item.Amount + '\n';
            }
            return str ; 
        }

    }
}