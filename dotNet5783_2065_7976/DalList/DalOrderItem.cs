using DalApi;
using System.Data;

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
        foreach(OrderItem oi1 in DataSource.OI_list)
            if (oi1.ID == oi.ID)
                throw new Exception("Order Item already exists ");
        DataSource.OI_list.Add(oi);
        return oi.ID;
        //for (int i = 0; i < DataSource.Config.OrderItemFirstClear; i++)
        //{
        //    if (oi.ID == DataSource.OI_arr[i].ID) // it exist
        //        throw new Exception("order item already exist ");

        //}
        //DataSource.OI_arr[DataSource.Config.OrderItemFirstClear] = oi;
        //DataSource.Config.ProductFirstClear++;
        
    }
    public void Delete(int id)

    {
        Boolean flag = false;
        foreach (OrderItem oi in DataSource.OI_list)
        {
            if (id == oi.ID)
            {
                flag = true;
                DataSource.OI_list.Remove(oi);
            }
            if (!flag)
                throw new Exception("Order Item not found ");
        }
        //for (int i = 0; i < DataSource.Config.OrderItemFirstClear; i++)
        //{
        //    if (id == DataSource.OI_arr[i].ID)
        //    {
        //        flag = true;
        //        for (int j = i; j < DataSource.Config.OrderItemFirstClear; j++)
        //            DataSource.OI_arr[j] = DataSource.OI_arr[j + 1];
        //        DataSource.Config.OrderItemFirstClear--;
        //    }

        


    }
    public void Update(int id, OrderItem newOI)
    {
        Boolean flag = false;
        foreach (OrderItem orderitem in DataSource.OI_list)
            if (id == orderitem.ID)
            {
                flag = true;
                int index = DataSource.OI_list.IndexOf(orderitem);
                DataSource.OI_list[index] = newOI;

            }
        if (!flag)
            throw new Exception("Order Item not found");//for (int i = 0; i < DataSource.Config.ProductFirstClear; i++)// check if old exist        
        //    if (DataSource.OI_arr[i].ID == id)
        //    {
        //        f = true;
        //        DataSource.OI_arr[i] = newOI;
        //    }
       
    }

    public OrderItem getOneOrderItem(int id )
    {
        //int i;
        int index = -1;
        Boolean flag = false;
        foreach (OrderItem orderitem in DataSource.OI_list)

            if (id == orderitem.ID)
            {
                //flag = true;

                index = DataSource.OI_list.IndexOf(orderitem);
                break;
            }
        //for (i = 0; i < DataSource.Config.OrderItemFirstClear; i++)
        //    if (DataSource.OI_arr[i].ID == id)
        //    { f = true; break; }
        if (!flag)
            throw new Exception("Order Item does not exist");
         return DataSource.OI_list[index];
    }
    public List<OrderItem> getAllOrderItems()

    {
        return DataSource.OI_list;
        //OrderItem[]  orderItems = new OrderItem[DataSource.Config.OrderItemFirstClear + 1];
        // read funct
        //{
        //    for (int i = 0; i < orderItems.Length; i++)
        //        orderItems[i] = DataSource.OI_arr[i];
        //    return orderItems;
        //}
    }
}
