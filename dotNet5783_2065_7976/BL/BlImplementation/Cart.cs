using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using BlApi;
using Dal;
using DalApi;
using DO;

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
        public BO.Cart AddProduct(BO.Cart cart, int productId)
        {
            bool found = false;
            DO.Product product = dal.Product.GetByID(productId);
            IEnumerable<BO.OrderItem> items = cart.Items;
            foreach (BO.OrderItem orderItem in items)
            {
                if (productId == orderItem.ProductID)
                {
                    
                    if (product.InStock >= 1)
                    
                        orderItem.Amount++;
                        return cart;     
                                   


                    
                }
            }
            
            //if (product.InStock >= 1)// if the product not in the cart
            //    BO.OrderItem orderItem = new BO.OrderItem {
            //     ProductID = productId,
            //     ProductName
            //    }


                if (found)
            {

            }

            return null;
        }
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
