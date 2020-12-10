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
            string[] theMostCommonWords = File.ReadAllLines(@".\..\..\..\..\TheMostCommonWords.txt");
            var lineBytes = lines.Select(x=>ToByteArrayFromx16(x)).ToList();
            var xored = XOR(lineBytes[0], lineBytes[1]);
            for (int i = 0; i < 1 + xored.Length - theMostCommonWords[3].Length; i++)
            {
                Console.WriteLine(i + "     " + ToString(XOR(ToBytes(theMostCommonWords[3]), SubArray(xored, i, ToBytes(theMostCommonWords[3]).Length))));
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
