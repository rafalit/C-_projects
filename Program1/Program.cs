using System;
using System.IO;

class Program
{
    static void Funkcja()
    {
        using (StreamWriter sw = new StreamWriter("plik.txt", append: true))
        {
            string slowo = "";
            string slowo_max = "";

            while (slowo != "koniec!")
            {
                Console.WriteLine("Podaj jakieś słowo (koniec! jeśli chcesz zakończyć program)");
                slowo = Console.ReadLine();

                if (ZawieraZnaki(slowo) && slowo!="koniec!")
                {
                    Console.WriteLine("Słowo nie może zawierać cyfr!");
                    continue;
                }

                sw.WriteLine(slowo);

                if (string.Compare(slowo_max, slowo) == -1 && slowo != "koniec!")
                {
                    slowo_max = slowo;
                }
            }
            Console.WriteLine("Maksymalne słowo leksykograficzne: {0}", slowo_max);
            sw.WriteLine("\n\n");
        }
    }

    static bool ZawieraZnaki(string word)
    {
        foreach (char c in word)
        {
            if (!char.IsLetter(c))
            {
                return true;
            }
        }
        return false;
    }

    static void Main()
    {
        Funkcja();
    }
}
