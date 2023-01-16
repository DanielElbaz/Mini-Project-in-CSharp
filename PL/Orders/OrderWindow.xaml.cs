using BO;
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


        public static readonly DependencyProperty OrderDependency = DependencyProperty.Register(nameof(Order), typeof(BO.Order), typeof(Window));
        public Order? Order { get => (Order)GetValue(OrderDependency); private set => SetValue(OrderDependency, value); }
        public OrderWindow(int id = 0)
        {
            try
            {
                Order = id == 0 ? new() { } : bl.Order.GetOrder(id);
                InitializeComponent();
            }
            catch (MissingIDException ex)
            {
                Close();
                MessageBox.Show(ex.Message, "Failed to get the entity", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

    }
}
