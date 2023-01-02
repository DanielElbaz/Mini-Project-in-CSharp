using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public static class Tools
    {
        public static string ToStringProperty<T>(this T t)
        {
            string str = "";
            foreach (PropertyInfo item in t.GetType().GetProperties())
                str += "\n" + item.Name + ": " + item.GetValue(t, null);
            return str;
        }

        public static BO.Product ConverToBO(this DO.Product product)
        {
            return new BO.Product
            {
                ID = product.ID,
                Name = product.Name,
                Price = product.Price,
                Category = (BO.Category)product.Category,
                InStock = product.InStock
                
            };
        }
    }
}

