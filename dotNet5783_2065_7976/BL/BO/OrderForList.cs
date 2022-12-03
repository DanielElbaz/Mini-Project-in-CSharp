using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;

namespace BO
{
    public class OrderForList
    {
        public int OrderID { get; set; }
        public string? CustomerName { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public int AmountOfItems { get; set; }
        public double TotalPrice { get; set; }
    }
}