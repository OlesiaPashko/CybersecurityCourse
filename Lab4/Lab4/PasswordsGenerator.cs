using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Lab4
{
    public class PasswordsGenerator
    {
        private readonly string[] weakPasswords;
        private readonly string[] commonWords;
        private readonly Random random;
        public PasswordsGenerator()
        {
            weakPasswords = File.ReadAllLines("./../../../../1MofPasswords.txt");
            commonWords = File.ReadAllLines("./../../../../TheMostCommonWords.txt");
            random = new Random();
        }

        public List<string> GeneratePasswords(int passwordsAmount, int weakest100Percent, int weakest1MPercent, int randomPercent)
        {
            List<string> passwords = new List<string>();
            for(int i = 0; i< passwordsAmount; i++)
            {
                passwords.Add(GenerateBasedOnPercents(weakest100Percent, weakest1MPercent, randomPercent));
            }
            return passwords;
        }

        private string GenerateBasedOnPercents(int weakest100Percent, int weakest1MPercent, int randomPercent)
        {
            var percent = random.Next(100);
            int weakest100PercentLimit = weakest100Percent;
            int weakest1MPercentLimit = weakest100PercentLimit + weakest1MPercent;
            int randomPercentLimit = weakest1MPercentLimit + randomPercent;
            if (percent < weakest100PercentLimit)
                return GetFromTheWeakest();
            if (percent < weakest1MPercentLimit)
                return GetFrom1MWeakest();
            if (percent < randomPercentLimit)
                return GetRandomPassword(10);
            return GetHumanLikePassword(10);
        }

        private string GetFromTheWeakest()
        {
            int index = random.Next(0, 100);
            return weakPasswords[index];
        }

        private string GetFrom1MWeakest()
        {
            int index = random.Next(100, weakPasswords.Length);
            return weakPasswords[index];
        }

        private string GetRandomPassword(int length)
        {
            string availableChars = "qwertyuiopasdfghjklzxcvbnm!%^$#&*()_-+1234567890";
            StringBuilder sb = new StringBuilder();
            for(int i = 0; i < length; i++)
            {
                sb.Append(availableChars[random.Next(availableChars.Length)]);
            }
            return sb.ToString();
        }

        private string GetHumanLikePassword(int length)
        {
            string nonAlphabetic = "!%^$#&*()_-+1234567890";
            string password = commonWords[random.Next(commonWords.Length)];
            for (int i = password.Length; i < length; i++)
            {
                int index = random.Next(password.Length - 1);
                char randomSymbol = nonAlphabetic[random.Next(nonAlphabetic.Length)];
                password = password.Insert(index, randomSymbol.ToString());
            }
            return password;
        }
    }
}
