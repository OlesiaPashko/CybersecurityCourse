using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Lab1
{
    class Program
    {
        static void Main()
        {
            /*Console.WriteLine("-----Task 2-----------");
            var decrypted2 = Decrypt1_2();
            Console.WriteLine(decrypted2);*/
            /*Console.WriteLine("-----Task 3-----------");
            var decrypted3 = Task3Decryptor.Decrypt1_3();
            Console.WriteLine(decrypted3);*/
            Genetic genetic = new Genetic();
            //var threegrams = genetic.ParseThreegrams(File.ReadAllText((@".\..\..\..\text3.txt")));
            string decrypted4 = genetic.Decrypt();
            Console.WriteLine(decrypted4);
        }

        public static string Decrypt1_2()
        {
            string text = File.ReadAllText(@".\..\..\..\text.txt");
            string decripted = BruteForceDecryptor.Decrypt(Encoding.UTF8.GetBytes(text));
            return decripted;
        }
    }
 }


