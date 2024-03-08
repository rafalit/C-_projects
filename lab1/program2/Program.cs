using System;
using System.IO;

class Program
{
    static void Main()
    {
        Funkcja("jed", "plik");
    }

    static void Funkcja(string szukany_wyraz, string wyraz)
    {
        using (StreamReader sr = new StreamReader($"{wyraz}.txt"))
        {
            int numerLinii = 0;

            while (!sr.EndOfStream)
            {
                numerLinii++;
                string napis = sr.ReadLine();

                if (napis.Contains(szukany_wyraz))
                {
                    int index = napis.IndexOf(szukany_wyraz);
                    Console.WriteLine($"linijka {numerLinii}, pozycja: {index}");
                }
            }
        }
    }
}
