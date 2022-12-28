using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;
//using BO;
using Dal;
using DalApi;
using DO;
//using DO;

namespace BlImplementation
{
    internal class Order : BlApi.IOrder
    {
        IDal dal = DalList.Instance;

        /// <summary>
        /// returns list of orders
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BO.OrderForList> GetOrders()
        {
            IEnumerable<DO.Order> doOrders;
            IEnumerable<DO.OrderItem> doOrderItems;
            //  DO.OrderItem orderItem  ;
              
                 doOrders = dal.Order.GetAll();
               doOrderItems = dal.OrderItem.GetAll();
            
          
            List<BO.OrderForList> orderForList = new List<BO.OrderForList>();


            foreach (DO.Order doOrder in doOrders) //run on bo order list

            {

                int amount = 0; // amount of each order item
                double totalPrice = 0; // total price of each order

                foreach (DO.OrderItem doOrderItem in doOrderItems)
                {

                    if (doOrderItem.OrderID == doOrder.ID)

                    {
                        amount += doOrderItem.Amount;
                        totalPrice += doOrderItem.Price * amount;
                    }
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
        public BO.Order GetOrder(int id)
        {
            DO.Order doOrder;
            DO.Product doProduct;


          if  (id <= 0)  throw new BO.invalidInputException(" invalid id") ;

            try { doOrder = dal.Order.GetByID(id); }
            catch (DO.MissingIDException ex)  { throw new BO.MissingIDException(ex.Message);}

            IEnumerable<DO.OrderItem> doOrderItems = dal.OrderItem.GetAll().Where(orderItem => orderItem.OrderID == id);
            List<BO.OrderItem> BoOrderItems = new List<BO.OrderItem>();
            double orderTotalPrice = 0;// total price for the order 
            foreach (DO.OrderItem DoOrderItem in doOrderItems)
            {
                doProduct = dal.Product.GetByID(DoOrderItem.ProductID);
                orderTotalPrice += DoOrderItem.Price * DoOrderItem.Amount;
                BoOrderItems.Add(new()
                {
                    ItemID = DoOrderItem.ID,
                    ProductID = DoOrderItem.ProductID,
                    Amount = DoOrderItem.Amount,
                    ProductName = doProduct.Name,
                    ProductPrice = doProduct.Price,
                    TotalPrice = DoOrderItem.Price * DoOrderItem.Amount



                });
            }


            BO.Order order = new BO.Order()
            { 
                CustomerName = doOrder.CustomerName,
                CustomerAddress = doOrder.CustomerAddress,
                CustomerEmail = doOrder.CustomerEmail,
                //OrderStatus =   doOrder.OrderStatus
                OrderDate = doOrder.OrderDate,
                ShipDate =  doOrder.ShipDate,
                DeliveryDate = doOrder.ShipDate,
                TotalPrice = orderTotalPrice,
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
        public BO.Order UpdateOrderSent(int id)
        {
            DO.Product doProduct;
            DO.Order doOrder;
            try { doOrder = dal.Order.GetByID(id);}
            catch (DO.MissingIDException) { throw new BO.MissingIDException("order ont found"); }
            if (doOrder.OrderDate == null) throw new BO.invalidInputException(" order date is null");
            if (doOrder.ShipDate != null) throw new BO.invalidInputException(" order is already in the system");
            IEnumerable<DO.OrderItem> doOrderItems = dal.OrderItem.GetAll().Where(orderItem => orderItem.OrderID == id); // list if order items from data layer 
            doOrder.ShipDate = DateTime.Now; // update the DO order 
            dal.Order.Update(doOrder.ID, doOrder);
            List<BO.OrderItem> BoOrderItems = new List<BO.OrderItem>();
            foreach (DO.OrderItem DoOrderItem in doOrderItems) // build new BO order item list
            {
                doProduct = dal.Product.GetByID(DoOrderItem.ProductID);
                BoOrderItems.Add(new()
                {
                    ItemID = DoOrderItem.ID,
                    ProductID = DoOrderItem.ProductID,
                    Amount = DoOrderItem.Amount,
                    ProductName = doProduct.Name,
                    ProductPrice = doProduct.Price,
                    TotalPrice = DoOrderItem.Price * DoOrderItem.Amount

                });
            }

            BO.Order boOrder = new ()// build new BO order
                {
                ID = doOrder.ID,
                CustomerName = doOrder.CustomerName,
                CustomerAddress = doOrder.CustomerAddress,
                CustomerEmail = doOrder.CustomerEmail,
                OrderDate =doOrder.OrderDate,
                ShipDate = doOrder.ShipDate,
                DeliveryDate = null,                
                Items = BoOrderItems

                 };        

            return boOrder;       
        
        }
        /// <summary>
        /// checks if order exists
        /// updates that the order has been supplies. returns order
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public BO.Order UpdateOrderSupply(int id)
        {

            DO.Product doProduct;
            DO.Order doOrder;
            try { doOrder = dal.Order.GetByID(id); }
            catch (DO.MissingIDException) { throw new BO.MissingIDException("order ont found"); }
            if (doOrder.OrderDate == null) throw new BO.invalidInputException(" order date is null");
            if (doOrder.ShipDate == null) throw new BO.invalidInputException(" order ship date is null");
            if (doOrder.DeliveryDate != null) throw new BO.invalidInputException(" order has already been delivered");

            IEnumerable<DO.OrderItem> doOrderItems = dal.OrderItem.GetAll().Where(orderItem => orderItem.OrderID == id); // list if order items from data layer 
            doOrder.DeliveryDate = DateTime.Now;            // update the DO order 
            dal.Order.Update(doOrder.ID, doOrder);
            List<BO.OrderItem> BoOrderItems = new List<BO.OrderItem>();
            foreach (DO.OrderItem DoOrderItem in doOrderItems) // build new BO order item list
            {
                doProduct = dal.Product.GetByID(DoOrderItem.ProductID);
                BoOrderItems.Add(new()
                {
                    ItemID = DoOrderItem.ID,
                    ProductID = DoOrderItem.ProductID,
                    Amount = DoOrderItem.Amount,
                    ProductName = doProduct.Name,
                    ProductPrice = doProduct.Price,
                    TotalPrice = DoOrderItem.Price * DoOrderItem.Amount

                });
            }

            BO.Order boOrder = new()// build new BO order
            {
                ID = doOrder.ID,
                CustomerName = doOrder.CustomerName,
                CustomerAddress = doOrder.CustomerAddress,
                CustomerEmail = doOrder.CustomerEmail,
                OrderDate = doOrder.OrderDate,
                ShipDate = doOrder.ShipDate,
                DeliveryDate = doOrder.DeliveryDate,
                Items = BoOrderItems

            };

            return boOrder;



        }
        /// <summary>
        /// returns order track information object
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public BO.OrderTracking OrderTracking(int id)
        {
            //DO.Product doProduct;
            DO.Order doOrder;
            try { doOrder = dal.Order.GetByID(id); }
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
    }
}