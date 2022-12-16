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
        public IOrder Order => new Order();
        public IProduct Product => new Product();
        public ICart Cart => new Cart();

        
    }
}
