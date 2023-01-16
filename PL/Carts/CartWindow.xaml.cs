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
using PL;



namespace PL
{
    /// <summary>
    /// Interaction logic for CartWindow.xaml
    /// </summary>
    public partial class CartWindow : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();

        public static DependencyProperty CartDependency = DependencyProperty.Register(nameof(BO.Cart), typeof(Cart), typeof(Window));
        public Cart Cart1
        {
            get => (Cart)GetValue(CartDependency);
            private set => SetValue(CartDependency, value);
        }

        public static readonly DependencyProperty itemsDependency = DependencyProperty.Register(nameof(items), typeof(ObservableCollection<OrderItem?>), typeof(Window));
        public ObservableCollection<OrderItem?> items
        {
            get => (ObservableCollection<OrderItem?>)GetValue(itemsDependency);
            private set => SetValue(itemsDependency, value);
        }
        public CartWindow(Cart cart)
        {
            var temp = cart.Items;
            items = temp == null ? new() : new(temp!);
            InitializeComponent();
        }


        private void ConfirmCart_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
