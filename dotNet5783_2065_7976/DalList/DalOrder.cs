namespace Dal;

public class DalOrder
{

    public int Add(Order order)
    {
        for (int i = 0; i < DataSource.Config.OrderFirstClear; i++)
        {
            if (order.ID == DataSource.O_arr[i].ID) // it exist
                throw new Exception("order already exist ");

        }
        DataSource.O_arr[DataSource.Config.OrderFirstClear] = order;
        DataSource.Config.OrderFirstClear++;
        return order.ID;
    }

    public void Delete(int id) // delete product by id
    {
        Boolean f = false;
        for (int i = 0; i < DataSource.Config.OrderFirstClear; i++)
        {
            if (id == DataSource.O_arr[i].ID)
            {
                f = true;
                for (int j = i; j < DataSource.Config.OrderFirstClear; j++)
                    DataSource.O_arr[j] = DataSource.O_arr[j + 1];
                DataSource.Config.OrderFirstClear--;
            }
        }
        if (!f)
            throw new Exception("order not found ");
    }

    public void update(int id, Order newOrder) // update old with new
    {
        Boolean f = false;
        for (int i = 0; i < DataSource.Config.OrderFirstClear; i++)// check if old exist        
            if (DataSource.O_arr[i].ID == id)
            { f = true;
                DataSource.O_arr[i] = newOrder;
            }
        if (!f)
            throw new Exception("order not found");

    }

    public Order getOneOrder(int id)
    {
        int i;
        Boolean f = false;
        for (i = 0; i < DataSource.Config.OrderFirstClear; i++)
            if (DataSource.O_arr[i].ID == id)
                 { f = true; break; }
        if (!f)
            throw new Exception("Order doesnt exist");
        return DataSource.O_arr[i];
    }
    public Order[] getallOrders()

    {
        Order[] orders = new Order[DataSource.Config.OrderFirstClear+1];
        // read funct
        {
            for (int i = 0; i < orders.Length; i++)
                orders[i] = DataSource.O_arr[i];
            return orders;
        }

    }


}