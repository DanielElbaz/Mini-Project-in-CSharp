
using DalApi;
using DO;
using static System.Collections.Specialized.BitVector32;

namespace Dal
{

    public class DalOrder : IOrder
    {

        public int Add(Order order)
        {
            foreach (Order? order1 in DataSource.OrderDataList)
                if (order1?.ID == order.ID)
                    throw new DuplicateIDException(); //duplicateID
                                                     //  throw new Exception("order already exists ");
            order.ID = DataSource.Config.getOrderLastId();
            DataSource.OrderDataList.Add(order);

            return order.ID;
        }

        public void Delete(int id) // delete product by id
        {
            Boolean flag = false;
            foreach (Order? order in DataSource.OrderDataList)
            {
                if (id == order?.ID)
                {
                    flag = true;
                    DataSource.OrderDataList.Remove(order);
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
            foreach (Order? order in DataSource.OrderDataList)
                if (id == order?.ID)
                {
                    flag = true;
                    int index = DataSource.OrderDataList.IndexOf(order);
                    DataSource.OrderDataList[index] = newOrder;
                    break;

                }
            if (!flag)
                throw new MissingIDException();
            // throw new Exception("order not found");


        }


        public Order GetByID(int id)
        {
            var item = DataSource.OrderDataList.FirstOrDefault(o => ((Order)o!).ID == id);
            if (item == null)
                throw new MissingIDException();
            return (Order)item;
            //int index = -1;
            //foreach (Order order in DataSource.OrderDataList)

            //    if (id == order.ID)
            //    {
            //        //flag = true;

            //        index = DataSource.OrderDataList.IndexOf(order);
            //        break;
            //    }
            //if (index == -1)
            //    throw new MissingIDException();
            //return (Order)DataSource.OrderDataList[index] ;


        }

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

        public Order GetBy(Func<Order?, bool> filter)
        {
            var item = DataSource.OrderDataList.FirstOrDefault(o => filter((o)));
            if (item == null)
                throw new invalidInputException("no items found");
            return (Order)item;
            //bool flag = false;
            //if (filter ==null)
            //  throw new invalidInputException();
            //Order order = new();
            //foreach (var o in DataSource.OrderDataList)
            //    if (filter(o))
            //    { flag = true;
            //     order = (Order)o!; 
            //    }
            //if(!flag)
            //    throw new invalidInputException("no items found");
            //return order;

        }
    }
}