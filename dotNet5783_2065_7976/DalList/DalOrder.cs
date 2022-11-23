namespace Dal;

public class DalOrder
{

    public int Add(Order order)
    {
        foreach (Order order1 in DataSource.O_list)
            if (order1.ID == order.ID)
                throw new Exception("order already exists ");
        DataSource.O_list.Add(order);
        //for (int i = 0; i < DataSource.Config.OrderFirstClear; i++)
        //{
        //    if (order.ID == DataSource.O_arr[i].ID) // it exist
        //        throw new Exception("order already exist ");

        //}
        //DataSource.O_arr[DataSource.Config.OrderFirstClear] = order;
        //DataSource.Config.OrderFirstClear++;
        return order.ID;
    }

    public void Delete(int id) // delete product by id
    {
        Boolean flag = false;
        foreach (Order order in DataSource.O_list)
        {
            if (id == order.ID)
            {
                flag = true;
                DataSource.O_list.Remove(order);
            }

            if (!flag)
                throw new Exception("order not found ");
        } 
             
        //for (int i = 0; i < DataSource.Config.OrderFirstClear; i++)
        //{
        //    if (id == DataSource.O_arr[i].ID)
        //    {
        //        flag = true;
        //        for (int j = i; j < DataSource.Config.OrderFirstClear; j++)
        //            DataSource.O_arr[j] = DataSource.O_arr[j + 1];
        //        DataSource.Config.OrderFirstClear--;
        //    }
        //}
        
    }

    public void Update(int id, Order newOrder) // update old with new
    {
        Boolean flag = false;
        foreach (Order order in DataSource.O_list)
            if (id == order.ID)
            {
                flag = true;
                int index = DataSource.O_list.IndexOf(order);
                DataSource.O_list[index] = newOrder;    
                
            }
        if (!flag)
            throw new Exception("order not found");
        //for (int i = 0; i < DataSource.Config.OrderFirstClear; i++)// check if old exist        
        //    if (DataSource.O_arr[i].ID == id)
        //    {
        //        f = true;
        //        DataSource.O_arr[i] = newOrder;
        //    }


    }

    public Order getOneOrder(int id)
    {
        //int i;
        int index= -1;
        //Boolean flag = false;
        foreach (Order order in DataSource.O_list)
        
            if (id == order.ID)
            {
                //flag = true;

                index = DataSource.O_list.IndexOf(order);
                break;
            }
        if (index != -1)
            throw new Exception("Order doesnt exist");
        return DataSource.O_list[index];
        //for (i = 0; i < DataSource.Config.OrderFirstClear; i++)
        //    if (DataSource.O_arr[i].ID == id)
        //    { flag = true; break; }


    }
    public List<Order> getAllOrders()

    {
        
        return DataSource.O_list;
        // read funct
        //{
        //    for (int i = 0; i < orders.Length; i++)
        //        orders[i] = DataSource.O_arr[i];
        //    return orders;
        //}

    }


}