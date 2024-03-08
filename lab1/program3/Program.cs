using System;
using System.IO;

class Program
{
    static void Funkcja(string nazwa, int ilosc, int min, int max, int seed, bool czy_rzecz)
    {
        using (StreamWriter sw = new StreamWriter($"{nazwa}.txt"))
        {
            Random random = new Random(seed);

            for (int i = 0; i < ilosc; i++)
            {
                double losowaLiczba;

                if (czy_rzecz)
                {
                    losowaLiczba = random.NextDouble() * (max - min) + min;
                }
                else
                {
                    losowaLiczba = random.Next(min, max + 1);
                }
                sw.WriteLine(losowaLiczba);
            }

        }
    }

    static void Main(string[] args)
    {
        if (args.Length != 6)
        {
            Console.WriteLine("Nieprawidłowa liczba argumentów.");
            return;
        }

        string n = args[0];
        int i = int.Parse(args[1]);
        int x = int.Parse(args[2]);
        int y = int.Parse(args[3]);
        int s = int.Parse(args[4]);
        bool f = bool.Parse(args[5]);

        Funkcja(n, i, x, y, s, f);
    }
}
