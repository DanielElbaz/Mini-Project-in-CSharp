using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BL;

namespace BO
{

    public class ProductForList
    {
        public int ProductID { get; set; }
        public string? ProductName { get; set; }
        public double ProductPrice { get; set; }
        public Category Category { get; set; }
        public override string ToString()
        {
            return this.ToStringProperty();
        }   

    }
}