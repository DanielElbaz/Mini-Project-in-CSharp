
using BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class ProductItem
    {
        public int ProductID { get; set; }
        public string? ProductName { get; set; }
        public double ProductPrice { get; set; }
        public Category Category { get; set; }
        public Boolean IsAvailable { get; set; }
        public int AmountInCart { get; set; }
        public override string ToString()
        {
            return this.ToStringProperty();
        }
    }
}
