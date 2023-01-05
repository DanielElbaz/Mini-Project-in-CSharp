using BlApi;
using BlImplementation;
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

namespace PL.Products
{
    /// <summary>
    /// Interaction logic for ProductForListWindow.xaml
    /// </summary>
    public partial class ProductForListWindow : Window
    {
        private IBl bl = new Bl();
        public ProductForListWindow()
        {
            InitializeComponent();
            this.categoryList.ItemsSource = Enum.GetValues(typeof(BO.Category));
            this.categoryList.SelectedIndex = 0;
           this.ProductListView.ItemsSource = bl.Product.GetAll();

        }

        private void ProductListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //ProductListView.ItemsSource = bl.Product.GetAll();

        }

        private void categoryList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BO.Category category = (BO.Category)categoryList.SelectedItem ;
            categoryList.Focus();
            ProductListView.ItemsSource = bl.Product.GetAll( elem => elem.Category== category);
           // productList.SelectedItem =

        }

        private void addProductBtn_Click(object sender, RoutedEventArgs e) => new ProductWindow().Show();

        private void ProductListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            BO.ProductForList p = (BO.ProductForList)ProductListView.SelectedItem;
            int id = p.ProductID;
            new ProductWindow(id).Show();
            InitializeComponent();
           // this.Refresh();
        }
    }
}
