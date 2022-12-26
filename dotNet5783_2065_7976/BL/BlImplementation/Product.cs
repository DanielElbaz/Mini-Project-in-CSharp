﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;
using Dal;
using DalApi;
using DO;
using BL;

namespace BlImplementation
{
    internal class Product : BlApi.IProduct
    {
        IDal dal = DalList.Instance;

        bool Check(int id, string? name, double price, int instock)
        {
            return (id > 0) && (name != null && name != "") && (price > 0) && (instock >= 0);
        }

        /// <summary>
        /// gets the list of products
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BO.ProductForList> GetAll()
        {


            IEnumerable<DO.Product> doProducts = dal.Product.GetAll();
            List<BO.ProductForList> productsForList = new List<BO.ProductForList>();
            foreach (DO.Product doProduct in doProducts)
            {
                productsForList.Add(new BO.ProductForList()
                {
                    Category = (BO.Category)doProduct.Category,
                    ProductID = doProduct.ID,
                    ProductName = doProduct.Name,
                    ProductPrice = doProduct.Price,
                });
            }
            return productsForList;

        }
        /// <summary>
        /// get a product details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public BO.Product GetProduct(int id)
        {

            if (id <= 0)
                throw new BO.invalidInputException("Dosennt found product");
            DO.Product doProduct;

            try {  doProduct = dal.Product.GetByID(id); }
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
        public BO.ProductItem GetProductForCatalog(int id, BO.Cart cart)
        {
            if (id <= 0)
                throw new BO.invalidInputException();
            if (cart == null)
                throw new BO.invalidInputException();

            DO.Product doProdcut = dal.Product.GetByID(id);
            IEnumerable<BO.OrderItem>? orderItems = cart.Items;
            int amount = 0; // amount of the specified product in the customers cart
            if (orderItems == null)
                throw new BO.invalidInputException();
            // Search the product in the cart
            foreach (BO.OrderItem orderItem in orderItems)
            {
                if (orderItem.ProductID == id)
                    amount = orderItem.Amount;
            }

            BO.ProductItem productItem = new BO.ProductItem()
            {
                ProductName = doProdcut.Name,
                ProductID = id,
                ProductPrice = doProdcut.Price,
                IsAvailable = true,
                Category = (BO.Category)doProdcut.Category,

                AmountInCart = amount
            };
            return productItem;

        }
        /// <summary>
        /// add product for manager
        /// </summary>
        /// <param name="product"></param>
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
                dal.Product.Add(productToAdd);
            }
            catch (DO.DuplicateIDExeption e)
            {

                ///????????????????????????????
                throw new BO.DuplicateIDException(e.Message);
            }

        }
        /// <summary>
        /// update product for manager
        /// </summary>
        /// <param name="product"></param>
        public void UpdateProduct(BO.Product p)
        {
            if (p == null)
                throw new BO.invalidInputException();
            if (!Check(p.ID , p.Name, p.Price, p.InStock))
                throw new BO.invalidInputException("Error in ditels");

            DO.Product newproduct = new DO.Product()
            {
                ID = p.ID,
                Name = p.Name,
                Price = p.Price,
                Category = (DO.Category)p.Category,
                InStock = p.InStock
            };
            IEnumerable<DO.Product> doProducts = dal.Product.GetAll();

            //Checking if the product is found in the system.
            foreach (DO.Product doProduct in doProducts)
                if (doProduct.ID == p.ID)
                {  //If found - try to do update.
                    try
                    {
                        dal.Product.Update(doProduct.ID, newproduct);
                    }
                    catch (DO.MissingIDException)
                    {

                        throw new BO.MissingIDException("product doesnt exist");
                    }
                    return;
                }
        }
        /// <summary>
        /// delete product for manger
        /// </summary>
        /// <param name="id"></param>
        public void DeleteProduct(int id)
        {
            Product product = new Product();
           // Order order = new Order();
           // DalOrderItem dalOrderItem = new DalOrderItem();
            //DalProduct dalProduct = new DalProduct();
            //Check if the product found in other orders.
            IEnumerable<DO.OrderItem> orders = dal.OrderItem.GetAll();
            foreach (DO.OrderItem o in orders)
                if (o.ProductID == id)
                {
                    throw new BO.DuplicateIDException("Product found in exsited order...");
                }


            IEnumerable<BO.ProductForList> products = product.GetAll();
            foreach (BO.ProductForList thisproduct in products)
            {
                if (thisproduct.ProductID == id)
                {
                    dal.Product.Delete(id);
                    return;
                }


            }
            throw new BO.MissingIDException("product not found");





        }
    }
}