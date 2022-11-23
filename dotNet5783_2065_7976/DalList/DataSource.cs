
using DO;

namespace Dal;

internal static class DataSource
{
    internal static readonly int RandomNum = 1000;
    internal class Config
    {
       
        internal static int OrderItemFirstClear = 0;
        internal static int ProductFirstClear = 0; // the first index in the array which is clear
        internal static int OrderFirstClear = 0;

        static internal Random rand = new Random();

       
        internal static int OrderLastId; // running index number of order
        internal static int getOrderLastId()
        {
            return OrderLastId++;
        }

    }

    static DataSource()
    {
        s_Initialize();

    }

        
        internal static int OI_capacity = 200;
        internal static int P_capacity = 50;
        internal static int O_capacity = 100;

    //internal static OrderItem[] OI_arr = new OrderItem[OI_capacity];
   internal static List<OrderItem> OI_list = new List<OrderItem>();
    //internal static Product[] P_arr = new Product[P_capacity];
   internal static List<Product> P_list = new List<Product>();
   //internal static Order[] O_arr = new Order[O_capacity];
   internal static List<Order> O_list = new List<Order>();

    private static void addOrderItem() // initialize order item
    {

        for (int i = 0; i < 40; i++)
        {
            int index = Config.rand.Next(P_list.Count);
            Product p = P_list[index]; // draw of any product randomally

            OrderItem oi = new OrderItem();
            oi.ID = Config.rand.Next(100000, 999999);
            oi.ProductID = p.ID;// get a random id of שמ existing product
            oi.OrderID = OI_list[Config.rand.Next(Config.OrderItemFirstClear)].ID;
            oi.Amount = Config.rand.Next(1, 5);
            oi.Price = p.Price;
            OI_list.Add(oi);
            i++;
            Config.OrderItemFirstClear++;

        }
    }
        private static void addOrder() // initialize order
    { 

        String[] emails = new String[] { "Whitneytense@wanadoo.fr", "angryAlberto36@earthlink.net","Luismysterious@yahoo.com",
            "jitteryKurt47@yahoo.ca","grotesqueElizabeth21@msn.com" ,"splendidCrystal@aim.com",
            "handsomeLatoya2@live.com","Mirandagrotesque@me.com" ,"exuberantRandi@aliceadsl.fr" 
            ,"preciousDana57@frontiernet.net" ,"clumsyAshlee41@me.com" ,"lonelyRuben@wanadoo.fr" , "Lucasfoolish@yahoo.com.sg",
             "zanyTrevor71@t-online.de","curiousKatie@blueyonder.co.uk","Ramonelated@gmx.net","helplessNathan@yahoo.co.id","Latoyadrab@outlook.com","illLawrence@skynet.be","famousGrace@live.com.au"};

        for (int i = 0; i < 20; i++)
        {
            Order o = new Order();
            o = new Order();
            o.ID = Config.OrderLastId; 
            o.CustomerEmail = emails[Config.rand.Next(20)] ;
            o.OrderDate = DateTime.Now - new TimeSpan(Config.rand.NextInt64(10L *1000L * 3600L * 24L *10L));
            o.CustomerAdress = i + "/" + 2 * i + "begin road jerusalem";
            o.ShipDate = DateTime.Now + new TimeSpan(Config.rand.NextInt64(10L * 1000L * 3600L * 24L * 10L));
            o.DeliveryDate = DateTime.Now + new TimeSpan(Config.rand.NextInt64(10L * 1000L * 3600L * 24L * 10L));
            i++;
            O_list.Add(o);
            Config.OrderFirstClear++;
        }
    }
        private static void addProduct() // add 10 products
    {
        String[] ProductNames = new String[] { "iPhone 11 ", "Galaxy S5", "Galaxy S6", "Galaxy S7", "Galaxy S8", "Galaxy S9", "Galaxy S10", "iPhone 12 " };
       


        for (int i = 0; i < 10; i++)
        {
            Product p = new Product();
            
            p.ID = Config.rand.Next(100000, 999999);
            //for (int j = 0; j< Config.ProductFirstClear; j++) //check if there is no other product with the same id
              //  while(P_arr[j].ID == P_arr[i].ID) // if theres product with same id
              foreach (Product pr in P_list )
                  if(p.ID == pr.ID)
                    p.ID = Config.rand.Next(100000, 999999);

            p.ID = Config.rand.Next(100000, 999999);
            p.Category = Categories.Phones;
            p.Name = ProductNames [Config.rand.Next(0,7 )];
            p.Price = Config.rand.Next(2000, 5000);
            p.InStock = Config.rand.Next(0, 20);
            i++;
            P_list.Add(p);
            Config.ProductFirstClear++;

         }
    }
        private static void s_Initialize()
        {
        addProduct();
        addOrder();        
        addOrderItem();
        }

    }
