using System;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Xml;
using System.Xml.Serialization;
using System.Linq;
using System.Globalization;

namespace program
{
    class Reader
    {
        static void Main(string[] args)
        {
            string filePath = "data.json";
            List<User> users = WczytajDane(filePath);

            ZapiszDoPlikuXML(users, "data.xml");

            List<User> wczytaniUzytkownicy = WczytajZPlikuXML("data.xml");

            Dictionary<string, List<string>> mojSlownik = new Dictionary<string, List<string>>();
            
            Dictionary<string, int> licznik = new Dictionary<string, int>();

            Dictionary<string, HashSet<int>> licznik2= new Dictionary<string, HashSet<int>>();

            int linijka = 1;

            foreach (var user in wczytaniUzytkownicy)
            {
                if(!mojSlownik.ContainsKey(user.UserName))
                {
                    mojSlownik[user.UserName] = new List<string>();
                }
                mojSlownik[user.UserName].AddRange(user.Text.Split(new string[] { "\" " }, StringSplitOptions.RemoveEmptyEntries));
                

                SprawdzKazdegoTweeta(licznik, user.Text);
                
                SprawdzTweety(licznik2, user.Text, linijka);

                linijka++;
            }

            NajstarszyTweet(wczytaniUzytkownicy);
            NajnowszyTweet(wczytaniUzytkownicy);
            
            //foreach (var value in mojSlownik["JStein_Wonkblog"])
            //{
            //    Console.WriteLine(value);
            //}
            var posortowanyLicznik = licznik.OrderByDescending(pair => pair.Value);

            int count = 0;
            foreach(var pair in posortowanyLicznik)
            {
                if(pair.Key.Length >= 5)
                {
                    //Console.WriteLine($"{pair.Key} : {pair.Value}");
                    count++;

                    if(count == 10)
                    {
                        break;
                    }
                }
            }

            var posortowanyLicznik2 = licznik2.OrderByDescending(pair => pair.Value.Count);
            var posortowanySłownik2 = posortowanyLicznik2.ToDictionary(pair => pair.Key, pair => pair.Value);
            
            Console.WriteLine("\n----------\n");

           foreach(var pair in posortowanySłownik2.Take(10))
            {
                Console.WriteLine($"{pair.Key} : {pair.Value.Count}");
            }
        }

        static List<User> WczytajDane(string sciezka)
        {
            string jsonContent = File.ReadAllText(sciezka);
            RootObject jsonData = JsonSerializer.Deserialize<RootObject>(jsonContent);
            return jsonData.data;
        }

        static void ZapiszDoPlikuXML(List<User> users, string sciezka)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<User>));

            using(TextWriter writer = new StreamWriter(sciezka))
            {
                serializer.Serialize(writer, users);
            }
            Console.WriteLine("Dane zapisano dopliku XML!");
        }

        static List<User> WczytajZPlikuXML(string sciezka)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<User>));
            using(FileStream fileStream = new FileStream(sciezka, FileMode.Open))
            {
                return (List<User>)serializer.Deserialize(fileStream);
            }
        }

        static List<User> SortujPoNazwie(List<User> wczytaniUzytkownicy)
        {
            return wczytaniUzytkownicy.OrderBy(o => o.UserName).ToList();
        }

        static void NajstarszyTweet(List<User> wczytaniUzytkownicy)
        {
            string formatDaty = "MMMM d, yyyy 'at' hh:mmtt";
            DateTime oldest = DateTime.ParseExact("May 5, 2023 at 08:00PM", formatDaty, CultureInfo.InvariantCulture);

            foreach (var user in wczytaniUzytkownicy)
            {
                DateTime data = DateTime.ParseExact(user.CreatedAt, formatDaty, CultureInfo.InvariantCulture);

                if(DateTime.Compare(data, oldest) < 0)
                {
                    oldest = data;
                }
            }
            Console.WriteLine("Najstarszy tweet został opublikowany: " + oldest.ToString(formatDaty));
        }

        static void NajnowszyTweet(List<User> wczytaniUzytkownicy)
        {
            string formatDaty = "MMMM d, yyyy 'at' hh:mmtt";
            DateTime newest = DateTime.ParseExact("May 5, 2016 at 07:00AM", formatDaty, CultureInfo.InvariantCulture);

            foreach(var user in wczytaniUzytkownicy)
            {
                DateTime data = DateTime.ParseExact(user.CreatedAt, formatDaty, CultureInfo.InvariantCulture);

                if(DateTime.Compare(newest, data) <0 )
                {
                    newest = data;
                }
            }
            Console.WriteLine("Najnowszy tweet został opublikowany: " + newest.ToString(formatDaty));
        }
        static Dictionary<string, int> SprawdzKazdegoTweeta(Dictionary<string, int> licznik, string text)
        {
            string [] words = text.Split(new char [] {' ', '.', ',', ':', '!', '?', '-', ')', '(', '[', ']'}, StringSplitOptions.RemoveEmptyEntries);
            
            foreach(string word in words)
            {
                if(word == "https")
                {
                    break;
                }
                if(!licznik.ContainsKey(word))
                {
                    licznik[word]=1;
                }
                else{
                    licznik[word]++;
                }
            }
            return licznik;
        }
        static Dictionary<string, HashSet<int>> SprawdzTweety(Dictionary<string, HashSet<int>> licznik2, string text, int linijka){
            string [] words = text.Split(new char [] {' ', '.', ',', ':', '!', '?', '-', ')', '(', '[', ']'}, StringSplitOptions.RemoveEmptyEntries);
            
            foreach(string word in words)
            {
                if(word == "https")
                {
                    break;
                }

                if(!licznik2.ContainsKey(word))
                {
                    licznik2[word]=new HashSet<int>();
                }
                licznik2[word].Add(linijka);
            }
            return licznik2;
        }
    }
}