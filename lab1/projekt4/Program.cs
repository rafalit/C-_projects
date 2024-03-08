using System;
using System.IO;

class Program
{
    static void Main()
    {
        Funkcja("plik");
    }

    static void Funkcja(string wyraz)
    {
        string sciezkaDoPliku = Path.Combine("..", "Program3", $"{wyraz}.txt");
        int iloscLinii = 0;
        int iloscZnakow = 0;
        double sumaLiczb = 0;
        double najmniejszaLiczba = double.MaxValue;
        double najwiekszaLiczba = double.MinValue;

        if (File.Exists(sciezkaDoPliku))
        {
            using (StreamReader sr = new StreamReader(sciezkaDoPliku))
            {
                while (!sr.EndOfStream)
                {
                    iloscLinii++;
                    string linia = sr.ReadLine();

                    //zliczanie
                    iloscZnakow += linia.Length;

                    if (double.TryParse(linia, out double liczba))
                    {
                        //średnia
                        sumaLiczb += liczba;

                        if (liczba < najmniejszaLiczba)
                            najmniejszaLiczba = liczba;
                            //min

                        if (liczba > najwiekszaLiczba)
                            najwiekszaLiczba = liczba;
                            //max
                    }
                    else
                    {
                        Console.WriteLine($"Błąd: Linia {iloscLinii} nie zawiera poprawnej liczby.");
                    }
                }
            }
            double srednia = iloscLinii > 0 ? sumaLiczb / iloscLinii : 0;

            Console.WriteLine($"Ilość linii: {iloscLinii}");
            Console.WriteLine($"Ilość znaków: {iloscZnakow}");
            Console.WriteLine($"Najmniejsza liczba: {najmniejszaLiczba}");
            Console.WriteLine($"Największa liczba: {najwiekszaLiczba}");
            Console.WriteLine($"Średnia liczba: {srednia}");
        }
        else
        {
            Console.WriteLine($"Plik {wyraz}.txt nie istnieje w folderze Program3.");
        }
    }
}
