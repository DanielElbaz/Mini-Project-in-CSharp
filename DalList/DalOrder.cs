
using DalApi;
using DO;
using System.Runtime.CompilerServices;
using static System.Collections.Specialized.BitVector32;

namespace Dal
{

    public class DalOrder : IOrder
    {

        [MethodImpl(MethodImplOptions.Synchronized)]
        public int Add(Order order)
        {
            if (DataSource.OrderDataList.Find(o => ((Order)o!).ID == order.ID) != null)
                throw new DuplicateIDException();
            order.ID = DataSource.Config.getOrderLastId();
            DataSource.OrderDataList.Add(order);
            return order.ID;
           
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Delete(int id) // delete product by id
        {
            int count = DataSource.OrderDataList.RemoveAll(o => ((Order)o!).ID == id);
            if (count == 0)
                throw new MissingIDException("not found " + id);
            
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Update(int id, Order newOrder) // update old with new
        {
            int count = DataSource.OrderDataList.RemoveAll(o => ((Order)o!).ID == id);
            if (count == 0)
                throw new MissingIDException("not found " + id);
            DataSource.OrderDataList.Add(newOrder);
        }


        [MethodImpl(MethodImplOptions.Synchronized)]
        public Order GetByID(int id)
        {
            var item = DataSource.OrderDataList.FirstOrDefault(o => ((Order)o!).ID == id);
            if (item == null)
                throw new MissingIDException();
            return (Order)item;
          
        }


        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Order?> GetAll(Func<Order?, bool>? filter = null)
        {
            if(filter ==null)
                return DataSource.OrderDataList;

            List<Order?> orders = new();
            foreach (var o in DataSource.OrderDataList)
                if(filter(o))
                    orders.Add(o);
            return orders;
            
           
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public Order GetBy(Func<Order?, bool> filter)
        {
            var item = DataSource.OrderDataList.FirstOrDefault(o => filter((o)));
            if (item == null)
                throw new invalidInputException("no items found");
            return (Order)item;
           
        }
    }
}