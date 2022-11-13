using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;

namespace Dal;

internal static class DataSource
{
    //internal class Config
    //{
    //    internal static string ConfigPath { get; set; }   
    //}
    //internal static readonly int Rand{ get; } = 2650;
    internal static int OI_capacity = 200;
    internal static int P_capacity = 50;
    internal static int O_capacity = 100;

   internal static OrderItem[] OI_arr = new OrderItem[OI_capacity];
   internal static Product[] P_arr = new Product[P_capacity];
   internal static Order[] O_arr = new Order[O_capacity];

    private static void OI_Initialize()
    { for (int i = 0; i < OI_capacity; i++)
        {
           // OrderItem orderItem[] =;

          


         }
    }
}
