using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Lab1
{
    public static class Task3Decryptor
    {
        /// <summary>
        /// Decryptes Task 1.3
        /// </summary>
        /// <returns></returns>
        public  static string Decrypt1_3()
        {
            string text2 = File.ReadAllText(@".\..\..\..\text2.txt");

            //get bytes from x16
            byte[] textBytes = Enumerable.Range(0, text2.Length)
                    .Where(x => x % 2 == 0)
                    .Select(x => Convert.ToByte(text2.Substring(x, 2), 16))
                    .ToArray();

            //get key length using
            int keyLength = GetKeyLength(textBytes, 0.01);
            List<List<byte>> slices = GetSlicesByKeyLength(keyLength, textBytes);
            List<string> decryptedSlices = new List<string>();
            foreach (var slice in slices)
            {
                decryptedSlices.Add(BruteForceDecryptor.Decrypt(slice.ToArray()));
            }
            return JoinDecryptedSlices(decryptedSlices, keyLength);
        }

        /// <summary>
        /// Joins decrypted slices
        /// Every slice was decrypted by one letter of key
        /// </summary>
        /// <param name="slices"></param>
        /// <param name="keyLength"></param>
        /// <returns></returns>
        private static string JoinDecryptedSlices(List<string> slices, int keyLength)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < GetMinLenght(slices); i++)
            {
                for (int j = 0; j < keyLength; j++)
                {
                    result.Append(slices[j][i]);
                }
            }
            result.Append(slices[0][slices[0].Length - 1]);
            return result.ToString();
        }

        /// <summary>
        /// Gets the length of the shortest string in list
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private static int GetMinLenght(List<string> list)
        {
            int minLength = list[0].Length;
            foreach (var str in list)
            {
                if (minLength > str.Length)
                    minLength = str.Length;
            }
            return minLength;
        }

        /// <summary>
        /// get slices of cybertext devided by letter of key
        /// for instance, if key has length of 5 than there will be returned 5 slices
        /// One slice for every letter
        /// </summary>
        /// <param name="keyLength"></param>
        /// <param name="text"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Get key length using coincidence index and slicing based on key length
        /// </summary>
        /// <param name="text"></param>
        /// <param name="precision"></param>
        /// <returns></returns>
        private static int GetKeyLength(byte[] text, double precision)
        {
            List<List<byte>> slices = GetSlices(text);
            double englishCoincidenceIndex = 0.065;
            for (int i = 0; i < slices.Count; i++)
            {
                if (Math.Abs(TextEstimator.GetCoincidenceIndex(slices[i].ToArray()) - englishCoincidenceIndex) < precision)
                {
                    return i + 1;
                }
            }
            return text.Length;//than it`s one time note
        }

        /// <summary>
        /// Creates array of slices for key length from 1 to 20
        /// for instance third element of returned array is {firstByte, forthByte, seventhByte, ...}
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
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

    }
}
