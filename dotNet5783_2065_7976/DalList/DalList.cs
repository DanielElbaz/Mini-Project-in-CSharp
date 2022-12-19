﻿using DalApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
    sealed public class DalList : IDal

    {
        private static IDal instance;
        public static IDal Instance { get { return instance; } }
        static DalList()
        {
            instance = new DalList();
        }
        private DalList() {}
        public IProduct Product => new DalProduct();
        public IOrder Order => new DalOrder();
        public IOrderItem OrderItem => new DalOrderItem();

    }
}