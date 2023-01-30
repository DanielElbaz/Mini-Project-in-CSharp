using BO;
//using DO;
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

namespace PL.Orders
{
    /// <summary>
    /// Interaction logic for OrderWindow.xaml
    /// </summary>
    public partial class OrderWindow : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();



        public bool manager
        {
            get { return (bool)GetValue(managerDP); }
            set { SetValue(managerDP, value); }
        }

        // Using a DependencyProperty as the backing store for manager.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty managerDP =
            DependencyProperty.Register("manager", typeof(bool), typeof(OrderWindow), new PropertyMetadata(null));


        public static readonly DependencyProperty OrderDependency = DependencyProperty.Register(nameof(Order), typeof(BO.Order), typeof(Window));
        public Order? Order { get => (Order)GetValue(OrderDependency); private set => SetValue(OrderDependency, value); }
        public OrderWindow(int id = 0, bool edit = false)
        {
            manager = edit;
            try
            {
                Order = id == 0 ? new() { } : bl.Order.GetOrder(id);
                InitializeComponent();
            //upd.Visibility = Visibility.Hidden;
            }
            catch (MissingIDException ex)
            {
                Close();
                MessageBox.Show(ex.Message, "Failed to get the entity", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

    

        private void updateSent_Click(object sender, RoutedEventArgs e)
        {
            // BO.Order order = (BO.Order)((sender as Button)!.DataContext!);
            
            try
            {
               Order = bl?.Order.UpdateOrderSent(Order!.ID);
                //Order = new(bl.Order.GetOrder(order.ID));
               // Order = bl.Order.GetOrder(order.ID);
            }
            catch (invalidInputException ex)
            {
                Close();
                MessageBox.Show(ex.Message, " ", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void updateDelivery_Click(object sender, RoutedEventArgs e)
        {
            //Order order = (Order)((sender as Button)!.DataContext!);
            try
            {
                Order = bl?.Order.UpdateOrderSupply(Order!.ID);
                //Order = bl.Order.GetOrder(order.ID);
            }
            catch (invalidInputException ex)
            {
                Close();
                MessageBox.Show(ex.Message, " ", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
