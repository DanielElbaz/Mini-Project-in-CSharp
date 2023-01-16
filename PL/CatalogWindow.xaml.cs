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

        public static readonly DependencyProperty CartDependency = DependencyProperty.Register(nameof(BO.Cart), typeof(Cart), typeof(Window));
        public Cart Cart
        {
            get => (Cart)GetValue(CartDependency);
            private set => SetValue(CartDependency, value);
        }

        public static readonly DependencyProperty ProductsDependency = DependencyProperty.Register(nameof(Products1), typeof(ObservableCollection<ProductItem>), typeof(Window));
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
            var temp = bl?.Product.GetAllCatalog(null);
            Products1 = temp == null ? new() : new(temp);
            InitializeComponent();
            //var temp = bl.Product.GetAll();
            //Products = temp == null ? new() : new(temp);           
            //InitializeComponent();
            //this.categoryList.ItemsSource = Enum.GetValues(typeof(BO.Category));
            //this.categoryList.SelectedIndex = 0;
            //this.ProductItemListView.ItemsSource = bl?.Product.GetAllCatalog( null);

        }
        private void ProductListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //ProductListView.ItemsSource = bl.Product.GetAll();

        }

        private void categoryList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        //    BO.Category category = (BO.Category)categoryList.SelectedItem;
        //    //categoryList.Focus();
        //    ProductItemListView.ItemsSource =    bl.Product.GetAll(elem => elem.Category == category);
            var temp = Category == BO.Category.None ?
            bl?.Product.GetAllCatalog() : bl?.Product.GetAllCatalog().Where(item => item!.Category == Category);
        Products1 = temp == null ? new () : new (temp);
        }
        private void addProductBtn_Click(object sender, RoutedEventArgs e) => new ProductWindow().Show();

        private void ProductItemView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            BO.ProductItem p = (BO.ProductItem)ProductItemListView.SelectedItem;
            int id = p.ProductID;
            new ProductWindow(id).Show();
            InitializeComponent();
            // this.Refresh();
        }
    }
}
