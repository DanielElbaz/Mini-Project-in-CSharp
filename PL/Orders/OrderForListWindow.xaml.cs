using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using PL.Products;

namespace PL.Orders
{
    /// <summary>
    /// Interaction logic for OrderForListWindow.xaml
    /// </summary>
    /// 
   public partial class OrderForListWindow : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();
        public static readonly DependencyProperty OrdersDependency = DependencyProperty.Register(nameof(Orders), typeof(ObservableCollection<OrderForList?>), typeof(Window));

        public ObservableCollection<OrderForList?> Orders
        {
            get => (ObservableCollection<OrderForList?>)GetValue(OrdersDependency);
            private set => SetValue(OrdersDependency, value);
        }
        public OrderForListWindow()
        {

            InitializeComponent();
            var temp = bl?.Order.GetOrders();
            Orders = temp == null ? new() : new(temp);
            //InitializeComponent();
            //IEnumerable<BO.OrderForList?> orders = bl.Order.GetOrders(); 
            //this.orderListView.ItemsSource = orders;
        }

        private void Order_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            int id = ((OrderForList?)(sender as ListViewItem)?.DataContext)?.OrderID
          ?? throw new NullReferenceException("null event sender");
            new OrderWindow(id,true).Show();
        }
    }
}
