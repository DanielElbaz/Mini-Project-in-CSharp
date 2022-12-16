using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;
using Dal;
using DalApi;
using DO;

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

            DO.Product doprocut = dal.Product.GetByID(id);
            BO.Product product = new BO.Product()
            {
                ID = id,
                Name = doprocut.Name,
                Price = doprocut.Price,
                Category = (BO.Category)doprocut.Category,
                InStock = doprocut.InStock
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

            DO.Product doprocut = dal.Product.GetByID(id);
            IEnumerable<BO.OrderItem>? orders = cart.Items;
            int amount = 0;
            if (orders == null)
                throw new BO.invalidInputException();
            // Search the product in the cart
            foreach (BO.OrderItem O in orders)
            {
                if (O.ProductID == id)
                    amount = O.Amount;
            }

            BO.ProductItem product = new BO.ProductItem()
            {
                ProductName = doprocut.Name,
                ProductID = id,
                ProductPrice = doprocut.Price,
                IsAvailable = true,
                Category = (BO.Category)doprocut.Category,

                AmountInCart = amount
            };
            return product;

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
            catch (DuplicateIDExeption e)
            {

                ///????????????????????????????
                throw e;
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
                    catch (Exception)
                    {

                        throw;
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
                    throw new DuplicateIDExeption("Product found in exsited order...");
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