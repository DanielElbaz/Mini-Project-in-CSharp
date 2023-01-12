﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BO;
using DO;

namespace PL.Orders
{
    /// <summary>
    /// Interaction logic for OrderForListWindow.xaml
    /// </summary>
    /// 
   public partial class OrderForListWindow : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();
        public OrderForListWindow()
        {
            InitializeComponent();
            IEnumerable<BO.OrderForList?> orders = bl.Order.GetOrders(); 
            this.orderListView.ItemsSource = orders;
        }

        private void Order_MouseDoubleClick(object sender, MouseButtonEventArgs e) => new OrderWindow().Show();
    }
}
