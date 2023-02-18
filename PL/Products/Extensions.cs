using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.Products
{
    public static class Extensions
    {
        static BlApi.IBl? bl = BlApi.Factory.Get();
        public static int InStock (this ProductForList p, int id)
        {
            return bl!.Product.GetProduct(id).InStock;
        }
    }
}
