namespace Dal;
//using DO;

public class DalProduct
{
    public int Add(Product P)
    {
        for (int i = 0; i < DataSource.Config.ProductFirstClear; i++)
        {
            if (P.ID == DataSource.P_arr[i].ID)
                return P.ID;
        }
        DataSource.P_arr[DataSource.Config.ProductFirstClear] = P;
        DataSource.Config.ProductFirstClear++;
        return P.ID;

        //return P.ID;
    }

    public void Delete(int id) // delete product by id
    {
        for (int i = 0; i < DataSource.Config.ProductFirstClear; i++)
        {
            if (id == DataSource.P_arr[i].ID)
                DataSource.P_arr[i].ID = 0;
            //  Product temp = new Product;
            //  Product  temp = DataSource.P_arr[i];
            // for (int j = i; j < DataSource.Config.ProductFirstClear; j++)
            //  { }
            //    DataSource.P_arr[i] = ;
        }
    }

    public void update(Product oldP, Product newP)
      {
        for (int i = 0; i < DataSource.Config.ProductFirstClear; i++)
        
            if (DataSource.P_arr[i].ID == oldP.ID)
                DataSource.P_arr[i] = newP;

    }

    public Product[] read()
    {
        return DataSource.P_arr;
    }



}
