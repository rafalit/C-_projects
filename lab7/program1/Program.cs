using System;
using System.Text;
using ex1;

public class Program
{
    public static void Main(string[]args)
    {
       if(args.Length<1)
       {
            Console.WriteLine("not enough");
            return;
       }
       AssymetricKey ciph = new AssymetricKey();
       int number = int.Parse(args[0]);

       if(number == 0)
       {
            ciph.GenereteKeys();
       }
       else if(number == 1)
       {
            ciph.EncryptText(args[1], args[2]);
       }
    
       else if(number == 2)
       {
            ciph.DecryptText(args[1], args[2]);
       }

       else if(number == 3)
       {
            if(!File.Exists(args[2]))
            {
                ciph.GenerateSignature(args[1], args[2]);
            }
            else
            {
                ciph.VerifySignature(args[1], args[2]);
            }
       }

       else
       {
            Console.WriteLine("wrong command");
       }
    }
}