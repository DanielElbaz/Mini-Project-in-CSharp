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
            this.combobox1.ItemsSource = Enum.GetValues(typeof(BO.Category));
           this.ProductListView.ItemsSource = bl.Product.GetAll();

        }

        private void ProductListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //ProductListView.ItemsSource = bl.Product.GetAll();

        }

        private void combobox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BO.Category category = (BO.Category)combobox1.SelectedItem ;
            ProductListView.ItemsSource = bl.Product.GetAll( elem => elem.Category== category);
           // combobox1.SelectedItem =

        }
    }
}
