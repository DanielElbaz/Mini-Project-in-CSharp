using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using BlApi;
using Dal;
using DalApi;

namespace BlImplementation
{
    internal class Cart: ICart
    {
        private IDal dal = new DalList();

        /// <summary>
        /// add product to cart
        /// returns cart
        /// </summary>
        /// 
        public Cart AddProductInCart(Cart cart, int productId)
        { }
        /// <summary>
        /// update amount of product
        /// </summary>
        /// 
        public Cart UpdateAmountOfProduct(Cart cart, int productId, int newAmount)
        { }
        /// <summary>
        /// confirm cart for order
        /// </summary>
        public void ConfirmCart(Cart cart)
        { }





    }
}
