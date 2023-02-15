using DalApi;
using DO;
using System.Data;
using System.Runtime.CompilerServices;

namespace Dal
{

    public class DalOrderItem : IOrderItem
    {
        /// <summary>
        /// add new order item
        /// </summary>
        /// <param name="oi"></param>
        /// <returns>id <returns>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public int Add(OrderItem oi)
        {
            if (DataSource.OrderItemDataList.Find(o => ((OrderItem)o!).ID == oi.ID) != null)
                throw new DuplicateIDException();
            oi.ID = DataSource.Config.getOrderItemLastId();
            DataSource.OrderItemDataList.Add(oi);
            return oi.ID;


        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Delete(int id)

        {
            int count = DataSource.OrderItemDataList.RemoveAll(o => ((OrderItem)o!).ID == id);
            if (count == 0)
                throw new MissingIDException("not found " + id);
          

        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Update(int id, OrderItem newOI)
        {

            int count = DataSource.OrderItemDataList.RemoveAll(o => ((OrderItem)o!).ID == id);
            if (count == 0)
                throw new MissingIDException("not found " + id);
            DataSource.OrderItemDataList.Add(newOI);

            
        }



        [MethodImpl(MethodImplOptions.Synchronized)]
        public OrderItem GetByID(int id)
        {
            var item = DataSource.OrderItemDataList.FirstOrDefault(o => ((OrderItem)o!).ID == id);
            if (item == null)
                throw new MissingIDException();
            return (OrderItem)item;

           
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
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

        [MethodImpl(MethodImplOptions.Synchronized)]
        public OrderItem GetBy(Func<OrderItem?, bool> filter)
        {
            var item = DataSource.OrderItemDataList.FirstOrDefault(o => filter((o)));
            if (item == null)
                throw new invalidInputException("no items found");
            return (OrderItem)item;
           
        }

    }
}