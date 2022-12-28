
using DalApi;
using DO;
using static System.Collections.Specialized.BitVector32;

namespace Dal
{

    public class DalOrder : IOrder
    {

        public int Add(Order order)
        {
            foreach (Order order1 in DataSource.O_list)
                if (order1.ID == order.ID)
                    throw new DuplicateIDExeption(); //duplicateID
                                                     //  throw new Exception("order already exists ");
            order.ID = DataSource.Config.getOrderLastId();
            DataSource.O_list.Add(order);

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
                    break;
                }

                if (!flag)
                    throw new MissingIDException();
                //  throw new Exception("order not found ");
            }


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
                    break;

                }
            if (!flag)
                throw new MissingIDException();
            // throw new Exception("order not found");


        }


        public Order GetByID(int id)
        {
            //int i;
            int index = -1;
            //Boolean flag = false;
            foreach (Order order in DataSource.O_list)

                if (id == order.ID)
                {
                    //flag = true;

                    index = DataSource.O_list.IndexOf(order);
                    break;
                }
            if (index == -1)
                throw new MissingIDException();
            // throw new Exception("Order doesnt exist");
            return DataSource.O_list[index];
            //for (i = 0; i < DataSource.Config.OrderFirstClear; i++)
            //    if (DataSource.O_arr[i].ID == id)
            //    { flag = true; break; }


        }

        public IEnumerable<Order> GetAll()
        {

            List<Order> orders = new List<Order>();
            for (int i = 0; i < DataSource.O_list.Count; i++)
                orders.Add(DataSource.O_list[i]);
            return orders;
            
           
        }


    }
}