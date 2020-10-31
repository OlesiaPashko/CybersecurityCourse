using System;
using System.Collections.Generic;
using System.Text;

namespace Lab1
{
    public static class BruteForceDecryptor
    {
        /// <summary>
        /// Decryptes text using 1 symbol xor
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
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
                decriptedChi2s.Add(TextEstimator.Chi2(outText.ToString()));
                outTexts.Add(outText.ToString());
            }

            int minIndex = GetMinIndex(decriptedChi2s);
            return outTexts[minIndex];
        }

        /// <summary>
        /// Gets the index of minimum in list
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
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
    }
}
