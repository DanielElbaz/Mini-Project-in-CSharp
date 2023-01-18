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


        //public int MyProperty
        //{
        //    get { return (int)GetValue(MyPropertyProperty); }
        //    set { SetValue(MyPropertyProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty MyPropertyProperty =
        //    DependencyProperty.Register("MyProperty", typeof(int), typeof(), new PropertyMetadata(0));


        public static DependencyProperty CartDependency1 = DependencyProperty.Register(nameof(BO.Cart), typeof(Cart), typeof(CartWindow));
        public Cart Cart1 
        {
            get => (Cart) GetValue(CartDependency1);
        private set => SetValue(CartDependency1, value);
    }

    //public static readonly DependencyProperty itemsDependency = DependencyProperty.Register(nameof(items), typeof(ObservableCollection<OrderItem?>), typeof(Window));
    //    public ObservableCollection<OrderItem?> items
    //    {
    //        get => (ObservableCollection<OrderItem?>)GetValue(itemsDependency);
    //        private set => SetValue(itemsDependency, value);
    //    }

        
        public CartWindow(Cart cart)
        {
            Cart1 = cart==null? new() : cart;

            // var temp = cart!.Items;
           // var temp = Cart1!.Items;
            //items = temp == null ? new() : new(temp!);
            
            InitializeComponent();
        }


        private void ConfirmCart_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
