using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using BlApi;
//using BO;
//using Dal;
using DalApi;
using DO;


namespace BlImplementation
{
    internal class Order : BlApi.IOrder
    {
        //IDal dal = DalList.Instance;
        DalApi.IDal? dal = DalApi.Factory.Get();


        /// <summary>
        /// returns list of orders
        /// </summary>
        /// <returns></returns>
        /// 
        [MethodImpl(MethodImplOptions.Synchronized)]

        public IEnumerable<BO.OrderForList?> GetOrders()
        {
            IEnumerable<DO.Order?> doOrders = dal!.Order.GetAll();
            IEnumerable<DO.OrderItem?> doOrderItems = dal.OrderItem.GetAll();
                  
          
            List<BO.OrderForList> orderForList = new List<BO.OrderForList>();
            foreach (DO.Order doOrder in doOrders) //run on bo order list
            {
                int amount = 0; // amount of each order item
                double totalPrice = 0; // total price of each order

                IEnumerable<DO.OrderItem?> items = from item in doOrderItems where item.Value.OrderID == doOrder.ID select item;

                // theres a match between order and orderitem
                if (doOrderItems.FirstOrDefault(o => ((DO.OrderItem)o!).OrderID == doOrder.ID) != null)

                {
                    totalPrice = items.Sum(item => item!.Value.Amount * item.Value.Price);
                    amount = items.Count();
                }              
                

               
                    orderForList.Add(new BO.OrderForList()
                    {
                        OrderID = doOrder.ID,
                        CustomerName = doOrder.CustomerName,
                        AmountOfItems = amount,
                        TotalPrice = totalPrice,
                        OrderStatus = doOrder switch
                        {
                            { DeliveryDate: not null } => BO.OrderStatus.delivered,
                            { ShipDate: not null } => BO.OrderStatus.shipped,
                            { OrderDate: not null } => BO.OrderStatus.ordered,
                            _=> BO.OrderStatus.nullState
                        }

                    });
                
            }
            return orderForList;
        }
        /// <summary>
        /// returns details of order
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// 
        [MethodImpl(MethodImplOptions.Synchronized)]

        public BO.Order GetOrder(int id)
        {
            DO.Order doOrder;
            //DO.Product doProduct;

          if  (id <= 0)  throw new BO.invalidInputException(" invalid id") ;

            try { doOrder = dal!.Order.GetByID(id); }
            catch (DO.MissingIDException ex)  { throw new BO.MissingIDException(ex.Message);}
            IEnumerable<DO.OrderItem?> doOrderItems = dal.OrderItem.GetAll(orderItem => ((DO.OrderItem)orderItem!).OrderID == id);
            //IEnumerable<DO.OrderItem?> doOrderItems = dal.OrderItem.GetAll().Where(orderItem => ((DO.OrderItem)orderItem!).OrderID == id);
            IEnumerable<BO.OrderItem> BoOrderItems ;
            //double orderTotalPrice = 0;// total price for the order 
            BoOrderItems = doOrderItems.Where
                (doOrderItem => dal.Product.GetByID(((DO.OrderItem)doOrderItem!).ProductID).ID == ((DO.OrderItem)doOrderItem).ProductID).Select
                (doOrderItem => new BO.OrderItem()
                {
                    ItemID = ((DO.OrderItem)doOrderItem!).ID,
                    ProductID = ((DO.OrderItem)doOrderItem).ProductID,
                    Amount = ((DO.OrderItem)doOrderItem).Amount,
                    ProductName = dal.Product.GetByID(((DO.OrderItem)doOrderItem).ProductID).Name,
                    ProductPrice = dal.Product.GetByID(((DO.OrderItem)doOrderItem).ProductID).Price,
                    TotalPrice = ((DO.OrderItem)doOrderItem).Price * ((DO.OrderItem)doOrderItem).Amount
                }

            );           

            BO.Order order = new()
            {   ID =id,
                CustomerName = doOrder.CustomerName,
                CustomerAddress = doOrder.CustomerAddress,
                CustomerEmail = doOrder.CustomerEmail,

                OrderStatus = doOrder switch
                {
                    { DeliveryDate: not null } => BO.OrderStatus.delivered,
                    { ShipDate: not null } => BO.OrderStatus.shipped,
                    { OrderDate: not null } => BO.OrderStatus.ordered,
                    _ => BO.OrderStatus.nullState
                },
                OrderDate = doOrder.OrderDate,
                PaymentDate = doOrder.OrderDate,
                ShipDate = doOrder.ShipDate,
                DeliveryDate = doOrder.DeliveryDate,
                TotalPrice = BoOrderItems.Sum(items => items.TotalPrice),                
                Items = BoOrderItems
            };


            return order;

        }
        /// <summary>
        /// checks if order exists
        /// updates that the order is sent. returns order
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// 
        [MethodImpl(MethodImplOptions.Synchronized)]

        public BO.Order UpdateOrderSent(int id)
        {
            //DO.Product doProduct;
            DO.Order doOrder;
            try { doOrder = dal!.Order.GetByID(id);}
            catch (DO.MissingIDException) { throw new BO.MissingIDException("order ont found"); }
            if (doOrder.OrderDate == null) throw new BO.invalidInputException(" order date is null");
            if (doOrder.ShipDate != null) throw new BO.invalidInputException(" Order has already been shipped");
            IEnumerable<DO.OrderItem?> doOrderItems = dal.OrderItem.GetAll(orderItem => ((DO.OrderItem)orderItem!).OrderID == id);
           // IEnumerable<DO.OrderItem?> doOrderItems = dal.OrderItem.GetAll().Where(orderItem => ((DO.OrderItem)orderItem!).OrderID == id); // list if order items from data layer 
            doOrder.ShipDate = DateTime.Now; // update the DO order 
            dal.Order.Update(doOrder.ID, doOrder);
          
           
            BO.Order boOrder = GetOrder(doOrder.ID);
            boOrder.OrderStatus = BO.OrderStatus.shipped;


            return boOrder;       
        
        }
        /// <summary>
        /// checks if order exists
        /// updates that the order has been supplies. returns order
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// 

        [MethodImpl(MethodImplOptions.Synchronized)]

        public BO.Order UpdateOrderSupply(int id)
        {

           // DO.Product doProduct;
            DO.Order doOrder;
            try { doOrder = dal!.Order.GetByID(id); }
            catch (DO.MissingIDException) { throw new BO.MissingIDException("order ont found"); }
            if (doOrder.OrderDate == null) throw new BO.invalidInputException(" order date is null");
            if (doOrder.ShipDate == null) throw new BO.invalidInputException(" order ship date is null");
            if (doOrder.DeliveryDate != null) throw new BO.invalidInputException(" order has already been delivered");
            IEnumerable<DO.OrderItem?> doOrderItems = dal.OrderItem.GetAll(orderItem => ((DO.OrderItem)orderItem!).OrderID == id);
           // IEnumerable<DO.OrderItem?> doOrderItems = dal.OrderItem.GetAll().Where( orderItem => ((DO.OrderItem)orderItem!).OrderID == id); // list if order items from data layer 
            doOrder.DeliveryDate = DateTime.Now;            // update the DO order 
            dal.Order.Update(doOrder.ID, doOrder);
           
            
            BO.Order boOrder = GetOrder(doOrder.ID);
            boOrder.OrderStatus = BO.OrderStatus.delivered;             

            return boOrder;
        }
        /// <summary>
        /// returns order track information object
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// 

        [MethodImpl(MethodImplOptions.Synchronized)]

        public BO.OrderTracking OrderTracking(int id)
        {
            //DO.Product doProduct;
            DO.Order doOrder;
            try { doOrder = dal!.Order.GetByID(id); }
            catch (DO.MissingIDException) { throw new BO.MissingIDException("order ont found"); }
            //if (doOrder.OrderDate == null) throw new BO.invalidInputException(" order date is null");
           // if (doOrder.ShipDate == null) throw new BO.invalidInputException(" order ship date is null");
            //if (doOrder.DeliveryDate == null) throw new BO.invalidInputException(" order has already been delivered");
            List<Tuple<DateTime?, string> > statusDescription = new List<Tuple<DateTime?, string>>();
            if (doOrder.OrderDate != null)
                statusDescription.Add(Tuple.Create(doOrder.OrderDate, "order created on " + doOrder.OrderDate));
            if (doOrder.ShipDate != null)
                statusDescription.Add(Tuple.Create(doOrder.ShipDate, "order shipped on " + doOrder.ShipDate));
            if (doOrder.DeliveryDate != null)
                statusDescription.Add(Tuple.Create(doOrder.DeliveryDate, "order delivered on " + doOrder.DeliveryDate));

            // statusDescription.Add 
            BO.OrderTracking boOrderTracking = new()
            {
                OrderID = doOrder.ID,
                StatusDescription = statusDescription,
                OrderStatus = doOrder switch
                {
                    { DeliveryDate: not null } => BO.OrderStatus.delivered,
                    { ShipDate: not null } => BO.OrderStatus.shipped,
                    { OrderDate: not null } => BO.OrderStatus.ordered,
                    _ => BO.OrderStatus.nullState
                },              
                
            };
            return boOrderTracking;
        }
        [MethodImpl(MethodImplOptions.Synchronized)]

        public BO.Order? nextOrder()
        {
            // IEnumerable<DO.Order?> orders = dal!.Order.GetAll(order => order!.Value.ShipDate == null || order!.Value.DeliveryDate == null); // orders not delivered

            IEnumerable<DO.Order?> orders = dal!.Order.GetAll();
            if (orders.Count() == 0)
                //throw new BO.incorrectDataException("All orders have been sent");
                return null;
            var shipped = from order in orders where (order.Value.ShipDate != null && order.Value.DeliveryDate == null) orderby order!.Value.ShipDate select GetOrder(order.Value.ID); //orders shipped 
            var notShipped = from order in orders where order.Value.ShipDate == null orderby order!.Value.OrderDate select GetOrder(order.Value.ID); // not shipped orders
            if (shipped.Count() == 0 && notShipped.Count() == 0)
                return null;
            else
            {
                if (shipped.Count() == 0)
                    return notShipped.First();
                if (notShipped.Count() == 0)
                    return shipped.First();
            }


                return shipped.First().ShipDate<= notShipped.First().OrderDate? shipped.First():notShipped.First();

        }
        
    }
}