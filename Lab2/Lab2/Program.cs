using System;
using System.IO;
using System.Linq;
using System.Text;

namespace Lab2
{
    class Program
    {
        static void Main()
        {
            string[] lines = File.ReadAllLines(@".\..\..\..\..\text.txt");
            var lineBytes = lines.Select(x=>ToByteArrayFromx16(x)).ToList();
            Console.WriteLine("What lines do you want to use?");
            Console.Write("first - ");
            int number1 = int.Parse(Console.ReadLine());
            Console.Write("second - ");
            int number2 = int.Parse(Console.ReadLine());
            Console.WriteLine("How many crib words do you wont to test?");
            int cribWordsAmount = int.Parse(Console.ReadLine());
            ToFindUsingDictionaryCribs(cribWordsAmount, lineBytes[number1], lineBytes[number2]);
        }



        static void ToFindUsingDictionaryCribs(int cribsCount, byte[] message1, byte[] message2)
        {
            string[] theMostCommonWords = File.ReadAllLines(@".\..\..\..\..\TheMostCommonWords.txt");
            for (int i = 0; i < cribsCount; i++)
            {
                string crib = theMostCommonWords[i];
                Console.WriteLine("The crib word in " + crib);
                var xored = XOR(message1, message2);
                var bytesOfCrib = ToBytes(crib);
                for (int j = 0; j < 1 + xored.Length - theMostCommonWords[3].Length; j++)
                {
                    Console.WriteLine("Skiped: " +j + "  , result:" + ToString(XOR(bytesOfCrib, SubArray(xored, j, bytesOfCrib.Length))));
                }
                Console.WriteLine("------------------------");
             }
        }

        public static byte[] SubArray(byte[] array, int head, int length)
        {
            return array.Skip(head)
                        .Take(length)
                        .ToArray();
        }

        static string ToString(byte[] input)
        {
            return Encoding.ASCII.GetString(input);
        }

        static byte[] ToBytes(string text)
        {
            return Encoding.ASCII.GetBytes(text);
        }

        static byte[] ToByteArrayFromx16(string text)
        {
            return Enumerable.Range(0, text.Length)
                    .Where(x => x % 2 == 0)
                    .Select(x => Convert.ToByte(text.Substring(x, 2), 16))
                    .ToArray();
        }

        static byte[] XOR(byte[] message1, byte[] message2)
        {
            int minLength = (message1.Length > message2.Length) ? message2.Length : message1.Length;

            byte[] result = new byte[minLength];

            for (int i = 0; i < minLength; i++)
            {
                result[i] = (byte)(message1[i] ^ message2[i]);
            }
            return result;

        }
    }
}
