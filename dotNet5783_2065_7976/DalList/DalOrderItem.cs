using DalApi;
using DO;
using System.Data;

namespace Dal
{

    public class DalOrderItem : IOrderItem
    {
        /// <summary>
        /// add new order item
        /// </summary>
        /// <param name="oi"></param>
        /// <returns>id <returns>
        public int Add(OrderItem oi)
        {
            foreach (OrderItem oi1 in DataSource.OI_list)
                if (oi1.ID == oi.ID)
                    throw new DuplicateIDExeption();
            //throw new Exception("Order Item already exists ");
            DataSource.OI_list.Add(oi);
            return oi.ID;

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
                    throw new MissingIDException();
                // throw new Exception("Order Item not found ");
            }




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
                throw new MissingIDException();
            // throw new Exception("Order Item not found");//for (int i = 0; i < DataSource.Config.ProductFirstClear; i++)// check if old exist        

        }

        public OrderItem GetByID(int id)
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

            if (!flag)
                throw new MissingIDException();
            //throw new Exception("Order Item does not exist");
            return DataSource.OI_list[index];
        }
        public IEnumerable<OrderItem> GetAll()
        {
            int count = 0, i = 0;
            foreach (var p in DataSource.OI_list)
            {
                count++;
            }

            OrderItem[] arr = new OrderItem[count];

            foreach (var p in DataSource.OI_list)
            {
                arr[i++] = p;
            }
            return arr;
        }
    }
}