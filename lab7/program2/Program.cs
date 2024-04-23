using System;
namespace ex2
{
    public class Program 
    {
        public static void Main(string[] args)
        {
            if(args.Length<3)
            {
                Console.WriteLine("not enough");
                return;
            }

            Verify verifier = new Verify();

            if(!File.Exists(args[1]))
            {
                string hash = verifier.ComputeHash(args[0], args[2]);
                File.WriteAllText(args[1], hash);
                Console.WriteLine("Hash craeted");
            }
            else
            {
                if(verifier.VerifyFile(args[0], args[1], args[2]))
                {
                    Console.WriteLine("File is valid");
                }
                else
                {
                    Console.WriteLine("File is invalid");
                }
            }
        }
    }
}