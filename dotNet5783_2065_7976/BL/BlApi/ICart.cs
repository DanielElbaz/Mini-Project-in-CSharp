using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;

namespace BlApi
{
    /// <summary>
    /// interface for cart
    /// </summary>
    public interface ICart
    {
        /// <summary>
        /// add product to cart
        /// returns cart
        /// </summary>
        /// 
        public Cart AddProduct(Cart cart, int productId);
        /// <summary>
        /// update amount of product
        /// </summary>
        /// 
        public Cart UpdateAmountOfProduct(Cart cart, int productId, int newAmount);
        /// <summary>
        /// confirm cart for order
        /// </summary>
        public void ConfirmCart(Cart cart);







    }
}
