
using D0;

namespace Dal;

internal static class DataSource
{
    internal static readonly int RandomNum = 1000;
    internal class Config
    {
       
        internal static int OrderItemFirstClear = 0;
        internal static int ProductFirstClear = 0;
        internal static int OrderFirstClear = 0;

        static internal Random rand = new Random();

       
        internal static int OrderLastId;
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

        internal static OrderItem[] OI_arr = new OrderItem[OI_capacity];
        internal static Product[] P_arr = new Product[P_capacity];
        internal static Order[] O_arr = new Order[O_capacity];

        private static void addOrderItem()
        {
            for(int i = 0; i < 40; i++)
              {
            OI_arr[i] = new OrderItem();
            OI_arr[i].ID = Config.rand.Next(100000, 999999);
            OI_arr[i].ProductID = P_arr[Config.rand.Next(Config.ProductFirstClear)].ID;// get a randomal id of existing product
            OI_arr[i].OrderID = O_arr[Config.rand.Next(Config.OrderItemFirstClear)].ID;
            OI_arr[i].Amount = Config.rand.Next(1, 5);
            OI_arr[i].Price =   

            i++;

                  }
        }
        private static void addOrder() {

        String[] emails = new String[] { "Whitneytense@wanadoo.fr", "angryAlberto36@earthlink.net","Luismysterious@yahoo.com",
            "jitteryKurt47@yahoo.ca","grotesqueElizabeth21@msn.com" ,"splendidCrystal@aim.com",
            "handsomeLatoya2@live.com","Mirandagrotesque@me.com" ,"exuberantRandi@aliceadsl.fr" 
            ,"preciousDana57@frontiernet.net" ,"clumsyAshlee41@me.com" ,"lonelyRuben@wanadoo.fr" , "Lucasfoolish@yahoo.com.sg",
             "zanyTrevor71@t-online.de","curiousKatie@blueyonder.co.uk","Ramonelated@gmx.net","helplessNathan@yahoo.co.id","Latoyadrab@outlook.com","illLawrence@skynet.be","famousGrace@live.com.au"};

        for (int i = 0; i < 20; i++)
        {

            O_arr[i] = new Order();
            O_arr[i].ID = Config.OrderLastId; 
            O_arr[i].CustomerEmail = emails[Config.rand.Next(20)] ;
            O_arr[i].OrderDate = DateTime.Now - new TimeSpan(Config.rand.NextInt64(10L *1000L * 3600L * 24L *10L));
            O_arr[i].CustomerAdress = i + "/" + 2 * i + "begin street jerusalem";
            O_arr[i].ShipDate = DateTime.Now + new TimeSpan(Config.rand.NextInt64(10L * 1000L * 3600L * 24L * 10L));
            O_arr[i].DeliveryDate = DateTime.Now + new TimeSpan(Config.rand.NextInt64(10L * 1000L * 3600L * 24L * 10L));
            i++;

        }
    }
        private static void addProduct() // add 10 products
    {
        String[] ProdactNames = new String[] { "iphone 11 ", "galaxy s5", "galaxy s6", "galaxy s7", "galaxy s8", "galaxy s9", "galaxy s10", "iphone 12 " };
       


        for (int i = 0; i < 10; i++)
        {
            P_arr[i] = new Product();
            P_arr[i].ID = Config.rand.Next(100000, 999999);
            for (int j = 0; j< Config.ProductFirstClear; j++) //check if there is no other prodact with the same id
                while(P_arr[j].ID == P_arr[i].ID) // if theres prodact with same id
                    P_arr[i].ID = Config.rand.Next(100000, 999999);

            P_arr[i].ID = Config.rand.Next(100000, 999999);
            P_arr[i].Category = Categories.Phones;
            P_arr[i].Name = ProdactNames [Config.rand.Next(0,7 )];
            P_arr[i].Price = Config.rand.Next(2000, 5000);
            P_arr[i].InStock = Config.rand.Next(0, 20);
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

    }
