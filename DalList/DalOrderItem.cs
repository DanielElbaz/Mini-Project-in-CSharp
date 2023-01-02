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
            foreach (OrderItem? oi1 in DataSource.OI_list)
                if (oi1?.ID == oi.ID)
                    throw new DuplicateIDExeption();
            //throw new Exception("Order Item already exists ");
            oi.ID = DataSource.Config.getOrderItemLastId();
            DataSource.OI_list.Add(oi);
            return oi.ID;

        }
        public void Delete(int id)

        {
            Boolean flag = false;
            foreach (OrderItem? oi in DataSource.OI_list)
            {
                if (id == oi?.ID)
                {
                    flag = true;
                    DataSource.OI_list.RemoveAll(item => ((OrderItem)item!).ID ==id);
                    break;
                }
                if (!flag)
                    throw new MissingIDException();
                // throw new Exception("Order Item not found ");
            }




        }
        public void Update(int id, OrderItem newOI)
        {
            Boolean flag = false;
            foreach (OrderItem? orderitem in DataSource.OI_list)
                if (id == orderitem?.ID)
                {
                    flag = true;
                    int index = DataSource.OI_list.IndexOf(orderitem);
                    DataSource.OI_list[index] = newOI;
                    break;

                }
            if (!flag)
                throw new MissingIDException();
            // throw new Exception("Order Item not found");//for (int i = 0; i < DataSource.Config.ProductFirstClear; i++)// check if old exist        

        }



        public OrderItem GetByID(int id)
        {
            var item = DataSource.OI_list.FirstOrDefault(o => ((OrderItem)o!).ID == id);
            if (item == null)
                throw new MissingIDException();
            return (OrderItem)item;

            //int index = -1;
            //foreach (OrderItem orderitem in DataSource.OI_list)

            //    if (id == orderitem.ID)
            //    {
            //        //flag = true;

            //        index = DataSource.OI_list.IndexOf(orderitem);
            //        break;
            //    }

            //if (index ==-1)
            //    throw new MissingIDException();

            //return DataSource.OI_list[index];
        }
        public IEnumerable<OrderItem?> GetAll(Func<OrderItem?, bool>? filter = null)
        {
            if (filter == null)
                return DataSource.OI_list;

            List<OrderItem?> orderItems = new();
            foreach (var o in DataSource.OI_list)
                if (filter(o))
                    orderItems.Add(o);
            return orderItems;


        }

        public OrderItem GetBy(Func<OrderItem?, bool> filter)
        {
            var item = DataSource.OI_list.FirstOrDefault(o => filter((o)));
            if (item == null)
                throw new invalidInputException("no items found");
            return (OrderItem)item;
            //bool flag = false;
            //if (filter == null)
            //    throw new invalidInputException();
            //OrderItem orderItem = new();
            //foreach (var o in DataSource.OI_list)
            //    if (filter(o))
            //    {
            //        flag = true;
            //        orderItem = (OrderItem)o!;
            //    }
            //if (!flag)
            //    throw new invalidInputException("no items found");
            //return orderItem;

        }

    }
}