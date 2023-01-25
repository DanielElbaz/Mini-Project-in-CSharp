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
using PL.Orders;

namespace PL
{
    
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {


        public static DependencyProperty TrackOrderDependency = DependencyProperty.Register(nameof(BO.OrderTracking), typeof(OrderTracking), typeof(OrderTrackingWindow));
        public OrderTracking orderTracking
        {
            get => (OrderTracking)GetValue(TrackOrderDependency);
            private set => SetValue(TrackOrderDependency, value);
        }


        //  private IBl bl = new Bl();
        BlApi.IBl? bl = BlApi.Factory.Get();
        public MainWindow()
        {
            InitializeComponent();
          

        }
       

        //private void ShowProductsButton_Click(object sender, RoutedEventArgs e) => new ProductForListWindow().Show();
        private void ShowManagerButton_Click(object sender, RoutedEventArgs e) => new ManagerWindow().Show();
        private void ShowCatalog_Click(object sender, RoutedEventArgs e) => new CatalogWindow().Show();

        private void OrderTracking_Click(object sender, RoutedEventArgs e)
        {
            {
                int id = 0;
                BO.Order order;
                try
                {

                    if (int.TryParse(idBox.Text, out id))

                    {
                        orderTracking = bl!.Order.OrderTracking(id);
                        order = bl.Order.GetOrder(id);
                       MessageBoxResult result =  MessageBox.Show(orderTracking.ToString() + "\n" + order.ToString() +" Press OK to Show details", " ", MessageBoxButton.OK);
                        if(result== MessageBoxResult.OK) 
                        {
                            new OrderWindow(id,false).Show();
                        }
                    }
                    //orderTrackingListView.

                    else
                        MessageBox.Show("invalid Input", " ", MessageBoxButton.OK, MessageBoxImage.Exclamation);

                }
                catch (BO.MissingIDException ex)
                {
                    MessageBox.Show(ex.Message, " ", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
        }
        
    }
}
