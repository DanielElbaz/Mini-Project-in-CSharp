using DalApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
    internal class DalXml:IDal
    {

        public static IDal Instance { get; } = new DalXml();
        private DalXml() { }

        public IOrder Order { get; } = new Order();
        public IProduct Product { get; } = new Product();
        public IOrderItem OrderItem { get; } = new OrderItem();
    }
}
