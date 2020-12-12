using Org.BouncyCastle.Crypto.Generators;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Lab4
{
    public class Hasher
    {
        public void StoreHashes(List<string> passwords)
        {
           
            //SavePasswordsHashes(passwords, "../../../md5PasswordHashes.csv", GetStringHashMD5);
            //SavePasswordsHashes(passwords, "../../../sha1PasswordHashes.csv", GetStringHashSHA1);
            SavePasswordsHashes(passwords, "../../../BCryptPasswordHashes.csv", GetStringHashBCrypt);
        }
        private void SavePasswordsHashes(List<string> passwords, string filePath, Func<string, string> hashingFunction)
        {
            StreamWriter sw = new StreamWriter(filePath);

            foreach (string password in passwords)
            {
                sw.WriteLine(hashingFunction(password));
            }

            sw.Close();
        }

        private string GetStringHashMD5(string password)
        {
            MD5 md5 = MD5.Create();
            byte[] hash = md5.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hash);
        }

        private string GetStringHashSHA1(string password)
        {
            SHA1 sha1 = SHA1.Create();

            byte[] salt = new byte[16];
            RandomNumberGenerator random = RandomNumberGenerator.Create();
            random.GetBytes(salt);
            string stringSalt = Convert.ToBase64String(salt);

            byte[] hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(password + stringSalt));

            return "Hash: " + Convert.ToBase64String(hash) + " Salt: " + stringSalt;
        }

        private string GetStringHashBCrypt(string password)
        {
            string salt = BCrypt.Net.BCrypt.GenerateSalt(5);
            return BCrypt.Net.BCrypt.HashPassword(password, salt);
        }
    }
}
