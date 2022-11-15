namespace Dal;

public class DalOrderItem
{
    /// <summary>
    /// add new order item
    /// </summary>
    /// <param name="oi"></param>
    /// <returns>id <returns>
    public int Add( OrderItem oi)
    {
        
        for (int i = 0; i < DataSource.Config.OrderItemFirstClear; i++)
        {
            if (oi.ID == DataSource.OI_arr[i].ID) // it exist
                throw new Exception("order item already exist ");

        }
        DataSource.OI_arr[DataSource.Config.OrderItemFirstClear] = oi;
        DataSource.Config.ProductFirstClear++;
        return oi.ID;
    }
    public void Delete(int id)

    {
        for (int i = 0; i < DataSource.Config.OrderItemFirstClear; i++)
        {
            if (id == DataSource.OI_arr[i].ID)
            {
                for (int j = i; j < DataSource.Config.OrderItemFirstClear; j++)
                    DataSource.OI_arr[j] = DataSource.OI_arr[j + 1];
                DataSource.Config.OrderItemFirstClear--;
            }

        }
    }
    public void Update(OrderItem oldOI, OrderItem newOI)
    {
        for (int i = 0; i < DataSource.Config.ProductFirstClear; i++)// check if old exist        
            if (DataSource.OI_arr[i].ID == oldOI.ID)
                DataSource.OI_arr[i] = newOI;
    }

    public OrderItem getOneOrderItem(int id )
    {
        int i;
        Boolean f = false;
        for (i = 0; i < DataSource.Config.OrderItemFirstClear; i++)
            if (DataSource.OI_arr[i].ID == id)
            { f = true; break; }
        if (!f)
            throw new Exception("Order Item doesnt exist");
         return DataSource.OI_arr[i];
    }
    public OrderItem[] getallOrderItems()

    {
        OrderItem[]  orderItems = new OrderItem[DataSource.Config.OrderItemFirstClear + 1];
        // read funct
        {
            for (int i = 0; i < orderItems.Length; i++)
                orderItems[i] = DataSource.OI_arr[i];
            return orderItems;
        }


    }

}
