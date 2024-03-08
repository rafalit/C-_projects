
using System;
using System.Collections.Generic;
using lab2;

class Program
{
    public static void Main(string [] args)
    {
        OsobaFizyczna osoba1 = new OsobaFizyczna("Mateusz", "Kowalski", "Jakub", "68542309102", "PL2412");
        OsobaFizyczna osoba2 = new OsobaFizyczna("Jarosław", "Kowalczyk", "Mirosław", "49021402321", "PL1902");
        OsobaFizyczna osoba3 = new OsobaFizyczna("Donald", "Nowak", "Oskar", "57920345812", "PL3621");
        OsobaFizyczna osoba4 = new OsobaFizyczna("Szymon", "Nowakowski", "Dawid", "76423680928", "PL4521");
        OsobaFizyczna osoba5 = new OsobaFizyczna("Marek", "Kruk", "", "64211809281", "PL4121");
        
        Console.WriteLine(osoba4);
        //osoba4.Pesel="1234";

        RachunekBankowy r1 = new RachunekBankowy("12345", 10000, false, new List<PosiadaczRachunku>{osoba1, osoba2, osoba5});
        RachunekBankowy r2 = new RachunekBankowy("23456", 27000, false, new List<PosiadaczRachunku>{osoba1, osoba3});

        System.Console.WriteLine(r1);
        RachunekBankowy.DokonajTransakcji(r1, r2, 9000, "Przelew");
        System.Console.WriteLine(r1);

        r1-=osoba5;
        System.Console.WriteLine(r1);
        r1+=osoba5;
        System.Console.WriteLine(r1);
    }
}