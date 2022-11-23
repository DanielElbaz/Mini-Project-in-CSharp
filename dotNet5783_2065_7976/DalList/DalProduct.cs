using DalApi;

namespace Dal;
//using DO;

public class DalProduct
{
   
    public int Add(Product P)
    {
        foreach (Product product1 in DataSource.P_list)
            if (product1.ID == P.ID)
                throw new Exception("Product already exists ");
        DataSource.P_list.Add(P);
        return P.ID;

        //int clearIndex = 0;
        //for (int i = 0; i < DataSource.Config.ProductFirstClear; i++)
        //{
        //    if (P.ID == DataSource.P_arr[i].ID)
        //        throw new Exception("product already exist ");
        //}    
        //DataSource.P_arr[DataSource.Config.ProductFirstClear] = P;
        //DataSource.Config.ProductFirstClear++;


        //return P.ID;
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
                throw new Exception("product not found ");
            //for (int i = 0; i < DataSource.Config.ProductFirstClear; i++)
            //{
            //    if (id == DataSource.P_arr[i].ID)
            //    {
            //        f = true;
            //        for (int j = i; j < DataSource.Config.ProductFirstClear; j++)
            //            DataSource.P_arr[j] = DataSource.P_arr[j + 1];
            //        DataSource.Config.ProductFirstClear--;
            //    }
            //}

        }
    }
    public void Update(int  id, Product newP) // update old with new
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
            throw new Exception("product not found");
        //for (int i = 0; i < DataSource.Config.ProductFirstClear; i++)// check if old exist        
        //    if (DataSource.P_arr[i].ID == id)
        //    {
        //        f = true;
        //        DataSource.P_arr[i] = newP;
        //    }


    }

     public Product GetProduct(int id)

    {
        int index=-1;
        foreach (Product product in DataSource.P_list)

            if (id == product.ID)
            {
                //flag = true;

                index = DataSource.P_list.IndexOf(product);
                break;
            }
        if (index != -1)
            throw new Exception("Product doesn't exist");
        return DataSource.P_list[index];
        //Boolean f = false;
        //for (i = 0; i < DataSource.Config.ProductFirstClear; i++)
        //    if (DataSource.P_arr[i].ID == id)
        //    { f = true; break; }

    }

    public List<Product> getAllProducts() // read funct
    {
        return DataSource.P_list;
        // read funct
        //{
        //    for (int i = 0; i < products.Length; i++)
        //        products[i] = DataSource.P_arr[i];
        //    return products;
        //}

    }



}
