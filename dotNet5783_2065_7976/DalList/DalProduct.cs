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
            foreach (Product product1 in DataSource.P_list)
                if (product1.ID == P.ID)
                    throw new DuplicateID();
            // throw new Exception("Product already exists ");
            DataSource.P_list.Add(P);
            return P.ID;

        }

        public void Delete(int id) // delete product by id
        {
            Boolean flag = false;
            foreach (Product product in DataSource.P_list)
            {
                if (id == product.ID)
                {
                    flag = true;
                    DataSource.P_list.Remove(product);
                }
                if (!flag)
                    // throw new Exception("product not found ");
                    throw new MissingID();


            }
        }
        public void Update(int id, Product newP) // update old with new
        {
            Boolean flag = false;
            foreach (Product product in DataSource.P_list)
                if (id == product.ID)
                {
                    flag = true;
                    int index = DataSource.P_list.IndexOf(product);
                    DataSource.P_list[index] = newP;

                }
            if (!flag)
                throw new MissingID();
            // throw new Exception("product not found");



        }

        public Product GetByID(int id)

        {
            int index = -1;
            foreach (Product product in DataSource.P_list)

                if (id == product.ID)
                {
                    //flag = true;

                    index = DataSource.P_list.IndexOf(product);
                    break;
                }
            if (index != -1)
                throw new MissingID();
            // throw new Exception("Product doesn't exist");
            return DataSource.P_list[index];


        }

        public IEnumerable<Product> GetAll()
            {
            int count = 0, i = 0;
            foreach (var p in DataSource.P_list)
            {
                count++;
            }

            Product[] arr = new Product[count];

            foreach (var p in DataSource.P_list)
            {
                arr[i++] = p;
            }
            return arr;
        }



    }
}