using BO;

using PL.Products;
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


namespace PL
{
    /// <summary>
    /// Interaction logic for CatalogWindow.xaml
    /// </summary>
    public partial class CatalogWindow : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();

        public static  DependencyProperty CartDependency = DependencyProperty.Register(nameof(BO.Cart), typeof(Cart), typeof(Window));
        public Cart Cart
        {
            get => (Cart)GetValue(CartDependency);
            private set => SetValue(CartDependency, value);
        }

        public static  DependencyProperty ProductsDependency = DependencyProperty.Register(nameof(Products1), typeof(ObservableCollection<ProductItem>), typeof(Window));
        public ObservableCollection<ProductItem?> Products1
        {
            get => (ObservableCollection<ProductItem?>)GetValue(ProductsDependency);
            private set => SetValue(ProductsDependency, value);
        }
         public Category Category { get; set; }

        //public static readonly DependencyProperty CategoryDependency = DependencyProperty.Register(nameof(BO.Category), typeof(ObservableCollection<BO.Category>), typeof(Window));
        //public ObservableCollection<BO.Category> Category
        //{
        //    get => (ObservableCollection<BO.Category>) GetValue(CategoryDependency);
        //    private set => SetValue(CategoryDependency, value);
        //}

        public Array Categories { get { return Enum.GetValues(typeof(BO.Category)); } }


        public CatalogWindow()
        {

            Cart = new() { Items = new() };
            //Category = Category.None;
            var temp = bl?.Product.GetAllCatalog().OrderBy(p =>p!.ProductID);
            Products1 = temp == null ? new() : new(temp);
            InitializeComponent();


        }
        private void ProductListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //ProductListView.ItemsSource = bl.Product.GetAll();

        }

        private void categoryList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        { 
            var temp = Category == BO.Category.None ?
            bl?.Product.GetAllCatalog().OrderBy(p =>p!.ProductID) : bl?.Product.GetAllCatalog().Where(item => item!.Category == Category);
        Products1 = temp == null ? new () : new (temp);
        }
       // private void addProductBtn_Click(object sender, RoutedEventArgs e) => new ProductWindow().Show();

        private void ProductItemView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            BO.ProductItem p = (BO.ProductItem)ProductItemListView.SelectedItem;
            int id = p.ProductID;
            new ProductWindow(id).Show();
            InitializeComponent();
            // this.Refresh();
        }

        private void Cart_Click(object sender, RoutedEventArgs e)
        {
            if (Cart.TotalPrice > 0)
            {
                new CartWindow(Cart).ShowDialog();
                Reset_Click(sender, e);
            }
            else
                MessageBox.Show("No items in the cart", " ", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }
        private void AddToCartButton_Click(object sender, RoutedEventArgs e)
        {
            ProductItem product = (ProductItem)((sender as Button)!.DataContext!);
            if (product.IsAvailable == true )
            {
                try
                {
                    Cart = bl!.Cart.AddProduct(Cart, product.ProductID ==0? throw new BO.MissingIDException (" Product not Found"): product.ProductID);
                    
                    Products1 = new(from p in Products1 orderby p.ProductID select p);
                    product.AmountInCart++;
                    bool tempBool = (product.AmountInCart < bl.Product.GetProduct(product.ProductID).InStock); //get the amount fro the data
                    product.IsAvailable = tempBool;
                    // MessageBox.Show(" Succesfully added " ," ", MessageBoxButton.OK);
                }
                catch (BO.MissingIDException ex)
                {

                    MessageBox.Show(ex.Message, " ", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
                catch (BO.invalidInputException ex)
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

            ProductItem product = (ProductItem)((sender as Button)!.DataContext!);
            if (product.AmountInCart != 0)
            {
                try
                {
                    Cart = bl!.Cart.UpdateAmountOfProduct(Cart, (int)product.ProductID, product.AmountInCart - 1);
                    product.AmountInCart--;
                    bool tempBool = (product.AmountInCart < bl.Product.GetProduct(product.ProductID).InStock); //get the amount fro the data
                    product.IsAvailable = tempBool;
                    Products1 = new(from p in Products1 orderby p.ProductID select p);
                    // MessageBox.Show(" Succesfully added " ," ", MessageBoxButton.OK);
                }
                catch (BO.MissingIDException ex)
                {

                    MessageBox.Show(ex.Message, " ", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
                catch (BO.invalidInputException ex)
                {

                    MessageBox.Show(ex.Message, " ", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
            

        }


        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            Cart = new() { Items = new() };            
            foreach( ProductItem? product in Products1)
            {
                product!.AmountInCart = 0;
                bool tempBool = (product.AmountInCart < bl!.Product.GetProduct(product.ProductID).InStock); //get the amount fro the data
                product.IsAvailable = tempBool;
            }
            Products1 = new(from p in Products1 orderby p.ProductID select p);
        }
    }
}
