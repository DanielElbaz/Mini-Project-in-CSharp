﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;
using Dal;
//using DalApi;
using DO;
using BL;
using BO;
using System.Data.Common;
using DalApi;
using System.Security.Cryptography;
using System.Runtime.CompilerServices;

namespace BlImplementation
{
    internal class Product : BlApi.IProduct
    {
        //IDal dal = DalList.Instance;
        DalApi.IDal? dal = DalApi.Factory.Get();

        bool Check(int id, string? name, double price, int instock)
        {
            return (id > 100000 && id < 1000000)  && (name != null && name != "") && (price > 0) && (instock >= 0);
        }

        /// <summary>
        /// gets the list of products
        /// </summary>
        /// <returns></returns>
        /// 
        [MethodImpl(MethodImplOptions.Synchronized)]

        public IEnumerable<BO.ProductForList?> GetAll(Func<BO.Product, bool>? func)
        {
            if (func == null)
            {
                return from item in dal!.Product.GetAll()
                       let doProduct = (DO.Product)item
                       select new BO.ProductForList
                       
                           {
                               Category = (BO.Category)doProduct.Category!,
                               ProductID = (int)doProduct.ID!,
                               ProductName = doProduct.Name,
                               ProductPrice = (double)doProduct.Price!
                           };
                //return dal!.Product.GetAll().Select
                //      (doProduct => new BO.ProductForList()
                //      {
                //          Category = (BO.Category)doProduct?.Category!,
                //          ProductID = (int)doProduct?.ID!,
                //          ProductName = doProduct?.Name,
                //          ProductPrice = (double)doProduct?.Price!
                //      }); 
            }
            return dal!.Product.GetAll()
                .Where(doProduct => func(doProduct?.ConverToBO()!))
                .Select(doProduct => new BO.ProductForList()
                {
                    Category = (BO.Category)doProduct?.Category!,
                    ProductID = (int)doProduct?.ID!,
                    ProductName = doProduct?.Name,
                    ProductPrice = (double)doProduct?.Price!
                });
        }
        /// <summary>
        /// get all products for catalog
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        /// 
        [MethodImpl(MethodImplOptions.Synchronized)]

        public IEnumerable<BO.ProductItem?> GetAllCatalog(Func<BO.Product, bool>? func)
        {

            if (func == null)
            {
                // If func argument is null, return all products from DAL layer
                return dal!.Product.GetAll().Select
                      (doProduct => new BO.ProductItem() // Create a new BO.ProductItem object from DAL layer product object
                      {
                          Category = (BO.Category)doProduct?.Category!,
                          ProductID = (int)doProduct?.ID!,
                          ProductName = doProduct?.Name,
                          ProductPrice = (double)doProduct?.Price!,
                          IsAvailable = ((int)doProduct?.InStock! > 0) ? true : false,
                          AmountInCart = 0
                      }) ;
            }
            return dal!.Product.GetAll()
                .Where(doProduct => func(doProduct?.ConverToBO()!))
                .Select(doProduct => new BO.ProductItem()
                {
                    Category = (BO.Category)doProduct?.Category!,
                    ProductID = (int)doProduct?.ID!,
                    ProductName = doProduct?.Name,
                    ProductPrice = (double)doProduct?.Price!,
                    IsAvailable = ((int)doProduct?.InStock! > 0) ? true : false,
                    AmountInCart = 0
                });


        }
        /// <summary>
        /// get a product details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// 

        [MethodImpl(MethodImplOptions.Synchronized)]

        public BO.Product GetProduct(int id)
        {

            if (id <= 0)
                throw new BO.invalidInputException("Doesnt found product");
            DO.Product doProduct;

            try { doProduct = dal!.Product.GetByID(id); }
            catch (DO.MissingIDException ex)
            { throw new BO.MissingIDException(ex.Message); }
            BO.Product product = new BO.Product()
            {
                ID = id,
                Name = doProduct.Name,
                Price = doProduct.Price,
                Category = (BO.Category)doProduct.Category,
                InStock = doProduct.InStock
            };
            return product;


        }
        /// <summary>
        /// get product fo catalog
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cart"></param>
        /// <returns>product item   </returns>
        /// 

        [MethodImpl(MethodImplOptions.Synchronized)]

        public BO.ProductItem GetProductForCatalog(int id, BO.Cart cart)
        {
            DO.Product doProdcut = new();
            if (id <= 0)
                throw new BO.invalidInputException();
            if (cart == null)
                throw new BO.invalidInputException();

            try { doProdcut = dal!.Product.GetByID(id); }
            catch (DO.MissingIDException ex)
            { throw new BO.MissingIDException(ex.Message); }
            IEnumerable<BO.OrderItem>? orderItems = cart.Items;
            int amount = 0; // amount of the specified product in the customers cart
            BO.OrderItem? orderItem =null;

            // Search the product in the cart
            if (orderItems != null) // cart is not empty
            {
                orderItem = orderItems.FirstOrDefault(o => o.ProductID == id);
                if (orderItem != null)
                    amount = orderItem.Amount;
            }
                
            BO.ProductItem productItem = new()
            {
                ProductName = doProdcut.Name,
                ProductID = id,
                ProductPrice = doProdcut.Price,
                IsAvailable = (doProdcut.InStock >= 1) ? true : false,
                Category = (BO.Category)doProdcut.Category,
                AmountInCart = amount
            };
            return productItem;

        }
        /// <summary>
        /// add product for manager
        /// </summary>
        /// <param name="product"></param>
        /// 
        [MethodImpl(MethodImplOptions.Synchronized)]

        public void AddProduct(BO.Product p)
        {
            if (p == null)
                throw new BO.invalidInputException("Product doesn't found");
            if (!Check(p.ID, p.Name, p.Price, p.InStock))
                throw new BO.invalidInputException();
            DO.Product productToAdd = new DO.Product()
            {
                ID = p.ID,
                Name = p.Name,
                Price = p.Price,
                InStock = p.InStock,
                Category = (DO.Category)p.Category

            };

            try
            {
                 dal!.Product.Add(productToAdd);
            }
            catch (DO.DuplicateIDException e)
            {
                ///????????????????????????????
                throw new BO.DuplicateIDException("Item already exists ", e);
            }

        }
        /// <summary>
        /// update product for manager
        /// </summary>
        /// <param name="product"></param>
        /// 
        [MethodImpl(MethodImplOptions.Synchronized)]

        public void UpdateProduct(BO.Product? p)
        {
            if (p == null)
                throw new BO.invalidInputException();
            if (!Check(p.ID, p.Name, p.Price, p.InStock))
                throw new BO.invalidInputException("Error in ditels");

            DO.Product newproduct = new DO.Product()
            {
                ID = p.ID,
                Name = p.Name,
                Price = p.Price,
                Category = (DO.Category)p.Category,
                InStock = p.InStock
            };
            IEnumerable<DO.Product?> doProducts = dal!.Product.GetAll();

            //Checking if the product is found in the system.
            DO.Product? dp = null;
             dp = doProducts?.FirstOrDefault(pr => ((DO.Product)pr!).ID == p.ID);
            if(dp!=null)
                try
                {
                    dal.Product.Update(((DO.Product)dp).ID, newproduct);
                }
                catch (DO.MissingIDException)
                {

                    throw new BO.MissingIDException("product doesnt exist");
                }
            return;

           
        }
        /// <summary>
        /// delete product for manger
        /// </summary>
        /// <param name="id"></param>
        /// 

        [MethodImpl(MethodImplOptions.Synchronized)]

        public void DeleteProduct(int id)
        {           
            //Check if the product found in other orders.
            IEnumerable<DO.OrderItem?> orders = dal!.OrderItem.GetAll();            
            if(orders.FirstOrDefault(o =>((DO.OrderItem)o!).ProductID == id)!=null)
                throw new BO.DuplicateIDException("Product found in exsited order...");           

            try
            {
                DO.Product doProduct = dal.Product.GetByID(id);// find the product in datasource
                dal.Product.Delete(id); //delete
            }
            catch (DO.MissingIDException ex)
            {
                throw new BO.MissingIDException("product not found",ex); }
            return;            
            //throw new BO.MissingIDException("product not found");

        }

    }
}