using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab1
{
    public static class TextEstimator
    {
        /// <summary>
        /// Gets Chi square for english text
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static double Chi2(string text)
        {
            
            var english_freq = new List<double>() {
            0.08167, 0.01492, 0.02782, 0.04253, 0.12702, 0.02228, 0.02015,  
            0.06094, 0.06966, 0.00153, 0.00772, 0.04025, 0.02406, 0.06749,  
            0.07507, 0.01929, 0.00095, 0.05987, 0.06327, 0.09056, 0.02758,  
            0.00978, 0.02360, 0.00150, 0.01974, 0.00074                     
            };

            var count = new List<double>();
            int ignored = 0;
            for (var i = 0; i < 26; i++) count.Add(0);

            for (var i = 0; i < text.Length; i++)
            {
                var c = text[i];
                if (c >= 65 && c <= 90)
                    count[c - 65]++;        
                else if (c >= 97 && c <= 122)
                    count[c - 97]++;  
                else if (c >= 32 && c <= 126)
                    ignored++;        
                else if (!(c == 0x80 || c == 0x99 || c == 0xe2))
                    return float.MaxValue;
            }
            double chi2 = 0;
            int len = text.Length - ignored;
            if (len == 0)
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

        /// <summary>
        /// Get coincidence index for text
        /// </summary>
        /// <param name="slice"></param>
        /// <returns></returns>
        public static double GetCoincidenceIndex(byte[] text)
        {
            double sum = 0;
            for (int i = 0; i < 256; i++)
            {
                int letterCount = text.Count(x => x == i);
                sum += letterCount * letterCount;
            }
            return sum / (text.Length * text.Length);
        }
    }
}
