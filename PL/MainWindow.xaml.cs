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
using System.Windows.Navigation;
using System.Windows.Shapes;
using BO;
using BlApi;
using BL;
using BlImplementation;
using PL.Products;
using static System.Net.WebRequestMethods;


namespace PL
{
    
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //  private IBl bl = new Bl();
        BlApi.IBl? bl = BlApi.Factory.Get();
        public MainWindow()
        {
            InitializeComponent();
          

        }
       

        //private void ShowProductsButton_Click(object sender, RoutedEventArgs e) => new ProductForListWindow().Show();
        private void ShowManagerButton_Click(object sender, RoutedEventArgs e) => new ManagerWindow().Show();
        // private void ShowOrdersButton_Click(object sender, RoutedEventArgs e) => new OrderForListWindow().Show();
      
    }
}
