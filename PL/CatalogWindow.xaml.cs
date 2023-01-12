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

        //public static readonly DependencyProperty ProductsDepedency = DependencyProperty.Register(nameof(Products), typeof(ObservableCollection<ProductForList?>), typeof(Window));
        //public ObservableCollection<ProductForList?> Products
        //{
        //    get => (ObservableCollection<ProductForList?>)GetValue(ProductsDepedency);
        //    private set => SetValue(ProductsDepedency, value);
        //}
        public CatalogWindow()
        {
           //var temp = bl.Product.GetAll();
           //Products = temp == null ? new() : new(temp);           
            InitializeComponent();
            this.categoryList.ItemsSource = Enum.GetValues(typeof(BO.Category));
            this.categoryList.SelectedIndex = 0;
            this.ProductItemListView.ItemsSource = bl.Product.GetAll();

        }
        private void ProductListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //ProductListView.ItemsSource = bl.Product.GetAll();

        }

        private void categoryList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BO.Category category = (BO.Category)categoryList.SelectedItem;
            categoryList.Focus();
            ProductItemListView.ItemsSource = bl.Product.GetAll(elem => elem.Category == category);
            // productList.SelectedItem =
        }
        private void addProductBtn_Click(object sender, RoutedEventArgs e) => new ProductWindow().Show();

        private void ProductItemView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            BO.ProductForList p = (BO.ProductForList)ProductItemListView.SelectedItem;
            int id = p.ProductID;
            new ProductWindow(id).Show();
            InitializeComponent();
            // this.Refresh();
        }
    }
}
