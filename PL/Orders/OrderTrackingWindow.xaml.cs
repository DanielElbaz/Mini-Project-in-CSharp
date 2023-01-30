using BO;
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

namespace PL.Orders
{

   
    /// <summary>
    /// Interaction logic for OrderTrackingWindow.xaml
    /// </summary>
    /// 

    public partial class OrderTrackingWindow : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();

        public static DependencyProperty TrackOrderDependency = DependencyProperty.Register(nameof(BO.OrderTracking), typeof(OrderTracking), typeof(OrderTrackingWindow));
        public OrderTracking orderTracking
        {
            get => (OrderTracking)GetValue(TrackOrderDependency);
            private set => SetValue(TrackOrderDependency, value);
        }


        public OrderTrackingWindow()
        {
            
            InitializeComponent();
        }

        private void TrackSearch_Click(object sender, RoutedEventArgs e)
        {
            int id = 0;
            try
            {

                if (int.TryParse(idBox.Text, out id))

                {
                    orderTracking = bl!.Order.OrderTracking(id);
                    MessageBox.Show(orderTracking.ToString());
                }
                //orderTrackingListView.

                else
                    MessageBox.Show("invalid Input", " ", MessageBoxButton.OK, MessageBoxImage.Exclamation);

            }
            catch (BO.MissingIDException ex)
            {
                MessageBox.Show(ex.Message, " ", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void ShowOrder_Click(object sender, RoutedEventArgs e) => new OrderWindow(int.Parse(idBox.Text));
       
    }
}
