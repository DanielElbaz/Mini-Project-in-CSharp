namespace Dal;
//using DO;

public class DalProduct
{
   
    public int Add(Product P)
    {
        //int clearIndex = 0;
        for (int i = 0; i < DataSource.Config.ProductFirstClear; i++)
        {
            if (P.ID == DataSource.P_arr[i].ID)
                throw new Exception("product already exist ");
        }    
        DataSource.P_arr[DataSource.Config.ProductFirstClear] = P;
        DataSource.Config.ProductFirstClear++;
        return P.ID;

        //return P.ID;
    }

    public void Delete(int id) // delete product by id
    {
        Boolean f = false;
        for (int i = 0; i < DataSource.Config.ProductFirstClear; i++)
        {
            if (id == DataSource.P_arr[i].ID)
            {
                f = true;
                for (int j = i; j < DataSource.Config.ProductFirstClear; j++)
                    DataSource.P_arr[j] = DataSource.P_arr[j + 1];
                DataSource.Config.ProductFirstClear--;
            }
        }
        if (!f)
            throw new Exception("product not found ");
    }

    public void update(int  id, Product newP) // update old with new
      {
        Boolean f = false;
        for (int i = 0; i < DataSource.Config.ProductFirstClear; i++)// check if old exist        
            if (DataSource.P_arr[i].ID == id)
            {
                f = true;
                DataSource.P_arr[i] = newP;
            }
        if (!f)
            throw new Exception("product not found");

    }

     public Product GetProduct(int id)

    {
        int i;
        Boolean f = false;
        for (i = 0; i < DataSource.Config.ProductFirstClear; i++)
            if (DataSource.P_arr[i].ID == id)
            { f = true; break; }
        if (!f)
            throw new Exception("Product doesnt exist");
           return DataSource.P_arr[i];
    }

    public Product[] getAllProducts() // read funct
    {

        Product[] products = new Product[DataSource.Config.ProductFirstClear + 1];
        // read funct
        {
            for (int i = 0; i < products.Length; i++)
                products[i] = DataSource.P_arr[i];
            return products;
        }

    }



}
