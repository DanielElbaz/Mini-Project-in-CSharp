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
using Dal;

namespace BlTest
{
    internal class Program2
    {
        private static IBl bl = new Bl();
        public static IDal dal = DalList.Instance;

        static BO.Cart cart = new()
        {
            CustomerName = "unknown",
            CustomerEmail = "unknown",
            CustomerAddress = "unknown",
            Items = null,
            TotalPrice = 0,

        };

        //public bool check(int num)
        //{
        //    bool isint = (num % 1 == 0);
        //    return isint;
        //}

        static void Main(string[] args)
        {


            choice ch;

            // char ch;

            do
            {
                Console.WriteLine("Hi, please press: \n" +
                                   "1: For product. \n" +
                                   "2: For order. \n" +
                                   "3: For cart. \n" +
                                   "0: For exit: \n");
                /*while (!*/
                string input = Console.ReadLine();
                bool valid = Enum.TryParse(input, out ch);
                //{
                //    Console.WriteLine("Wrong Input!");
                //}
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
                        MainCart();
                        break;
                }

            } while (ch != choice.exit);
        }


        public static void MainProduct()
        {
            Console.WriteLine("\nWhat would you like to do? \n" +
                       "a- get a list of products.\n" +
                       "b- get a single product details.\n" +
                       "c- get a single product details from catalog.\n" +
                       "d- add a product.\n" +
                       "e- delete a product.\n" +
                       "f- update a product.\n" +
                       "x- main menu.\n");


            char c = Char.Parse(Console.ReadLine());

            switch (c)
            {
                case 'a': // product list
                    {
                        IEnumerable<BO.ProductForList> products;
                        products = bl.Product.GetAll();

                        foreach (BO.ProductForList product in products)
                        {
                            Console.WriteLine(product);
                        }
                       // MainProduct();
                    }

                    break;
                case 'b': // product details
                    {
                        BO.Product product = new();
                        // string? val;
                        int id;
                        Console.WriteLine(" Enter id of a product \n");
                        id = Int32.Parse(Console.ReadLine());
                        //if (!)
                        try {
                            product = bl.Product.GetProduct(id);
                            Console.WriteLine(product);
                        }
                        catch (BO.invalidInputException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }

                        catch (BO.MissingIDException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        
                       // MainProduct();
                    }
                    break;

                case 'c': // product details for catalog
                    {

                        //List<BO.OrderItem> items = new();
                        //BO.OrderItem item;
                        int productId;
                        BO.ProductItem productItem = new();
                        // string? email, name, address;
                        Console.WriteLine(" Enter id of a product \n");
                        productId = Int32.Parse(Console.ReadLine());
                        try { productItem = bl.Product.GetProductForCatalog(productId, cart);
                            Console.WriteLine(productItem);
                        }
                        catch (BO.invalidInputException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        catch (BO.MissingIDException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }

                        //Console.WriteLine(" Enter details of cart: name, address, email,\n");
                        //name = Console.ReadLine();
                        //address = Console.ReadLine();
                        //email = Console.ReadLine();
                        // Console.WriteLine(productItem);
                        //MainProduct();

                    }
                    break;


                case 'd': //add product
                    {
                        int id, inStock;
                        double price;
                        string? name;
                        BO.Category category;
                        Console.WriteLine(" Enter id \n");
                        id = Int32.Parse(Console.ReadLine());
                        Console.WriteLine(" Enter name \n");
                        name = Console.ReadLine();
                        Console.WriteLine(" Enter  price \n");
                        price = Double.Parse(Console.ReadLine());
                        Console.WriteLine(" Enter amount in stock \n");
                        inStock = Int32.Parse(Console.ReadLine());
                        Console.WriteLine(" Enter category: 0 for phone\n" +
                                           "1 for computers\n" +
                                           "2 for Tablets\n" +
                                           "3 for Earphones\n"+
                                           "4 for gameplay \n");
                        string input = Console.ReadLine();
                        bool valid = Enum.TryParse(input, out category);
                        //category = (BO.Category)System.Console.Read();
                        if(valid) { category = (BO.Category)category; }
                        else {                            
                                Console.WriteLine("Error ");                                                       
                        }
                        BO.Product product = new BO.Product()
                        {
                            ID = id,
                            Name = name,
                            Price = price,
                            Category = category,
                            InStock = inStock,
                        };

                        try
                        { bl.Product.AddProduct(product);
                            Console.WriteLine( " product {0} addedd successfully {1}", product.ID, product);
                        }
                        catch (BO.invalidInputException ex)
                        { Console.WriteLine(ex.Message); }
                        //MainProduct();
                    }
                    break;


                case 'e':// delete product
                    {
                        int id;
                        Console.WriteLine(" Enter id \n");
                        id = Int32.Parse(Console.ReadLine());
                        try {
                            bl.Product.DeleteProduct(id);
                            Console.WriteLine(" product {0} removed successfully", id);
                        }
                        catch (BO.DuplicateIDException ex)
                        {
                            Console.WriteLine(ex);
                        }
                        catch (BO.MissingIDException ex)
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
                        Console.WriteLine(" Enter id of product to update\n");
                        id = Int32.Parse(Console.ReadLine());
                        Console.WriteLine(" Enter name \n");
                        name = Console.ReadLine();
                        Console.WriteLine(" Enter  price \n");
                        price = Double.Parse(Console.ReadLine());
                        Console.WriteLine(" Enter amount in stock \n");
                        inStock = Int32.Parse(Console.ReadLine());
                        Console.WriteLine(" Enter category: 0 for phone\n" +
                                           "1 for computers\n" +
                                           "2 for Tablets\n" +
                                           "3 for Earphones\n" +
                                           "4 for gameplay \n");
                        string input = Console.ReadLine();
                        bool valid = Enum.TryParse(input, out category);
                        //category = (BO.Category)System.Console.Read();
                        if (valid) { category = (BO.Category)category; }
                        else
                        {
                            Console.WriteLine("Error ");
                        }
                        BO.Product product = new BO.Product()
                        {
                            ID = id,
                            Name = name,
                            Price = price,
                            Category = category,
                            InStock = inStock,
                        };

                        try
                        { bl.Product.UpdateProduct(product);
                            Console.WriteLine(" product {0} updated successfully {1}", product.ID, product);
                        }
                        catch (BO.MissingIDException ex)
                        { Console.WriteLine(ex.Message); }
                        //MainProduct();
                    }
                    break;

                case 'x':
                    break;
                default:
                    {
                        Console.WriteLine("wrong input");
                        MainProduct();
                    }
                    break;
            }
        }

        public static void MainOrder()
        {

            Console.WriteLine("\nWhat would you like to do? \n" +
                      "a- get a list of orders.\n" +
                      "b- get a single order details.\n" +
                      "c- update an order that was sent.\n" +
                      "d- update an order that was delivered.\n" +
                      "e- track an order.\n" +
                      "x - main menu.\n");

            char c = Char.Parse(Console.ReadLine());
            switch (c)
            {
                case 'a': // get list 
                    {
                        IEnumerable<BO.OrderForList> orders;
                        orders = bl.Order.GetOrders();

                        foreach (BO.OrderForList order in orders)
                        {
                            Console.WriteLine(order);
                        }

                    }
                    break;

                case 'b': //get order
                    {
                        BO.Order order = new();
                        int id;
                        Console.WriteLine(" Enter id of a order \n");
                        id = Int32.Parse(Console.ReadLine());
                        //if (!)
                        try { 
                            order = bl.Order.GetOrder(id);
                            Console.WriteLine(order);
                        }
                        catch (BO.invalidInputException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }

                        catch (BO.MissingIDException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }                    

                    }
                    break;

                case 'c'://update order sent
                    {
                        int id;
                        BO.Order order = new();
                        Console.WriteLine(" Enter id of a order \n");
                        id = Int32.Parse(Console.ReadLine());
                        try {
                            order = bl.Order.UpdateOrderSent(id);
                            Console.WriteLine(order);
                        }
                        catch (BO.MissingIDException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        catch (BO.invalidInputException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }                       
                    }
                    break;
                case 'd':// update order delivered
                    {
                        int id;
                        BO.Order order = new();
                        Console.WriteLine(" Enter id of a order \n");
                        id = Int32.Parse(Console.ReadLine());
                        try {
                            order = bl.Order.UpdateOrderSupply(id);
                            Console.WriteLine(order);
                        }
                        catch (BO.MissingIDException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        catch (BO.invalidInputException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                       
                    }
                    break;

                case 'e': //track order
                    {
                        int id;

                        BO.OrderTracking ot = new();
                        Console.WriteLine(" Enter id of a order \n");
                        id = Int32.Parse(Console.ReadLine());
                        try { ot = bl.Order.OrderTracking(id); }
                        catch (BO.MissingIDException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }

                        Console.WriteLine(ot);
                    }
                    break;
                case 'x': // main menu
                    break;

                default:
                    {
                        Console.WriteLine("wrong input");
                        MainOrder();
                    }
                    break;
            }
        }
        public static void MainCart()
        {

            Console.WriteLine("\nWhat would you like to do? \n" +
            "a- add product to cart.\n" +
            "b- update amount of product.\n" +
            "c- confirm cart.\n"+
            "x- main menu.\n");


            char c = Char.Parse(Console.ReadLine());


            
            switch (c)
            {
                case 'a': // add a product to the cart
                    {
                        int id;
                        DO.Product product = new();
                        Console.WriteLine(" Enter id of a product \n");
                        id = Int32.Parse(Console.ReadLine());
                        try
                        { cart = bl.Cart.AddProduct(cart, id);
                             product = dal.Product.GetByID(id);
                            Console.WriteLine("product added successfully to your cart\n" + cart );
                        }
                        catch (BO.incorrectDataException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        catch (BO.MissingIDException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }

                       MainCart();
                    }
                    break;
                case 'b': // update amount of product in the cart
                    {
                        int id;
                        int amount;
                        Console.WriteLine("Enter id of product \n");
                        id = Int32.Parse(Console.ReadLine());

                        Console.WriteLine("Enter a new amount of product \n");
                        amount = Int32.Parse(Console.ReadLine());
                        try { 
                            cart = bl.Cart.UpdateAmountOfProduct(cart, id, amount);
                            Console.WriteLine("Amount updated succesfully \n");
                            Console.WriteLine(cart);
                        }
                        catch (BO.MissingIDException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        catch (BO.invalidInputException ex1)
                        { Console.WriteLine(ex1.Message); }
                       
                        MainCart();
                    }
                    break;
                case 'c':// confirm cart
                    {
                        string name;
                        string email_address;
                        string adress;
                        Console.WriteLine("Please write your name: \n");
                        name = Console.ReadLine();
                        Console.WriteLine("Please write your email adress: \n");
                        email_address = Console.ReadLine();
                        Console.WriteLine("Finally write your adress: \n");
                        adress = Console.ReadLine();
                        cart.CustomerName = name;
                        cart.CustomerEmail = email_address;
                        cart.CustomerAddress = adress;
                        try {
                            bl.Cart.ConfirmCart(cart);
                            Console.WriteLine(" Cart confirmed successfully\n" + cart);                           
                           // Console.WriteLine(cart);
                            cart = new();// reset cart
                        }
                        catch (BO.incorrectDataException ex) { Console.WriteLine(ex.Message); }
                        catch (BO.MissingIDException ex) { Console.WriteLine(ex.Message); }
                        catch (BO.DuplicateIDException ex) { Console.WriteLine(ex.Message); }
                        
                        MainCart();
                    }
                    break;

                case 'x': // main menu
                    break;

                default: //wrong input
                    {
                        Console.WriteLine("Wrong input");
                        MainCart();
                    } 
                    break;




            }
        }
    }
}
