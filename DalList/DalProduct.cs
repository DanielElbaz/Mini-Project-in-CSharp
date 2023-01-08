using DalApi;
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
            foreach (Product product1 in DataSource.ProductDataList)
                if (product1.ID == P.ID)
                    throw new DuplicateIDException();
            // throw new Exception("Product already exists ");
            DataSource.ProductDataList.Add(P);
            return P.ID;

        }

        public void Delete(int id) // delete product by id
        {
            Boolean flag = false;
            foreach (Product product in DataSource.ProductDataList)
            {
                if (id == product.ID)
                {
                    flag = true;
                    DataSource.ProductDataList.Remove(product);
                    break;
                }           


            }
            if (!flag)
                // throw new Exception("product not found ");
                throw new MissingIDException();
        }
        public void Update(int id, Product newP) // update old with new
        {
            Boolean flag = false;
            foreach (Product product in DataSource.ProductDataList)
                if (id == product.ID)
                {
                    flag = true;
                    int index = DataSource.ProductDataList.IndexOf(product);
                    DataSource.ProductDataList[index] = newP;
                    break;

                }
            if (!flag)
                throw new MissingIDException();
            // throw new Exception("product not found");



        }

        public Product GetByID(int id)

        {
            var item = DataSource.ProductDataList.FirstOrDefault(p => ((Product)p!).ID == id);
            if (item == null)
                throw new MissingIDException();
            return (Product)item;
            //int index = -1;
            //foreach (Product product in DataSource.ProductDataList)

            //    if (id == product.ID)
            //    {
            //        //flag = true;

            //        index = DataSource.ProductDataList.IndexOf(product);
            //        break;
            //    }
            //if (index == -1)
            //    throw new MissingIDException();

            //return DataSource.ProductDataList[index];


        }

        

            public IEnumerable<Product?> GetAll(Func<Product?, bool>? filter = null)
            {
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
            //bool flag = false;
            //if (filter == null)
            //    throw new invalidInputException();
            //Order order = new();
            //foreach (var o in DataSource.OrderDataList)
            //    if (filter(o))
            //    {
            //        flag = true;
            //        order = (Order)o!;
            //    }
            //if (!flag)
            //    throw new invalidInputException("no items found");
            //return order;

        }






    }
}