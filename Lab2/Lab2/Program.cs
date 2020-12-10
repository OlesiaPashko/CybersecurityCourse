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
            (byte[], byte[]) lines = AskLinesInUser();

            ShowDictionaryCribsTest(lines);

            ShowUserCribsTest(lines);
        }


        static void ShowUserCribsTest((byte[], byte[]) lines)
        {
            while (true)
            {
                Console.WriteLine("Please enter your crib (the one that is most likely to work)");
                string crib = Console.ReadLine();
                TryCrib(crib, lines.Item1, lines.Item2);
            }
        }

        static void ShowDictionaryCribsTest((byte[], byte[]) lines)
        {
            Console.WriteLine("How many crib words do you wont to test?");
            int cribWordsAmount = int.Parse(Console.ReadLine());
            TryDictionaryCribs(cribWordsAmount, lines.Item1, lines.Item2);
        }

        static (byte[], byte[]) AskLinesInUser()
        {
            string[] lines = File.ReadAllLines(@".\..\..\..\..\text.txt");
            var lineBytes = lines.Select(x => ToByteArrayFromx16(x)).ToList();

            Console.WriteLine("What lines do you want to use?");
            Console.Write("first - ");
            int number1 = int.Parse(Console.ReadLine());
            Console.Write("second - ");
            int number2 = int.Parse(Console.ReadLine());

            byte[] line1 = lineBytes[number1];
            byte[] line2 = lineBytes[number2];

            return (line1, line2);
        }

        static void TryCrib(string crib, byte[] message1, byte[] message2)
        {
            Console.WriteLine("The crib word is:  " + crib);
            var xored = XOR(message1, message2);
            var bytesOfCrib = ToBytes(crib);
            for (int j = 0; j < 1 + xored.Length - crib.Length; j++)
            {
                Console.WriteLine("Skiped: " + j + "  , result:" + ToString(XOR(bytesOfCrib, SubArray(xored, j, bytesOfCrib.Length))));
            }
            Console.WriteLine("------------------------");
        }

        static void TryDictionaryCribs(int cribsCount, byte[] message1, byte[] message2)
        {
            string[] theMostCommonWords = File.ReadAllLines(@".\..\..\..\..\TheMostCommonWords.txt");
            for (int i = 0; i < cribsCount; i++)
            {
                TryCrib(theMostCommonWords[i], message1, message2);
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
