﻿using PL.Products;
using PL.Orders;
using System;
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

namespace PL
{
    /// <summary>
    /// Interaction logic for ManagerWindow.xaml
    /// </summary>
    public partial class ManagerWindow : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();
        public ManagerWindow()
        {
            InitializeComponent();
        }
        private void ShowProductsButton_Click(object sender, RoutedEventArgs e) => new ProductForListWindow().Show();
        private void ShowOrdersButton_Click(object sender, RoutedEventArgs e) => new OrderForListWindow().Show();
    }
    
    
}
