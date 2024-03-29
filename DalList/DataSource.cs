﻿
using DO;
using DalApi;

namespace Dal
{

    internal static class DataSource
    {
        internal static readonly int RandomNum = 1000;
        internal class Config
        {

            static internal Random rand = new Random();
            internal static int OrderLastId = 1; // running index number of order
            internal static int OrderItemLastId = 1;

            internal static int getOrderLastId()
            {
                return OrderLastId++;
            }
            internal static int getOrderItemLastId()
            {
                return OrderItemLastId++;
            }

        }

        static DataSource()
        {
            s_Initialize();

        }



        internal static List<OrderItem?> OrderItemDataList = new();
        internal static List<Product?> ProductDataList = new();
        internal static List<Order?> OrderDataList = new();

        private static void addOrderItem() // initialize order item
        {

            for (int i = 1; i <= 40; i++)
            {
                int index = Config.rand.Next(ProductDataList.Count);
                Product p = (Product)ProductDataList[index]!; // draw of any product randomally

                OrderItem oi = new OrderItem()
                {
                    ID = Config.rand.Next(100000, 999999),
                    ProductID = p.ID,// get a random id of an existing product
                    OrderID = ((Order)OrderDataList[Config.rand.Next(OrderDataList.Count)]!).ID, // random order id from the order list
                    Amount = Config.rand.Next(1, 5),
                    Price = p.Price
                };
                OrderItemDataList.Add(oi);
                // i++;
                // Config.OrderItemFirstClear++;

            }
        }
        private static void addOrder() // initialize order
        {

            String[] emails = new String[] { "Whitneytense@wanadoo.fr", "angryAlberto36@earthlink.net","Luismysterious@yahoo.com",
            "jitteryKurt47@yahoo.ca","grotesqueElizabeth21@msn.com" ,"splendidCrystal@aim.com",
            "handsomeLatoya2@live.com","Mirandagrotesque@me.com" ,"exuberantRandi@aliceadsl.fr"
            ,"preciousDana57@frontiernet.net" ,"clumsyAshlee41@me.com" ,"lonelyRuben@wanadoo.fr" , "Lucasfoolish@yahoo.com.sg",
             "zanyTrevor71@t-online.de","curiousKatie@blueyonder.co.uk","Ramonelated@gmx.net","helplessNathan@yahoo.co.id","Latoyadrab@outlook.com","illLawrence@skynet.be","famousGrace@live.com.au"};

            for (int i = 1; i <= 20; i++)
            {
                Order o = new Order();
                o = new Order();
                o.ID = Config.OrderLastId++;
                o.CustomerName = "Customer " + i;
                o.CustomerEmail = emails[Config.rand.Next(20)];
                o.OrderDate = DateTime.Now - new TimeSpan(Config.rand.NextInt64(10L * 1000L * 3600L * 24L * 10L));
                o.CustomerAddress = i + "/" + 2 * i + "begin road jerusalem";
                o.ShipDate = DateTime.Now + new TimeSpan(Config.rand.NextInt64(10L * 1000L * 3600L * 24L * 10L));
                o.DeliveryDate = DateTime.Now + 2 * new TimeSpan(Config.rand.NextInt64(10L * 1000L * 3600L * 24L * 10L));
                // i++;
                OrderDataList.Add(o);
                //Config.OrderFirstClear++;
            }
        }
        private static void addProduct() // add 10 products
        {
            String[] ProductNames = new String[] { "iPhone 11 ", "Galaxy S5", "Galaxy S6", "Galaxy S7", "Galaxy S8", "Galaxy S9", "Galaxy S10", "iPhone 12 " };



            for (int i = 1; i <= 10; i++)
            {
                Product p = new Product();

                p.ID = Config.rand.Next(100000, 999999);
               
                foreach (Product pr in ProductDataList)
                    if (p.ID == pr.ID)
                        p.ID = Config.rand.Next(100000, 999999);

                p.ID = Config.rand.Next(100000, 999999);
                p.Category = (DO.Category)(Config.rand.Next(0, 4));
                p.Name = ProductNames[Config.rand.Next(0, 7)];
                p.Price = Config.rand.Next(2000, 5000);
                p.InStock = Config.rand.Next(0, 20);
                //i++;
                ProductDataList.Add(p);
                //Config.ProductFirstClear++;

            }
        }
        private static void s_Initialize()
        {
            addProduct();
            addOrder();
            addOrderItem();
        }

    }
}