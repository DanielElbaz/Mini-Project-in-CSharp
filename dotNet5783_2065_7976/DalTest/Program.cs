using DO;
using Dal;
using System;

namespace DalTest
{
    class Program
    {
        private static DalOrder dalOrder = new DalOrder();
        private static DalOrderItem dalOrderItem = new DalOrderItem();
        private static DalProduct dalProduct = new DalProduct();

        static void Main(string[] args)
        {
            choice ch;
            // char ch;

            do
            {
                Console.WriteLine("enter \n" + 
                                   "1 for product, \n" +
                                   "2 for order, \n" +
                                   "3 for order item, \n" +
                                   "0 for exit \n" );
                while (!Enum.TryParse(Console.ReadLine(), out ch))
                {
                    Console.WriteLine("Wrong Input!");
                }

                switch (ch)
                {

                    case choice.product:
                        {
                            MainProduct();
                        }
                        break;

                    case choice.order:
                        {
                            MainOrder();
                        }
                        break;

                    case choice.orderItem:
                        {
                            Console.WriteLine("What would you like to do? \n" +
                                         "a- Add a new order item.\n" +
                                         "b- View a single order item.\n" +
                                         "c- View the order item list.\n" +
                                         "d- Update an order item.\n"+
                                         "e- Delete an order item.\n");
                        }
                        break;
                }


            } while (ch != choice.exit);




        }
        public static void MainProduct()
        {
                             Console.WriteLine("What would you like to do? \n" +
                                         "a- Add a new product.\n" +
                                         "b- View a single product.\n" +
                                         "c- View the Product list.\n" +
                                         "d- Update a product.\n"+
                                         "e- Delete a product.\n");
            
            char c = (char)System.Console.Read();

            switch(c)
            {
                case 'a':
                    {
                        int id, price, stock;
                        // string? category = "Phones"; 
                        Categories category;
                        string name,id1;
                        Console.WriteLine("enter id,  name, category, price and in stock \n");

                        
                        name =Console.ReadLine();
                        
                        id1 = Console.ReadLine();
                        int.TryParse(id1, out id);
                        category = (Categories)int.Parse(Console.ReadLine());                       
                       // price = Console.Read();
                       // stock = Console.Read();
                        int.TryParse(Console.ReadLine(), out price);
                        int.TryParse(Console.ReadLine(), out stock);

                        Product p = new Product();
                        p.ID = id;
                        p.Name = name?? "avi ";
                        p.Category = category;
                        // p.Category = (Categories)Enum.Parse(typeof(Categories), category); 
                        p.InStock = stock;
                        p.Price = price;

                        try { Console.WriteLine("successfully added product " + dalProduct.Add(p)); }
                        catch(Exception st)
                        {
                            Console.WriteLine(st);
                        }
                    }

                    break;

                case 'b':
                    {
                        int id;
                        Console.WriteLine("enter id of product to view");
                        int.TryParse(Console.ReadLine(), out id);
                        try
                        {
                            Product p = dalProduct.GetProduct(id);
                            Console.WriteLine(p);
                        }
                        catch (Exception st)
                        {
                            Console.WriteLine(st);
                        }


                    }
                    break;
                case 'c':
                    {
                        Product[] products = dalProduct.getAllProducts();
                        foreach (Product P in products)
                            Console.WriteLine(P + "\n");

                    }


                    break;
                case 'd':
                    {
                        try
                        {
                           
                            int id, price, stock, newId;
                            string? category = " "; string? name = " ";
                            Console.WriteLine("enter id of old product");
                            int.TryParse(Console.ReadLine(), out newId);
                            Console.WriteLine("enter of product to update: id, category, name, price and in stock \n");

                            int.TryParse(Console.ReadLine(), out id);
                            category = System.Console.ReadLine();
                            name = System.Console.ReadLine();
                            int.TryParse(Console.ReadLine(), out price);
                            int.TryParse(Console.ReadLine(), out stock);

                            Product p = new Product();
                            p.ID = id;
                            p.Name = name ?? "avi ";
                            p.Category = (Categories)Enum.Parse(typeof(Categories), category);
                            p.InStock = stock;
                            p.Price = price;
                            dalProduct.update(newId, p);
                            Console.WriteLine("successfully updated product number" + id);                            

                        }
                        catch(Exception str)
                        {
                            Console.WriteLine(str);
                        }
                    }
                    break;
                case 'e':
                    {
                        int id;
                        Console.WriteLine("enter id of product to delete");
                        id = Console.Read();
                        //int.TryParse(Console.ReadLine(), out id);

                        try
                        {
                            dalProduct.Delete(id);
                            Console.WriteLine(+id + "successfully deleted");
                        }
                        catch (Exception str)
                        { Console.WriteLine(str); }
                    }
                    break;
            }


        }
        public static void MainOrder()
        {
            Console.WriteLine("What would you like to do? \n" +
                                       "a- Add a new order.\n" +
                                       "b- View a single order.\n" +
                                       "c- View the orders list.\n" +
                                       "d- Update an order.\n" +
                                       "e- Delete an order.\n");

            char c = (char)System.Console.Read();

            switch (c)
            {

                case 'a':
                    {
                        Random rand = new Random();

                        int id;
                        string customerName, customerEmail, customerAddress;
                        DateTime orderDate, shipDate, deliveryDate;
                        Console.WriteLine("enter id,  customerName, customerEmail, customerAddress, \n");
                        id = Console.Read();
                        customerName = Console.ReadLine();
                        customerEmail = Console.ReadLine();
                        customerAddress = Console.ReadLine();
                        orderDate = DateTime.Now - new TimeSpan(rand.NextInt64(10L * 1000L * 3600L * 24L * 10L));
                        shipDate = DateTime.Now + new TimeSpan(rand.NextInt64(10L * 1000L * 3600L * 24L * 10L));
                        deliveryDate = DateTime.Now + new TimeSpan(rand.NextInt64(10L * 1000L * 3600L * 24L * 10L));
                       
                        Order order = new Order();
                        order.ID = id;
                        order.CustomerName = customerName;
                        order.CustomerEmail = customerEmail;
                        order.CustomerAdress = customerAddress;
                        order.OrderDate = orderDate;
                        order.ShipDate = shipDate;
                        order.DeliveryDate = deliveryDate;
                        try { Console.WriteLine("successfully added product " + dalOrder.Add(order)); }
                        catch (Exception st)
                        {
                            Console.WriteLine(st);
                        }




                    }
                    break;
                case 'b':
                    {

                        int id;
                        Console.WriteLine("enter id of order to view");
                        int.TryParse(Console.ReadLine(), out id);
                        try
                        {
                            Order order = dalOrder.getOneOrder (id);
                            Console.WriteLine(order);
                        }
                        catch (Exception st)
                        {
                            Console.WriteLine(st);
                        }
                    }
                    break;
                case 'c':
                    {
                        Order[] orders = dalOrder.getAllOrders();
                        foreach (Order order in orders)
                            Console.WriteLine(order + "\n");
                    }
                    break;
                case 'd':
                    {
                    }
                    break;
                case 'e':
                    {
                    }
                    break;
            }
        }

                    public static void MainOrderItem()
        {

        }


    }
}
