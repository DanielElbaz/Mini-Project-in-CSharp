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
            if (DataSource.OrderItemDataList.Find(o => ((OrderItem)o!).ID == oi.ID) != null)
                throw new DuplicateIDException();
            oi.ID = DataSource.Config.getOrderItemLastId();
            DataSource.OrderItemDataList.Add(oi);
            return oi.ID;


        }
        public void Delete(int id)

        {
            int count = DataSource.OrderItemDataList.RemoveAll(o => ((OrderItem)o!).ID == id);
            if (count == 0)
                throw new MissingIDException("not found " + id);
          

        }
        public void Update(int id, OrderItem newOI)
        {

            int count = DataSource.OrderItemDataList.RemoveAll(o => ((OrderItem)o!).ID == id);
            if (count == 0)
                throw new MissingIDException("not found " + id);
            DataSource.OrderItemDataList.Add(newOI);

            
        }



        public OrderItem GetByID(int id)
        {
            var item = DataSource.OrderItemDataList.FirstOrDefault(o => ((OrderItem)o!).ID == id);
            if (item == null)
                throw new MissingIDException();
            return (OrderItem)item;

           
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
           
        }

    }
}