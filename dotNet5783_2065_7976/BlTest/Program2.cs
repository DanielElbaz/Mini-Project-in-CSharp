using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlApi;
using DO;
using BO;
using System.Security.Cryptography.X509Certificates;
using BlImplementation;
using DalApi;
using System.Diagnostics;

namespace BlTest
{
    internal class Program2
    {
       private static IBl bl = new Bl();

        public bool check(int num)
        {
            bool isint = (num % 1 == 0);
            return isint;
        }

        static void Main(string[] args)
        {
          
            choice ch;

            // char ch;

            do
            {
                Console.WriteLine("Hi, please press: \n" +
                                   "1: For product. \n" +
                                   "2: For order. \n" +
                                   "3: For order item. \n" +
                                   "0: For exit: \n");
                /*while (!*/
                Enum.TryParse(Console.ReadLine(), out ch);
                //{
                //    Console.WriteLine("Wrong Input!");
                //}

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
                            MainOrderItem();
                        }
                        break;
                }

            } while (ch != choice.exit);
        }


        public static void MainProduct()
        {
            Console.WriteLine("What would you like to do? \n" +
                       "a- get a list of products.\n" +
                       "b- get a single product details.\n" +
                       "c- get a single product details from catalog.\n" +
                       "d- add a product.\n" +
                       "e- delete a product.\n" +
                       "f- update a product.\n");

            char c = (char)System.Console.Read();

            switch (c)
            {
                case 'a':
                    {
                        IEnumerable<BO.ProductForList> products;
                         products = bl.Product.GetAll();
                        
                        foreach( BO.ProductForList product in products)
                        {
                            Console.WriteLine(product );
                        }


                    }break;
                case 'b':
                    {
                        BO.Product product = new();
                       // string? val;
                        int id;
                        Console.WriteLine(" Enter id of a product \n");
                         id = (int)System.Console.Read();
                        //if (!)
                        try {product = bl.Product.GetProduct(5); }
                        catch (BO.invalidInputException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }

                        catch ( BO.MissingIDException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        Console.WriteLine(product);
;                    }
                    break;

                case 'c': // add 
                    {

                        List<BO.OrderItem> items = new();
                        BO.OrderItem item;
                        int productId;
                        string? email, name, address;
                        Console.WriteLine(" Enter id of a product \n");
                        productId = (int)System.Console.Read();

                        Console.WriteLine(" Enter details of cart: name, address, email,\n");
                        name = Console.ReadLine();
                        address = Console.ReadLine();
                        email = Console.ReadLine();

                        Console.WriteLine(" Enter details orderItems: item Id, product id, amount,\n");
                        item =




                        BO.Cart cart = new


                    }
                    break;


                    case 'd':
                    {
                        int id, inStock;
                        double price;
                        string? name;
                        BO.Category category;
                        Console.WriteLine(" Enter details of a product, id, name, price and amount in stock \n");
                        id = (int)System.Console.Read();
                        name = Console.ReadLine();
                        price = (double)System.Console.Read();
                        inStock = (int)System.Console.Read();
                        category = (BO.Category)System.Console.Read();

                        BO.Product product = new BO.Product()
                        {
                            ID = id,
                            Name = name,
                            Price = price,
                            Category = category,
                            InStock = inStock,
                        };

                        try
                        { bl.Product.AddProduct(product); }
                        catch (BO.invalidInputException ex)
                        { Console.WriteLine(ex.Message); }
                        

                    }
                    break;


                case 'e':// delete product
                    {
                        int id;
                        id = (int)Console.Read();
                        try { bl.Product.DeleteProduct(id); }
                        catch(BO.MissingIDException ex)
                        {
                            Console.WriteLine(ex);
                        }

                    }
                    break;
                case 'f': // update product
                    {
                        int id, inStock;
                        double price;
                        string? name;
                        BO.Category category;
                        Console.WriteLine(" Enter details of a product, id, name, price and amount in stock \n");
                        id = (int)System.Console.Read();
                        name = Console.ReadLine();
                        price = (double)System.Console.Read();
                        inStock = (int)System.Console.Read();
                        category = (BO.Category)System.Console.Read();

                        BO.Product product = new BO.Product()
                        {
                            ID = id,
                            Name = name,
                            Price = price,
                            Category = category,
                            InStock = inStock,
                        };

                        try
                        { bl.Product.UpdateProduct(product); }
                        catch (BO.MissingIDException ex)
                        { Console.WriteLine(ex.Message); }

                    }
                    break;
                    default:
                    {
                        Console.WriteLine("wrong input");
                        MainProduct();
                    }break;



            }
        }

        public static void MainOrder()
        {
        }
        public static void MainOrderItem()
        {
        }
    }
}