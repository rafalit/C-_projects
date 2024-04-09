using System;
using System.Threading;
using System.Collections.Generic;

namespace lab5 
{
    public class Program
    {
        public static void Main(string []args)
        {
            Console.WriteLine("Exercise 1");
            Exercise1 ex1 = new Exercise1(5, 5);
            ex1.Start();
        }
    }
}