using System;
using System.Security.Cryptography;  
using System.Text;  

namespace ex1
{
    public class AssymetricKey
    {
        RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
        string filePublicKey;
        string filePrivateKey;

        public void GenereteKeys(string publickey = "publicKey.xml", string privatekey = "privateKey.xml")
        {
            filePublicKey = publickey;
            filePrivateKey = privatekey;
            Console.WriteLine("generating keys...");
            var publicKey = rsa.ToXmlString(false);
            var privateKey = rsa.ToXmlString(false);
            File.WriteAllText(filePublicKey, publicKey);
            File.WriteAllText(filePrivateKey, privateKey);
        }

        public void EncryptText(string file_a, string file_b, 
                                string publickey = "publicKey.xml")
        {
            var publicKey = File.ReadAllText(publickey);
            var rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(publicKey);
            byte[] data = File.ReadAllBytes(file_a);
            byte[] encryptBytes = rsa.Encrypt(data, false);

            File.WriteAllBytes(file_b, encryptBytes);
        }

        public void DecryptText(string file_a, string file_b, string privatekey="privateKey.xml")
        {
            var privateKey = File.ReadAllText(privatekey);
            var rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(privateKey);
            byte[] data = File.ReadAllBytes(file_a);
            byte[] decryptBytes = rsa.Decrypt(data, false);
            File.WriteAllBytes(file_b, decryptBytes);
        }

        public bool VerifySignature(string file_a, string file_b, 
                                    string publickey="publicKey.xml")
        {
            var publicKey = File.ReadAllText(publickey);
            var rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(publicKey);

            byte[] inputBytes = File.ReadAllBytes(file_a);
            byte[] signatureBytes = File.ReadAllBytes(file_b);

            bool verified = rsa.VerifyData(inputBytes, new SHA1CryptoServiceProvider(), signatureBytes);
            return verified;
        }

        public void GenerateSignature(string file_a, string file_b, string privatekey="privateKey.xml")
        {
            var privateKey = File.ReadAllText(privatekey);
            var rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(privateKey);

            byte[] inputBytes = File.ReadAllBytes(file_a);
            byte[] signatureBytes = rsa.SignData(inputBytes, new SHA1CryptoServiceProvider());

            File.WriteAllBytes(file_b, signatureBytes);
        }
    }
}