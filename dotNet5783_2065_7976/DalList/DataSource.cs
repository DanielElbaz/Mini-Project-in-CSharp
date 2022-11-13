using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;

namespace Dal;

internal static class DataSource
{
    internal class Config
    {
        // internal static string ConfigPath { get; set; }
        internal static int OrderItemFirstClear = 0;
        internal static int ProductFirstClear = 0;
        internal static int OrderFirstClear = 0;

        internal static int OrderItemLastId { get { return OrderItemLastId + 1; } } // last id of the item in the array
       // internal static int ProductLastId { get { return ProductLastId + 1; } }
        internal static int OrderLastId{ get { return OrderLastId + 1; } }

    }

    static DataSource()
    {
        s_Initialize();

    }

         static  Random rand = new Random();
        internal static int OI_capacity = 200;
        internal static int P_capacity = 50;
        internal static int O_capacity = 100;

        internal static OrderItem[] OI_arr = new OrderItem[OI_capacity];
        internal static Product[] P_arr = new Product[P_capacity];
        internal static Order[] O_arr = new Order[O_capacity];

        private static void addOrderItem()
        {
            for(int i = 0; i < 40; i++)
              {
            OI_arr[i] = new OrderItem();
            OI_arr[i].ID = Config.OrderItemLastId;
            OI_arr[i].OrderID = O_arr[rand.Next(0, 100)].ID; 


            i++;

                  }
        }
        private static void addOrder() {

        DateTime date1 = DateTime.now;
        for (int i = 0; i < 20; i++)
        {
            O_arr[i] = new Order();
            O_arr[i].ID = Config.OrderLastId; 
            O_arr[i].ShipDate = DateTime.MinValue;
            O_arr[i].OrderDate = O_arr[i].ShipDate - TimeSpan;


            i++;

        }
    }
        private static void addProduct()
    {
        for (int i = 0; i < 10; i++)
        {
            P_arr[i] = new Product();
            P_arr[i].ID = rand.Next(100000, 999999);
            for (int j = 0; j< Config.ProductLastId; j++) //check if there is no other prodact with the same id
                while(P_arr[j].ID == P_arr[i].ID)
                    P_arr[i].ID = rand.Next(100000, 999999);

            P_arr[i].ID = rand.Next(100000, 999999);
            P_arr[i].Category = rand.Next(0, 3);
            P_arr[i].Name = rand.Next(0, 26);
            P_arr[i].Price = rand.Next(200, 5000);
            P_arr[i].InStock = rand.Next(0, 20);
            i++;
            Config.ProductFirstClear++;

        }
    }
        private static void s_Initialize()
        {
        addProduct();
        addOrder();        
        addOrderItem();

        }


        //private static void OI_Initialize()
        //{ for (int i = 0; i < OI_capacity; i++)
        //    {
        //       // OrderItem orderItem[] =;




        //     }
        //}
    }
