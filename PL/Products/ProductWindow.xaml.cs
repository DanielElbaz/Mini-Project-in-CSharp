using BlApi;
using BO;
//using BlImplementation;
using System;
using System.Collections.Generic;
using System.Globalization;
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
        // private IBl bl = new Bl();
        BlApi.IBl? bl = BlApi.Factory.Get();

        public static readonly DependencyProperty ProductDependency = DependencyProperty.Register(nameof(Product), typeof(BO.Product), typeof(Window));
        public Product? Product { get => (Product)GetValue(ProductDependency); private set => SetValue(ProductDependency, value); }

        public Array Categories { get { return Enum.GetValues(typeof(BO.Category)); } }

        static internal Random rand = new Random();

        //public ProductWindow()
        //{
        //    InitializeComponent();
        //    this.categoryList.ItemsSource = Enum.GetValues(typeof(BO.Category));
        //    this.categoryList.SelectedIndex = 0;
        //}

        public ProductWindow(int id = 0)
        {
            try
            {
                
                Product = id == 0 ? new() {  Category = BO.Category.None } : bl.Product.GetProduct(id);
                InitializeComponent();
                Product.ID = (Product.ID == 0) ? rand.Next(100000, 999999) : id;
            }
            catch (MissingIDException ex)
            {
                Close();
                MessageBox.Show(ex.Message, "Failed to get the entity", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }
       

        private bool check(string id,string price,string instock, BO.Category category)
        {
            int id1, instock1;
            double price1;
            return int.TryParse(id, out id1) && Double.TryParse(price, out price1) && int.TryParse(instock, out instock1) &&(category!=BO.Category.None);
        }
        private void addButton_Click(object sender, RoutedEventArgs e)
        {

            //if (!check(id.Text, price.Text, inStock.Text, (BO.Category)categoryList.SelectedItem))            
            //    MessageBox.Show("One or more of the inputs are invalid");
              
            
           
            try
            {
                if (check(id.Text, price.Text, inStock.Text, (BO.Category)categoryList.SelectedItem))
                {
                    bl?.Product.AddProduct(Product);
                    MessageBox.Show("The item has been added");
                    this.Close();
                }
                else
                    MessageBox.Show("One or more of the inputs are invalid");
            }
            catch (BO.invalidInputException ex)
            { MessageBox.Show(ex.Message);  }
            catch(BO.DuplicateIDException ex1)
            {
                bool updated = false;
                try
                {
                    bl?.Product.UpdateProduct(Product);
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

          // this.Close();
        }

        

        private void UpdateButtonClick(object sender, RoutedEventArgs e)
        {

        }
    }
}
