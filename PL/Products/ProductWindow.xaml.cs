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
    /// Interaction logic for ProductWindow.xaml
    /// </summary>
    public partial class ProductWindow : Window
    {
        private IBl bl = new Bl();
        public ProductWindow()
        {
            InitializeComponent();
            this.categoryList.ItemsSource = Enum.GetValues(typeof(BO.Category));
            this.categoryList.SelectedIndex = 0;
        }

        public ProductWindow(int id)
        {
            InitializeComponent();
            this.categoryList.ItemsSource = Enum.GetValues(typeof(BO.Category));
            BO.Product product = bl.Product.GetProduct(id);
            addButton.Content = "Update";
            this.id.Text = Convert.ToString(product.ID);
            this.id.IsEnabled = false;
            this.name.Text = Convert.ToString(product.Name);
            this.price.Text = Convert.ToString(product.Price);
            this.inStock.Text = Convert.ToString(product.InStock);
            this.categoryList.SelectedItem = product.Category;
        }

        private bool check(string id,string price,string instock)
        {
            int id1, instock1;
            double price1;
            return int.TryParse(id, out id1) && Double.TryParse(price, out price1) && int.TryParse(instock, out instock1);
        }
        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            BO.Product product = new();
            if (!check(id.Text, price.Text, inStock.Text))
            {
                MessageBox.Show("One or more of the inputs are invalid");
                //this.Close();
            }
            //int inStock;
            //double price;
            //string name;
            else
            {
                 product = new()
                {
                    ID = Int32.Parse(id.Text),
                    Name = name.Text,
                    Category = (BO.Category)categoryList.SelectedItem,
                    Price = Double.Parse(price.Text),
                    InStock = Int32.Parse(inStock.Text),

                };
            }
            // string id = id.Text;
            try { bl.Product.AddProduct(product); }
            catch (BO.invalidInputException ex)
            { MessageBox.Show(ex.Message);  }
            catch(BO.DuplicateIDException ex1)
            {
                bool updated = false;
                try
                {
                    bl.Product.UpdateProduct(product);
                    MessageBox.Show("The item has been updated");
                    updated = true; 
                    this.Close();
                }
                catch(BO.MissingIDException ex2)
                { 
                    MessageBox.Show(ex2.Message);
                    
                }
                if(!updated)
                  MessageBox.Show(ex1.Message); 
            }

           this.Close();
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e) => this.Close();
       
    }
}
