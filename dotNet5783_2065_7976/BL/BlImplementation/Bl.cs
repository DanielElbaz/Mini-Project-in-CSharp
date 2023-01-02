using BlApi;
using Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace BlImplementation
{


    sealed public class Bl : IBl

    {
        public IOrder Order { get; } = new BlImplementation.Order();
        public IProduct Product { get; } = new BlImplementation.Product();
        public ICart Cart { get; } = new BlImplementation.Cart();

        
    }
}
