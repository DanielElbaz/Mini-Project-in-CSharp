﻿using DalApi;
using DO;
using static Dal.DataSource;
using System.Linq;

namespace Dal
{
    //using DO;

    public class DalProduct : IProduct
    {

        public int Add(Product P)
        {
            if(DataSource.ProductDataList.Find(p => ((Product)p!).ID == P.ID) != null)
                throw new DuplicateIDException();
            DataSource.ProductDataList.Add(P);
            return P.ID;
            

        }

        public void Delete(int id) // delete product by id
        {
            int count = DataSource.ProductDataList.RemoveAll(p => ((Product)p!).ID == id);
            if (count == 0)
                throw new MissingIDException("not found " + id);
        }


        public void Update(int id, Product newP) // update old with new
        {
            //Boolean flag = false;
            int count  = DataSource.ProductDataList.RemoveAll(p =>((Product)p!).ID==id);
                if(count==0)
                  throw new MissingIDException("not found " + id);
            DataSource.ProductDataList.Add(newP);

           

        }

        public Product GetByID(int id)

        {
            var item = DataSource.ProductDataList.FirstOrDefault(p => ((Product)p!).ID == id);
            if (item == null)
                throw new MissingIDException();
            return (Product)item;
            

        }

        

            public IEnumerable<Product?> GetAll(Func<Product?, bool>? filter = null)
            {


            //return (filter == null ? DataSource.ProductDataList.Select(item => item)
            //          : DataSource.ProductDataList.Where(filter!) ?? throw new MissingIDException("Missing product"))
            //?? throw new MissingIDException ("Missing data or product list"); 


            if (filter == null)
                return DataSource.ProductDataList;

            List<Product?> products = new();
            foreach (var p in DataSource.ProductDataList)
                if (filter(p))
                    products.Add(p);
            return products;


        }

        public Product GetBy(Func<Product?, bool> filter)
        {
            var item = DataSource.ProductDataList.FirstOrDefault(p => filter((p)));
            if(item == null)
                throw new invalidInputException("no items found");
            return (Product)item;
          
        }






    }
}