using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Decode
{
    class Program
    {
        static void Main(string[] args)
        {
            //string text1 = File.ReadAllText(@"C:\Users\User\source\repos\Decode\text.txt");
            //Decrypt(Encoding.UTF8.GetBytes(text1));
            string text2 = File.ReadAllText(@"C:\Users\User\source\repos\Decode\text2.txt");
            //Console.WriteLine(text2.Length);
            byte[] textBytes = Enumerable.Range(0, text2.Length)
                    .Where(x => x % 2 == 0)
                    .Select(x => Convert.ToByte(text2.Substring(x, 2), 16))
                    .ToArray();
            int keyLength = GetKeyLength(textBytes, 0.01);
            Console.WriteLine(keyLength);
            List<List<byte>> slices = GetSlicesByKeyLength(keyLength, textBytes);
            List<string> decryptedSlices = new List<string>();
            //foreach(var slice in slices)
            //{
                //decryptedSlices.Add(Decrypt(slices[2].ToArray()));
            //}
            //Console.WriteLine(JoinDecryptedSlices(decryptedSlices, keyLength));
        }

        private static string JoinDecryptedSlices(List<string> slices, int keyLength)
        {
            StringBuilder result = new StringBuilder();

            for (int i = 0; i < slices[0].Length; i++)
            {
                for (int j = 0; j < keyLength; j++)
                {
                    result.Append(slices[j][i]);
                }
            }
            return result.ToString();
        }

        private static List<List<byte>> GetSlicesByKeyLength(int keyLength, byte[] text)
        {
            List<List<byte>> slices = new List<List<byte>>();
            for (int i = 0; i < keyLength; i++)
            {
                List<byte> slice = new List<byte>();
                for (int j = i; j < text.Length; j += keyLength)
                {
                    slice.Add(text[j]);
                }
                slices.Add(slice);
            }
            return slices;
        }

        private static int GetKeyLength(byte[] text, double precision)
        {
            List<List<byte>> slices = GetSlices(text);
            double englishCoincidenceIndex = 0.065;
            for(int i = 0; i < slices.Count;i++)
            {
                if (Math.Abs(GetCoincidenceIndex(slices[i].ToArray()) - englishCoincidenceIndex)< precision){
                    return i + 1;
                }
            }
            return text.Length;//than it`s one time note
        }

        public static List<List<byte>> GetSlices(byte[] text)
        {
            List<List<byte>> slices = new List<List<byte>>();
            for (int i = 1; i < 20; i++)
            {
                List<byte> slice = new List<byte>();
                for (int j = 0; j < text.Length; j += i)
                {
                    slice.Add(text[j]);
                }
                slices.Add(slice);
            }
            return slices;
        }

        public static double GetCoincidenceIndex(byte[] slice)
        {
            double sum = 0;
            for (int i = 0; i < 256; i++)
            {
                int letterCount = slice.Count(x => x == i);
                sum += letterCount * letterCount;
            }
            return sum / (slice.Length * slice.Length);
        }


        public static string Decrypt(byte[] text)
        {
            List<double> decriptedChi2s = new List<double>();
            List<string> outTexts = new List<string>();
            for (int i = 0; i < 256; i++)
            {
                StringBuilder outText = new StringBuilder();
                foreach (char ch in text)
                {
                    outText.Append((char)(ch ^ i));
                }
                decriptedChi2s.Add(GetChi2(outText.ToString()));
                outTexts.Add(outText.ToString());
                //Console.WriteLine(i);
                //Console.WriteLine(outText.ToString());
                //Console.ReadLine();
            }

            int minIndex = GetMinIndex(decriptedChi2s);
            Console.WriteLine("Decripted text : ");
            Console.WriteLine(outTexts[minIndex]);
            Console.WriteLine("Index = " + minIndex);
            Console.WriteLine("Chi2 is " + decriptedChi2s[minIndex]);
            return outTexts[minIndex];
        }

        private static int GetMinIndex(List<double> list)
        {
            int indexOfMin = 0;
            double min = list[0];
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i] < min)
                {
                    min = list[i];
                    indexOfMin = i;
                }
            }
            return indexOfMin;
        }
        public static double GetChi2(string text)
        {
            var english_freq = new List<double>() {
            0.08167, 0.01492, 0.02782, 0.04253, 0.12702, 0.02228, 0.02015,  // A-G
            0.06094, 0.06966, 0.00153, 0.00772, 0.04025, 0.02406, 0.06749,  // H-N
            0.07507, 0.01929, 0.00095, 0.05987, 0.06327, 0.09056, 0.02758,  // O-U
            0.00978, 0.02360, 0.00150, 0.01974, 0.00074                     // V-Z
            };

            var count = new List<double>();
            int ignored = 0;
            for (var i = 0; i < 26; i++) count.Add(0);

            for (var i = 0; i < text.Length; i++)
            {
                var c = text[i];
                if (c >= 65 && c <= 90)
                    count[c - 65]++;        // uppercase A-Z
                else if (c >= 97 && c <= 122)
                    count[c - 97]++;  // lowercase a-z
                else if (c >= 32 && c <= 126)
                    ignored++;        // numbers and punct.
                else
                    ignored++;  // TAB, CR, LF
            }
            double chi2 = 0;
            int len = text.Length - ignored;
            if(len == 0)
            {
                return Double.MaxValue;
            }
            for (var i = 0; i < 26; i++)
            {
                var observed = count[i];
                double expected = len * english_freq[i];
                var difference = observed - expected;
                chi2 += difference * difference / expected;
            }
            return chi2;
        }
    }
 }


