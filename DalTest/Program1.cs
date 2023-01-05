using DO;
using System.Security.Cryptography.X509Certificates;
using DalApi;
using System.Diagnostics;
using Dal;

namespace DalTest
{
   internal class Program1
    {
        public static DalApi.IDal? dal = DalApi.Factory.Get();

        //public static IDal dal = DalList.Instance;
        //private static dal.Order dal.Order = new dal.Order();
        //  private static dal.OrderItem dal.OrderItem = new dal.OrderItem();
        //   private static dal.Product dal.Product = new dal.Product();

        static void Main(string[] args)
        {
            choice ch;
            do
            {
                Console.WriteLine("Hi, please press: \n" +
                                   "1: For product. \n" +
                                   "2: For order. \n" +
                                   "3: For order item. \n" +
                                   "0: For exit: \n");
                /*while (!*/
                string? input = Console.ReadLine();
                bool valid = Enum.TryParse(input, out ch);
                               if (!valid)
                {
                    Console.WriteLine("Error ");
                    continue;
                }

                switch (ch)
                {
                    case choice.product:                   
                          MainProduct();                        
                        break;

                    case choice.order:                        
                           MainOrder();
                              break;

                    case choice.orderItem:
                             MainOrderItem();
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
                        "d- Update a product.\n" +
                        "e- Delete a product.\n");

            char c = Char.Parse(Console.ReadLine()!);

            switch (c)
            {
                case 'a':
                    {
                        int id, price, stock;
                        // string? category = "Phones"; 
                        Category category;
                        string? name, id1;
                        Console.WriteLine("enter id,  name, category (0-4), price and in stock \n");                        

                        id1 = Console.ReadLine();
                        int.TryParse(id1, out id);
                        //int.TryParse(Console.ReadLine(), out id);
                        name = Console.ReadLine();

                        category = (Category)int.Parse(Console.ReadLine()!);
                        // price = Console.Read();
                        // stock = Console.Read();
                        int.TryParse(Console.ReadLine(), out price);
                        int.TryParse(Console.ReadLine(), out stock);

                        Product p = new Product();
                        p.ID = id;
                        p.Name = name ?? "avi ";
                        p.Category = category;
                        // p.Category = (Categories)Enum.Parse(typeof(Categories), category); 
                        p.InStock = stock;
                        p.Price = price;

                        try { Console.WriteLine("successfully added product " + dal?.Product.Add(p)); }
                        catch (Exception st)
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
                            Product p = dal!.Product.GetByID(id);
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
                        IEnumerable<Product?> products =dal?.Product.GetAll()!;
                        foreach (Product? P in products)
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
                            p.Category = (Category)Enum.Parse(typeof(Category), category!);
                            p.InStock = stock;
                            p.Price = price;
                            dal?.Product.Update(newId, p);
                            Console.WriteLine("successfully updated product number" + id);

                        }
                        catch (Exception str)
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
                            dal?.Product.Delete(id);
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

            char c = Char.Parse(Console.ReadLine()!);

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
                        customerName = Console.ReadLine()!;
                        customerEmail = Console.ReadLine()!;
                        customerAddress = Console.ReadLine()!;
                        orderDate = DateTime.Now - new TimeSpan(rand.NextInt64(10L * 1000L * 3600L * 24L * 10L));
                        shipDate = DateTime.Now + new TimeSpan(rand.NextInt64(10L * 1000L * 3600L * 24L * 10L));
                        deliveryDate = DateTime.Now + new TimeSpan(rand.NextInt64(10L * 1000L * 3600L * 24L * 10L));

                        Order order = new Order();
                        order.ID = id;
                        order.CustomerName = customerName;
                        order.CustomerEmail = customerEmail;
                        order.CustomerAddress = customerAddress;
                        order.OrderDate = orderDate;
                        order.ShipDate = shipDate;
                        order.DeliveryDate = deliveryDate;
                        try { Console.WriteLine("successfully added product " + dal!.Order.Add(order)); }
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
                            Order order = dal!.Order.GetByID(id);
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
                        IEnumerable<Order?> orders = dal!.Order.GetAll();
                        foreach (Order? order in orders)
                            Console.WriteLine(order + "\n");
                    }
                    break;
                case 'd':
                    {
                        Random rand = new Random();
                        int oldId, newId;
                        string customerName, customerEmail, customerAddress;
                        DateTime orderDate, shipDate, deliveryDate;
                        Console.WriteLine("enter id of old order");
                        int.TryParse(Console.ReadLine(), out oldId);
                        Console.WriteLine("enter of product to update: id, customerName, customerEmail, customerAddress \n");
                        newId = Console.Read();
                        customerName = Console.ReadLine()!;
                        customerEmail = Console.ReadLine()!;
                        customerAddress = Console.ReadLine()!;
                        orderDate = DateTime.Now - new TimeSpan(rand.NextInt64(10L * 1000L * 3600L * 24L * 10L));
                        shipDate = DateTime.Now + new TimeSpan(rand.NextInt64(10L * 1000L * 3600L * 24L * 10L));
                        deliveryDate = DateTime.Now + new TimeSpan(rand.NextInt64(10L * 1000L * 3600L * 24L * 10L));

                        Order order = new Order();
                        order.ID = newId;
                        order.CustomerName = customerName;
                        order.CustomerEmail = customerEmail;
                        order.CustomerAddress = customerAddress;
                        order.OrderDate = orderDate;
                        order.ShipDate = shipDate;
                        order.DeliveryDate = deliveryDate;
                        try
                        {
                            dal?.Order.Update(oldId, order);
                            Console.WriteLine("successfully updated order number" + oldId);
                        }

                        catch (Exception str)
                        {
                            Console.WriteLine(str);
                        }

                    }
                    break;
                case 'e':
                    {                       
                            int id;
                            Console.WriteLine("enter id of order to delete");
                            id = Console.Read();
                            try
                            {
                                dal?.Order.Delete(id);
                                Console.WriteLine(+id + "successfully deleted");
                            }
                            catch (Exception str)
                            { Console.WriteLine(str); }                      

                    }
                    break;
            }
        }

        public static void MainOrderItem()
        {
            Console.WriteLine("What would you like to do? \n" +
                                         "a- Add a new order item.\n" +
                                         "b- View a single order item.\n" +
                                         "c- View the order item list.\n" +
                                         "d- Update an order item.\n" +
                                         "e- Delete an order item.\n");
            char c = Char.Parse(Console.ReadLine()!);

            switch (c)

            {
                case 'a':
                    {

                        int id, orderId, productId, price, amount;                       
                        Console.WriteLine("enter id,  customerName, customerEmail, customerAddress, \n");
                        id = Console.Read();
                        orderId = Console.Read();
                        productId = Console.Read();
                        price = Console.Read();
                        amount = Console.Read();

                        OrderItem orderItem = new OrderItem();
                        orderItem.ID = id;
                        orderItem.OrderID = orderId;
                        orderItem.ProductID = productId;
                        orderItem.Price = price;
                        orderItem.Amount = amount;

                        try { Console.WriteLine("successfully added orderItem " + dal!.OrderItem.Add(orderItem)); }
                        catch (Exception st)
                        {
                            Console.WriteLine(st);
                        }
                    }
                    break;
                case 'b':
                    {

                        int id;
                        Console.WriteLine("enter id of orderItem to view");
                        int.TryParse(Console.ReadLine(), out id);
                        try
                        {
                            OrderItem orderItem = dal!.OrderItem.GetByID(id);
                            Console.WriteLine(orderItem);
                        }
                        catch (Exception st)
                        {
                            Console.WriteLine(st);
                        }

                    }
                    break;

                case 'c':
                    {
                        IEnumerable<OrderItem?> orderItems = dal!.OrderItem.GetAll();
                        foreach (OrderItem? orderItem in orderItems)
                            Console.WriteLine(orderItem + "\n");

                    }
                    break;
                case 'd':
                    {
                        int oldId, id, orderId, productId, price, amount;
                        Console.WriteLine("enter old Id");
                        oldId = Console.Read();

                        Console.WriteLine("enter id,  customerName, customerEmail, customerAddress, \n");
                        id = Console.Read();
                        orderId = Console.Read();
                        productId = Console.Read();
                        price = Console.Read();
                        amount = Console.Read();

                        OrderItem orderItem = new OrderItem();
                        orderItem.ID = id;
                        orderItem.OrderID = orderId;
                        orderItem.ProductID = productId;
                        orderItem.Price = price;
                        orderItem.Amount = amount;

                        try {

                        dal?.OrderItem.Update(oldId, orderItem);
                        Console.WriteLine("successfully added orderItem " + oldId ); }
                        catch (Exception st)
                        {
                            Console.WriteLine(st);
                        }
                    }
                    break;
                case 'e':
                    {
                        int id;
                        Console.WriteLine("enter id of orderItem to delete");
                        id = Console.Read();
                        //int.TryParse(Console.ReadLine(), out id);

                        try
                        {
                            dal?.OrderItem.Delete(id);
                            Console.WriteLine("order item"+id + " successfully deleted");
                        }
                        catch (Exception str)
                        { Console.WriteLine(str); }

                    }
                    break;
            }
           }
        }
    }
