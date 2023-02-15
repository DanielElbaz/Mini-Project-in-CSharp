using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Net.Mail;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using BL;
using BlApi;
using BO;
//using Dal;
using DalApi;
using DO;

namespace BlImplementation
{
    internal class Cart: ICart
    {
        //private IDal dal = DalList.Instance;
        DalApi.IDal? dal = DalApi.Factory.Get();

        /// <summary>
        /// add product to cart
        /// returns cart
        /// </summary>
        /// 

        [MethodImpl(MethodImplOptions.Synchronized)]
        public BO.Cart AddProduct(BO.Cart cart, int productId) // This function adds a product to a cart. 

        {
            DO.Product product;
            // Check if the cart is null. If yes, throw an exception of type BO.incorrectDataException
            if (cart == null)
                throw new BO.incorrectDataException();
            try // Try to retrieve the product from the DAL (Data Access Layer) based on the product ID.
            {
                product = dal!.Product.GetByID(productId);
            }
            // If there is an incorrect data exception in the DAL, re-throw it as BO.incorrectDataException
            catch (DO.IncorrectDataException dex)
            {
                throw new BO.incorrectDataException("Invalid product ID", dex);
            }
            // If the product ID is not found in the DAL, re-throw the exception as BO.MissingIDException
            catch (DO.MissingIDException dex)
            {
                throw new BO.MissingIDException("ID not found", dex);
            }

            // Check if the product is already in the cart. If yes, update the amount and total price.
            var item = cart.Items?.FirstOrDefault(elem => elem.ProductID == productId);
            if (item == null) // new product item for cart
            {
                if (product.InStock >= 1)
                {
                    // If the product is in stock, add it to the cart.
                    item = new()
                    {
                        ItemID = 0,
                        ProductID = product.ID,
                        ProductName = product.Name,
                        ProductPrice = product.Price,
                        TotalPrice = product.Price,
                        Amount = 1,
                    };
                    // Check if the cart's items list is null. If yes, create a new list.
                    if (cart.Items == null)
                        cart.Items = new();

                    // Add the product item to the cart and update the total price.
                    cart.Items.Add(item);
                    cart.TotalPrice += product.Price;
                }
            }

            else // if the product already exists
            {
                if (product.InStock >= item.Amount + 1)
                {
                    item.Amount++; // add to the amount of the item
                    item.TotalPrice += item.ProductPrice;
                    cart.TotalPrice += product.Price;
                                        
                }
            }

            return cart;
        }

        /// <summary>
        /// update amount of product
        /// </summary>
        /// 

        [MethodImpl(MethodImplOptions.Synchronized)]
        public BO.Cart UpdateAmountOfProduct(BO.Cart cart, int productId, int newAmount)
        {
            DO.Product product;
            if (cart == null)
                throw new BO.incorrectDataException();
           
            try
            {
                product = dal!.Product.GetByID(productId);
            }
            catch (DO.MissingIDException dex)
            {
                throw new BO.MissingIDException("invalid product id", dex);
            }
            if (newAmount < 0)
                throw new BO.invalidInputException("wrong amount");

            var item = cart.Items?.FirstOrDefault(elem => elem.ProductID == productId);

            if (item == null) // new product item for cart
                throw new BO.invalidInputException("item doesnt exist in the cart");
            int difference = newAmount - item.Amount;

            if (product.InStock >= difference) // there is in stock
            {

                if (newAmount == 0)
                {
                    cart.TotalPrice -= item!.TotalPrice; // reduce the total price of item from the cart
                    item.TotalPrice = 0; // reset the total price of the item
                    item.Amount = 0;
                    cart.Items!.Remove(item); // delete the order item
                }
                else // new amount >0

                {
                    item.Amount = newAmount;
                    item.TotalPrice += difference * product.Price; // update total price
                    cart.TotalPrice += difference * product.Price;
                    //product.InStock += difference;
                }
            }
            else // not enough in stock
            {
                throw new BO.invalidInputException("not enough in stock");
            }                
                
                            
            return cart; 
        }
        /// <summary>
        /// confirm cart for order
        /// </summary>

        public bool IsValid(string email) //function that checks if the email is valid
        {
            var valid = true;

            try
            {
                var emailAddress = new MailAddress(email);
            }
            catch
            {
                valid = false;
            }

            return valid;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]

        public void ConfirmCart(BO.Cart cart)
        {
            int orderId =0; // id of order to add 
            DO.Product product;
            DO.Order order ;
            // Check validity of datas
            if (cart.Items == null || cart.TotalPrice==0)//No items in cart
                throw new BO.incorrectDataException("No items in the cart");
            
            if( cart.CustomerEmail!= null && cart.CustomerEmail != " "&& !IsValid(cart.CustomerEmail))
                throw new BO.incorrectDataException("invalid email address");
           
            if (cart.CustomerName == null || cart.CustomerAddress == " " || cart.CustomerAddress == null)
                throw new BO.incorrectDataException("customer name or address null ");
           

            foreach (BO.OrderItem orderItem in cart.Items)
            {
                try
                {
                    product = dal!.Product.GetByID(orderItem.ProductID);
                }
                catch (DO.MissingIDException ex)
                {
                    throw new BO.MissingIDException( ex.Message+ " id " + orderItem.ProductID ) ;
                } // get the product in the orderItem list
                if (orderItem.Amount <= 0) // negative amount
                    throw new BO.incorrectDataException("invalid amount in order item  " + orderItem.ProductName);
                if (product.InStock < orderItem.Amount)// not enough in stock
                    throw new BO.incorrectDataException("not enough in stock for Product " + product.Name);

             }
            order = new() // Initializing all the order
            {
                CustomerName = cart.CustomerName ?? throw new BO.incorrectDataException(" null customer name"),
                CustomerEmail = cart.CustomerEmail,
                CustomerAddress = cart.CustomerAddress ?? throw new BO.incorrectDataException(" null customer address"),
                OrderDate = DateTime.Now,
                ShipDate = null,
                DeliveryDate = null

            };             
            try
            {
                orderId = dal!.Order.Add(order);
            }

            catch(DO.DuplicateIDException ex)
            {
                Console.WriteLine(ex.Message);
            }

            foreach (BO.OrderItem orderItem1 in cart.Items)
            {                
                try
                {
                    dal!.OrderItem.Add(new DO.OrderItem
                    {
                        Amount = orderItem1.Amount,
                        Price = orderItem1.ProductPrice,
                        OrderID = orderId,
                        ProductID = orderItem1.ProductID
                    });

                    product = dal.Product.GetByID(orderItem1.ProductID);
                    product.InStock -= orderItem1.Amount;
                    dal.Product.Update(product.ID,product);
                   
                }
                catch(DO.DuplicateIDException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
                      

        }
        
    }
}
