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
        public void MainProduct()
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
                    int id, price, stock;
                    Console.WriteLine("enter id, category, name, price and in stock \n");
                         id = Convert.ToInt32(Console.ReadLine());
                         string category = System.Console.ReadLine();
                         string n = System.Console.ReadLine();
                         price = Convert.ToInt32(Console.ReadLine());
                         stock = Convert.ToInt32(Console.ReadLine());

                    break;
            }


        }
        public void MainProduct();
        public void MainProduct();

    }
}
}