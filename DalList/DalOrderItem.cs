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
            foreach (OrderItem? oi1 in DataSource.OrderItemDataList)
                if (oi1?.ID == oi.ID)
                    throw new DuplicateIDException();
            //throw new Exception("Order Item already exists ");
            oi.ID = DataSource.Config.getOrderItemLastId();
            DataSource.OrderItemDataList.Add(oi);
            return oi.ID;

        }
        public void Delete(int id)

        {
            Boolean flag = false;
            foreach (OrderItem? oi in DataSource.OrderItemDataList)
            {
                if (id == oi?.ID)
                {
                    flag = true;
                    DataSource.OrderItemDataList.RemoveAll(item => ((OrderItem)item!).ID ==id);
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
            foreach (OrderItem? orderitem in DataSource.OrderItemDataList)
                if (id == orderitem?.ID)
                {
                    flag = true;
                    int index = DataSource.OrderItemDataList.IndexOf(orderitem);
                    DataSource.OrderItemDataList[index] = newOI;
                    break;

                }
            if (!flag)
                throw new MissingIDException();
            // throw new Exception("Order Item not found");//for (int i = 0; i < DataSource.Config.ProductFirstClear; i++)// check if old exist        

        }



        public OrderItem GetByID(int id)
        {
            var item = DataSource.OrderItemDataList.FirstOrDefault(o => ((OrderItem)o!).ID == id);
            if (item == null)
                throw new MissingIDException();
            return (OrderItem)item;

            //int index = -1;
            //foreach (OrderItem orderitem in DataSource.OrderItemDataList)

            //    if (id == orderitem.ID)
            //    {
            //        //flag = true;

            //        index = DataSource.OrderItemDataList.IndexOf(orderitem);
            //        break;
            //    }

            //if (index ==-1)
            //    throw new MissingIDException();

            //return DataSource.OrderItemDataList[index];
        }
        public IEnumerable<OrderItem?> GetAll(Func<OrderItem?, bool>? filter = null)
        {
            if (filter == null)
                return DataSource.OrderItemDataList;

            List<OrderItem?> orderItems = new();
            foreach (var o in DataSource.OrderItemDataList)
                if (filter(o))
                    orderItems.Add(o);
            return orderItems;


        }

        public OrderItem GetBy(Func<OrderItem?, bool> filter)
        {
            var item = DataSource.OrderItemDataList.FirstOrDefault(o => filter((o)));
            if (item == null)
                throw new invalidInputException("no items found");
            return (OrderItem)item;
            //bool flag = false;
            //if (filter == null)
            //    throw new invalidInputException();
            //OrderItem orderItem = new();
            //foreach (var o in DataSource.OrderItemDataList)
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