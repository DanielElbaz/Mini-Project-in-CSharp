using System;
namespace Targil0
{
    partial class Program
    {
        static void Main(string[] args)
        {
            Welcome2065();
            Welcome7976();
            Console.ReadKey();
        }

        static partial void Welcome7976();
        private static void Welcome2065()
        {
            Console.Write("Enter your name: ");
            string? user = Console.ReadLine();
            Console.Write("{0}, welcome to my first console application", user);
        }
    }
}