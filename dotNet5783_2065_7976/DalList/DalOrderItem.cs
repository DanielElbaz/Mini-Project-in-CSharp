﻿using DalApi;
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
            foreach (OrderItem oi1 in DataSource.OI_list)
                if (oi1.ID == oi.ID)
                    throw new DuplicateIDExeption();
            //throw new Exception("Order Item already exists ");
            oi.ID = DataSource.Config.getOrderItemLastId();
            DataSource.OI_list.Add(oi);
            return oi.ID;

        }
        public void Delete(int id)

        {
            Boolean flag = false;
            foreach (OrderItem oi in DataSource.OI_list)
            {
                if (id == oi.ID)
                {
                    flag = true;
                    DataSource.OI_list.Remove(oi);
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
            foreach (OrderItem orderitem in DataSource.OI_list)
                if (id == orderitem.ID)
                {
                    flag = true;
                    int index = DataSource.OI_list.IndexOf(orderitem);
                    DataSource.OI_list[index] = newOI;
                    break;

                }
            if (!flag)
                throw new MissingIDException();
            // throw new Exception("Order Item not found");//for (int i = 0; i < DataSource.Config.ProductFirstClear; i++)// check if old exist        

        }



        public OrderItem GetByID(int id)
        {
            //int i;
            int index = -1;
            //Boolean flag = false;
            foreach (OrderItem orderitem in DataSource.OI_list)

                if (id == orderitem.ID)
                {
                    //flag = true;

                    index = DataSource.OI_list.IndexOf(orderitem);
                    break;
                }

            if (index ==-1)
                throw new MissingIDException();
            //throw new Exception("Order Item does not exist");
            return DataSource.OI_list[index];
        }
        public IEnumerable<OrderItem> GetAll()
        {
            

            List<OrderItem> orderItems = new List<OrderItem>();
            for (int i = 0; i < DataSource.OI_list.Count; i++)
                orderItems.Add(DataSource.OI_list[i]);
            return orderItems;

            
        }

        
    }
}