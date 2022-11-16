using D0;
using Dal;
using System;

namespace DalTest
{
    class Program
    {
        private static DalOrder order = new DalOrder();
        private static DalOrderItem orderItem = new DalOrderItem();
        private static DalProduct product = new DalProduct();

        static void Main(string[] args)
        {
            choice ch;
            // char ch;

            do
            {
                Console.WriteLine(@"enter 0 for exit,
                                   1 for product,
                                   2 for order,
                                   3 for order item ");
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
                            Console.WriteLine("What would you like to do? \n" +
                                         "a- Add a new order.\n" +
                                         "b- View a single order.\n" +
                                         "c- View the order list.\n" +
                                         "d- Update an order.\n"+
                                         "e- Delete an order.\n");
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
                        string? category; string? name = " ";
                        Console.WriteLine("enter id, category, name, price and in stock \n");

                        int.TryParse(Console.ReadLine(), out id);
                        category = System.Console.ReadLine();
                        name = System.Console.ReadLine();
                        int.TryParse(Console.ReadLine(), out price);
                        int.TryParse(Console.ReadLine(), out stock);

                        Product p = new Product();
                        p.ID = id;
                        p.Name = name?? "avi ";
                        p.Category = category;
                        p.InStock = stock;
                        p.Price = price;

                        try { Console.WriteLine("successfully added product " + product.Add(p)); }
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
                        Product p = product.GetProduct(id);
                        Console.WriteLine(p);


                    }
                    break;
                case 'c':
                    {
                        Product[] products = product.getAllProducts();
                        foreach (Product P in products)
                            Console.WriteLine(P + "\n");

                    }


                    break;
                case 'd':
                    {
                        try
                        {
                           
                            int id, price, stock, newId;
                            string? category; string? name = " ";
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
                            p.Category = category;
                            p.InStock = stock;
                            p.Price = price;
                            product.update(newId, p);
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
                        int.TryParse(Console.ReadLine(), out id);

                        try
                        {
                            product.Delete(id);
                            Console.WriteLine(+id + "successfully deleted");
                        }
                        catch (Exception str)
                        { Console.WriteLine(str); }
                    }
                    break;
            }


        }
        public void MainOrder()
        { }
        public void MainOrderItem()
        {

        }


    }
}
