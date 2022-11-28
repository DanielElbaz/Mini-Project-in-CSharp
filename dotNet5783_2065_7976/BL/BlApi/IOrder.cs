﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BO;
namespace BlApi;
/// <summary>
/// interfce for order
/// </summary>
public interface IOrder
{
    /// <summary>
    /// returns list of orders
    /// </summary>
    /// <returns></returns>
    public IEnumerable<OrderForList> GetOrders();
    /// <summary>
    /// returns details of order
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Order GetOrder(int id);
    /// <summary>
     /// checks if order exists
    /// updates that the order is sent. returns order
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
     public Order UpdateOrderSent(int id);
    /// <summary>
    /// checks if order exists
    /// updates that the order has been supplies. returns order
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Order UpdateOrderSupply(int id);
    /// <summary>
    /// returns order trackinf object
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public OrderTracking OrderTracking(int id); 
}
