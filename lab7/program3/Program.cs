
using System;
using System.Text;
using ex3;

public class Program
{
    public static void Main(string[] args)
    {
        if (args.Length < 4)
        {
            Console.WriteLine("not enough");
            return;
        }
        AES aes = new AES();

        if (args[3] == "0")
        {
            string file = File.ReadAllText(args[0]);
            byte[] input_data = Encoding.UTF8.GetBytes(file);
            byte[] encrypted = aes.Encrypt(input_data, args[2]);
            File.WriteAllBytes(args[1], encrypted);
        }
        else if (args[3] == "1")
        {
            byte[] file = File.ReadAllBytes(args[0]);
            byte[] decrypt = aes.Decrypt(file, args[2]);
            File.WriteAllText(args[1], Encoding.UTF8.GetString(decrypt));
        }
        else
        {
            Console.WriteLine("Invalid arguments");
        }
    }
}