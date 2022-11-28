using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;
using Dal;
//using BO;
//using Dal;
using DalApi;

namespace BlImplementation
{
    internal class Product: BlApi.IProduct
    {
       

        /// <summary>
        /// gets the list of products
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BO.ProductForList> GetListedProducts()
        {
            private DalApi.IDal dal = new DalList();
            return ;
        }
        /// <summary>
        /// get a product details
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Product GetProduct(int id)
        { }
        /// <summary>
        /// get product fo catalog
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cart"></param>
        /// <returns>product item   </returns>
        public ProductItem GetProductForCatalog(int id, Cart cart)
        { }
        /// <summary>
        /// add product for manager
        /// </summary>
        /// <param name="product"></param>
        public void AddProduct(Product product)
        { }
        /// <summary>
        /// update product for manager
        /// </summary>
        /// <param name="product"></param>
        public void UpdateProduct(Product product)
        { }
        /// <summary>
        /// delete product for manger
        /// </summary>
        /// <param name="id"></param>
        public void DeleteProduct(int id) 
        { }

    }
}
