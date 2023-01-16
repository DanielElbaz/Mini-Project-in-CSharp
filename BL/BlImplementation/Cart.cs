﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Net.Mail;
using System.Reflection.Metadata;
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
        public BO.Cart AddProduct(BO.Cart cart, int productId)
        {
            
            DO.Product product;
            if (cart == null)
                throw new BO.incorrectDataException();


            try
            {
                product = dal.Product.GetByID(productId);
            }
            catch (DO.IncorrectDataException dex)
            {
                throw new BO.incorrectDataException("invalid product id", dex);
            }
            catch (DO.MissingIDException dex)
            {
                throw new BO.MissingIDException("id not found", dex);
            }

            var item = cart.Items?.FirstOrDefault(elem => elem.ProductID == productId);
            if (item == null) // new product item for cart
            {
                if (product.InStock >= 1)
                {
                    item = new()
                    {
                        ItemID = 0,
                        ProductID = product.ID,
                        ProductName = product.Name,
                        ProductPrice = product.Price,
                        TotalPrice = product.Price,
                        Amount = 1,
                    };
                    if (cart.Items == null)
                        cart.Items = new();

                    cart.Items.Add(item);
                    cart.TotalPrice += product.Price;
                }
            }

            else // product already exists
            {
                if (product.InStock >= item.Amount + 1)
                {
                    item.Amount++; // add to the amount of the item
                    item.TotalPrice += item.ProductPrice;
                    cart.TotalPrice += product.Price;
                    //product.InStock--; // reduce ammount
                    
                }
            }

            return cart;
        }
        
        /// <summary>
        /// update amount of product
        /// </summary>
        /// 
        public BO.Cart UpdateAmountOfProduct(BO.Cart cart, int productId, int newAmount)
        {
            DO.Product product;
            if (cart == null)
                throw new BO.incorrectDataException();


            try
            {
                product = dal.Product.GetByID(productId);
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
                    cart.Items!.Remove(item); // delete the order item
                }
                else

                {
                    item.Amount = newAmount;
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

        public bool IsValid(string email)
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
        public void ConfirmCart(BO.Cart cart)
        {
            int orderId =0; // id of order to add 
            DO.Product product;
            DO.Order order ;
            if (cart.Items == null)//no itmes in cart
                throw new BO.incorrectDataException("no items in the cart");
            
            if( cart.CustomerEmail!= null && cart.CustomerEmail != " "&& !IsValid(cart.CustomerEmail))
                throw new BO.incorrectDataException("invalid email address");
           
            if (cart.CustomerName == null || cart.CustomerAddress == null)
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
            order = new()
            {
                CustomerName = cart.CustomerName ?? throw new BO.incorrectDataException(" null customer name"),
                CustomerEmail = cart.CustomerEmail,
                CustomerAddress = cart.CustomerAddress ?? throw new BO.incorrectDataException(" null customer name"),
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

            //DO.OrderItem orderItem2 = new DO.OrderItem();
            foreach (BO.OrderItem orderItem1 in cart.Items)
            {                
                try
                {
                    dal!.OrderItem.Add(new DO.OrderItem
                    {
                        Amount = orderItem1.Amount,
                        Price = orderItem1.TotalPrice,
                        OrderID = orderId,
                        ProductID = orderItem1.ProductID
                    });

                    product = dal.Product.GetByID(orderItem1.ProductID);
                    product.InStock -= orderItem1.Amount;
                    dal.Product.Update(product.ID,product);


                    // dal.OrderItem.Add(orderItem2);
                }
                catch(DO.DuplicateIDException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
                      

        }
        
    }
}
