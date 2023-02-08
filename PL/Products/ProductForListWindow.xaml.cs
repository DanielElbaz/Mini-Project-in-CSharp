//using BlApi;
//using BlImplementation;
using BO;
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

namespace PL.Products
{
    /// <summary>
    /// Interaction logic for ProductForListWindow.xaml
    /// </summary>
    public partial class ProductForListWindow : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();
        public static readonly DependencyProperty ProductsDependency = DependencyProperty.Register(nameof(Products), typeof(ObservableCollection<ProductForList?>), typeof(Window));

        public ObservableCollection<ProductForList?> Products
        {
            get => (ObservableCollection<ProductForList?>)GetValue(ProductsDependency);
            private set => SetValue(ProductsDependency, value);
        }
        public Category Category { get; set; } = Category.None;

        public Array Categories { get { return Enum.GetValues(typeof(BO.Category)); } }

        public ProductForListWindow()
        {
            InitializeComponent();
            var temp = bl?.Product.GetAll();
            Products = temp == null ? new() : new(temp);
            //InitializeComponent();
            //this.categoryList.ItemsSource = Enum.GetValues(typeof(BO.Category));
            //this.categoryList.SelectedIndex = 5;
            //this.ProductListView.ItemsSource = bl.Product.GetAll();

        }

        private void ProductListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //ProductListView.ItemsSource = bl.Product.GetAll();

        }

        private void categoryList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var temp = Category == BO.Category.None ?
            bl?.Product.GetAll() : bl?.Product.GetAll().Where(item => item!.Category == Category);
            Products = temp == null ? new() : new(temp);

        }

        private void addProductBtn_Click(object sender, RoutedEventArgs e) => new ProductWindow(0).Show();

        private void ProductListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            int id = ((ProductForList?)(sender as ListViewItem)?.DataContext)?.ProductID
          ?? throw new NullReferenceException("null event sender");
            new ProductWindow(id).ShowDialog();
            Products = new(from p in bl?.Product.GetAll().OrderBy(p => p!.ProductID) select p);

          
        }
    }
}
