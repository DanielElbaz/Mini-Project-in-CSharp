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
//using DO;
using PL;



namespace PL
{
    /// <summary>
    /// Interaction logic for CartWindow.xaml
    /// </summary>
    public partial class CartWindow : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();


        public double total // total price for cart
        {
            get { return (int)GetValue(totalDP); }
            set { SetValue(totalDP, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty totalDP =
            DependencyProperty.Register("total", typeof(double), typeof(CartWindow), new PropertyMetadata(null));


        public static DependencyProperty CartDependency1 = DependencyProperty.Register(nameof(BO.Cart), typeof(Cart), typeof(CartWindow));
        public Cart Cart1 
        {
            get => (Cart) GetValue(CartDependency1);
        private set => SetValue(CartDependency1, value);
    }

        public static readonly DependencyProperty itemsDependency = DependencyProperty.Register(nameof(items), typeof(ObservableCollection<OrderItem?>), typeof(Window));
        public ObservableCollection<OrderItem?> items
        {
            get => (ObservableCollection<OrderItem?>)GetValue(itemsDependency);
            private set => SetValue(itemsDependency, value);
        }


        public CartWindow(Cart cart)
        {
            Cart1 = cart==null? new() : cart;

            //var temp = cart!.Items;
            var temp = cart!.Items ;
            items = temp == null ? new() : new(from o in temp orderby o.ProductID select o);
            total = Cart1.TotalPrice;
            InitializeComponent();
        }


        private void ConfirmCart_Click(object sender, RoutedEventArgs e)
        {
            
            try { bl!.Cart.ConfirmCart(Cart1);
                MessageBox.Show("order added");
                Cart1 = new();
                this.Close();
            }
            catch(BO.incorrectDataException ex )
            {
                MessageBox.Show(ex.Message, " ", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            catch (BO.MissingIDException ex)
            {
                MessageBox.Show(ex.Message, " ", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }

        }

        private void AddToCartButton_Click(object sender, RoutedEventArgs e)
        {
            OrderItem orderItem = (OrderItem)((sender as Button)!.DataContext!); // the order item
            ProductItem product = bl!.Product.GetProductForCatalog(orderItem.ProductID,Cart1); // get the product
            if (product.IsAvailable == true)
            {
                try
                {
                    Cart1 = bl!.Cart.AddProduct(Cart1, product.ProductID == 0 ? throw new BO.MissingIDException(" Product not Found") : product.ProductID);
                    items = new(from o in Cart1.Items orderby o.ProductID select o);
                    total = Cart1.TotalPrice;
                    //product.AmountInCart++;                    
                    //bool tempBool = (product.AmountInCart < bl.Product.GetProduct(product.ProductID).InStock); //get the amount fro the data
                    //product.IsAvailable = tempBool;
                    
                }
                catch (BO.MissingIDException ex)
                {

                    MessageBox.Show(ex.Message, " ", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }

            }

            else
            {
                MessageBox.Show(" Product is out of stock ", " ", MessageBoxButton.OK);
            }
        }

        private void removeFromCartButton_Click(object sender, RoutedEventArgs e)
        {


            OrderItem orderItem = (OrderItem)((sender as Button)!.DataContext!);
            ProductItem product = bl!.Product.GetProductForCatalog(orderItem.ProductID, Cart1);
            if (product.AmountInCart != 0)
            {
                try
                {
                    Cart1 = bl!.Cart.UpdateAmountOfProduct(Cart1, (int)orderItem.ProductID, --product.AmountInCart);                    
                    total = Cart1.TotalPrice;
                   // bool tempBool = (product.AmountInCart < bl.Product.GetProduct(product.ProductID).InStock); //get the amount fro the data
                    //product.IsAvailable = tempBool;
                    //bl.Cart.UpdateAmountOfProduct(Cart1, orderItem.ProductID, orderItem.Amount++);
                    items = new(from o  in Cart1.Items orderby o.ProductID select o);
                    // MessageBox.Show(" Succesfully added " ," ", MessageBoxButton.OK);
                }
                catch (BO.MissingIDException ex)
                {

                    MessageBox.Show(ex.Message, " ", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }            

        }

        private void deleteProduct_Click(object sender, RoutedEventArgs e)
        {

            OrderItem orderItem = (OrderItem)((sender as Button)!.DataContext!);
            ProductItem product = bl!.Product.GetProductForCatalog(orderItem.ProductID, Cart1);
            if (product.AmountInCart != 0)
            {
                try
                {
                    Cart1 = bl!.Cart.UpdateAmountOfProduct(Cart1, (int)orderItem.ProductID, 0);                    
                    total = Cart1.TotalPrice;
                    //bool tempBool = (product.AmountInCart < bl.Product.GetProduct(product.ProductID).InStock); //get the amount fro the data
                   // product.IsAvailable = tempBool;                    
                    items = new(from o in Cart1.Items orderby o.ProductID select o);
                    
                }
                catch (BO.MissingIDException ex)
                {

                    MessageBox.Show(ex.Message, " ", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }


        }

        private void BackToCart_Click(object sender, RoutedEventArgs e) => this.Close();
        
    }
}
