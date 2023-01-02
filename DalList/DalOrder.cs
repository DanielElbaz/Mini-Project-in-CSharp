
using DalApi;
using DO;
using static System.Collections.Specialized.BitVector32;

namespace Dal
{

    public class DalOrder : IOrder
    {

        public int Add(Order order)
        {
            foreach (Order? order1 in DataSource.O_list)
                if (order1?.ID == order.ID)
                    throw new DuplicateIDExeption(); //duplicateID
                                                     //  throw new Exception("order already exists ");
            order.ID = DataSource.Config.getOrderLastId();
            DataSource.O_list.Add(order);

            return order.ID;
        }

        public void Delete(int id) // delete product by id
        {
            Boolean flag = false;
            foreach (Order? order in DataSource.O_list)
            {
                if (id == order?.ID)
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
            foreach (Order? order in DataSource.O_list)
                if (id == order?.ID)
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
            var item = DataSource.O_list.FirstOrDefault(o => ((Order)o!).ID == id);
            if (item == null)
                throw new MissingIDException();
            return (Order)item;
            //int index = -1;
            //foreach (Order order in DataSource.O_list)

            //    if (id == order.ID)
            //    {
            //        //flag = true;

            //        index = DataSource.O_list.IndexOf(order);
            //        break;
            //    }
            //if (index == -1)
            //    throw new MissingIDException();
            //return (Order)DataSource.O_list[index] ;


        }

        public IEnumerable<Order?> GetAll(Func<Order?, bool>? filter = null)
        {
            if(filter ==null)
                return DataSource.O_list;

            List<Order?> orders = new();
            foreach (var o in DataSource.O_list)
                if(filter(o))
                    orders.Add(o);
            return orders;
            
           
        }

        public Order GetBy(Func<Order?, bool> filter)
        {
            var item = DataSource.O_list.FirstOrDefault(o => filter((o)));
            if (item == null)
                throw new invalidInputException("no items found");
            return (Order)item;
            //bool flag = false;
            //if (filter ==null)
            //  throw new invalidInputException();
            //Order order = new();
            //foreach (var o in DataSource.O_list)
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