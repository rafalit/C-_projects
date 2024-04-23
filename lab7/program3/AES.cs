using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;
using System.IO;

namespace ex3
{
    public class AES
    {
        byte[] salt = Encoding.UTF8.GetBytes("CwuuJx/7");
        byte[] initVector = Encoding.UTF8.GetBytes("uyZG5sl561Wo2ZTE");
        int iterations = 5;

        public byte[] Encrypt(byte[] input_data, string password)
        {
            Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(password, salt, iterations, HashAlgorithmName.SHA256);
            Aes encAlg = Aes.Create();
            encAlg.IV = initVector;
            encAlg.Key = key.GetBytes(16);
            MemoryStream encryptionStream = new MemoryStream();
            using (CryptoStream encrypt = new CryptoStream(encryptionStream, encAlg.CreateEncryptor(),
                                                    CryptoStreamMode.Write))
            {
                encrypt.Write(input_data, 0, input_data.Length);
                encrypt.FlushFinalBlock();
            }
            key.Reset();
            Console.WriteLine("Encryption done");
            return encryptionStream.ToArray();
        }

        public byte[] Decrypt(byte[] input_data, string password)
        {
            Rfc2898DeriveBytes k1 = new Rfc2898DeriveBytes(password, salt, iterations, HashAlgorithmName.SHA256);
            Aes decAlg = Aes.Create();
            decAlg.Key = k1.GetBytes(16);
            decAlg.IV = initVector;
            MemoryStream decryptionStream = new MemoryStream();
            using (CryptoStream decrypt = new CryptoStream(decryptionStream, decAlg.CreateDecryptor(),
                                                    CryptoStreamMode.Write))
            {
                decrypt.Write(input_data, 0, input_data.Length);
                decrypt.Flush();
            }
            k1.Reset();
            Console.WriteLine("Decryption done");
            return decryptionStream.ToArray();
        }
    }
}