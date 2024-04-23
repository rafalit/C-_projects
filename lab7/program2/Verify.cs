using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;

using ex2;

public class Verify
{
    public string ComputeHash(string filePath, string algorithm)
    {
        string[] avaliable_alg = new string[]{"SHA256", "SHA512", "MD5"};
        if(!avaliable_alg.Contains(algorithm.ToUpper()))
        {
            throw new ArgumentException("Invalid algorithm selected");
        }

        using(var hashAlgorithm = HashAlgorithm.Create(algorithm.ToUpper()))
        {
            using(var file = System.IO.File.OpenRead(filePath))
            {
                var hash = hashAlgorithm.ComputeHash(file);
                return Convert.ToBase64String(hash);
            }
        }
    }

    public bool VerifyFile(string file_path, string hash_file, string algorithm)
    {
        string[] avaliable_alg = new string[]{"SHA256", "SHA512", "MD5"};
        if(!avaliable_alg.Contains(hash_file.ToUpper()))
        {
            throw new ArgumentException("Invalid algorithm selected");
        }
        if(!File.Exists(hash_file))
        {
            throw new ArgumentException("Hash file does not exist");
        }

        string hash = File.ReadAllText(hash_file);
        string file = ComputeHash(file_path, algorithm);
        return hash == file;
    }
}