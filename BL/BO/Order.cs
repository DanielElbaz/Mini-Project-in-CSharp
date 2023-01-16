using BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BO
{
    public class Order
    {
        public int ID { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerEmail { get; set; }
        public string? CustomerAddress { get; set; }
        public OrderStatus OrderStatus { get; set; }

        public DateTime? OrderDate { get; set; }
        public DateTime? PaymentDate { get; set; }
        public DateTime? ShipDate { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public IEnumerable<OrderItem>? Items { get; set; }
        public double TotalPrice { get; set; }

        public override string ToString()
        {
           string str = "Order Date:" + OrderDate +'\n'+
                           "Payment Date:" + PaymentDate +'\n'+
                         "Ship Date:" + ShipDate + '\n' +
                         "Delivery Date:" + DeliveryDate + '\n'+
                          "Total Price:" + TotalPrice + '\n' +
                         "Items:" +'\n'; 
            if(Items!=null)
            foreach(OrderItem item in Items)
            {
                str += item.ProductName + " X" + item.Amount + '\n';
            }


            return str;
        }


    }
}