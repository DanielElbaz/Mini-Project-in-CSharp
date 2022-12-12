using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;
using BO;
using Dal;
using DalApi;
//using DO;

namespace BlImplementation
{
    internal class Order : BlApi.IOrder
    {
        IDal dal = new DalList();

        /// <summary>
        /// returns list of orders
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BO.OrderForList> GetOrders()
        {
            IEnumerable<DO.Order> doOrders = dal.Order.GetAll();
            List<BO.OrderForList> orderForList = new List<BO.OrderForList>();
            foreach (DO.Order doOrder in doOrders)
            {
                orderForList.Add(new BO.OrderForList()
                {
                    OrderID = doOrder.ID,
                    CustomerName = doOrder.CustomerName,

                    OrderStatus = doOrder.OrderDate,
                    AmountOfItems = doOrder.
                    TotalPrice = doOrder
                });
            }
            return productsForList;
        }
        /// <summary>
        /// returns details of order
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Order GetOrder(int id)
        { }
        /// <summary>
        /// checks if order exists
        /// updates that the order is sent. returns order
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Order UpdateOrderSent(int id)
        { }
        /// <summary>
        /// checks if order exists
        /// updates that the order has been supplies. returns order
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Order UpdateOrderSupply(int id)
        { }
        /// <summary>
        /// returns order trackinf object
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public OrderTracking OrderTracking(int id)
        { }
    }
}