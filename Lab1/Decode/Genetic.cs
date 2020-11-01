using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Lab1
{
    public class Genetic
    {
        private char[] alphabet = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
        private string cyphrotext = "";
        private Dictionary<string, double> threegramIndexes = new Dictionary<string, double>();
        private double expectedIndex = -5.899951364365705;
        public Genetic()
        {
            ReadThreegrams();
            cyphrotext = File.ReadAllText(@".\..\..\..\text3.txt");

        }
        public void ReadThreegrams()
        {
            Dictionary<string, decimal> threegramsCounts = new Dictionary<string, decimal>();
            decimal sum = 0;
            string[] text = File.ReadAllLines(@".\..\..\..\3grams.txt");
            foreach(var line in text)
            {
                string key = line.Substring(0, 3);
                decimal value = decimal.Parse(line.Substring(5, line.Length - 5));
                threegramsCounts.Add(key.ToUpper(), value);
                sum += value;
            }
            foreach(var keyValue in threegramsCounts)
            {
                double count = Decimal.ToDouble(Decimal.Divide(keyValue.Value, sum));
                threegramIndexes.Add(keyValue.Key, Math.Log10(count));
            }
        }

        public double GetThreegramsIndex(string text)
        {
            double index = 0;
            for(int i = 0; i < text.Length-2; i++)
            {
                index+= threegramIndexes[text.Substring(i, 3)];
            }
            return index;
        }

        private double EstimateBasedOfThreegrams(string populationItem)
        {
            double index = GetThreegramsIndex(DecryptSubstitution(cyphrotext, populationItem));
            double estimation = Math.Abs(index - expectedIndex);
            return estimation;
        }

        private Dictionary<string, decimal> CountThreegramsPercents(List<string> threegrams)
        {
            Dictionary<string, decimal> counts = new Dictionary<string, decimal>();
            for(int i = 0; i < threegrams.Count; i++)
            {
                if (!counts.ContainsKey(threegrams[i]))
                {
                    int count = threegrams.Count(x => x == threegrams[i]);
                    counts.Add(threegrams[i], ((decimal)count) / threegrams.Count);
                }
            }
            return counts;
        }

        public string Decrypt()
        {
            int generation = 0;
            List<string> population = GetFirstPopulation(1000);
            string best = GetBest(population, 1)[0];
            while(EstimateBasedOfThreegrams(best) >= 0.05)
            { 
                Console.WriteLine(generation);
                List<string> bestFromPopulation = GetBest(population, 500);
                List<string> children = Crossing(bestFromPopulation);
                MutatePopulation(children);
                population = children;
                generation++;
                best = GetBest(population, 1)[0];
                Console.WriteLine("estimation = " + EstimateBasedOfThreegrams(best));
                Console.WriteLine(DecryptSubstitution(cyphrotext, best));
            }
            string decrypted = DecryptSubstitution(cyphrotext, GetBest(population, 1)[0]);
            return decrypted;
        }

        private string DecryptSubstitution(string cipherText, string key)
        {
            char[] chars = new char[cipherText.Length];

            for (int i = 0; i < cipherText.Length; i++)
            {
                if (key.IndexOf(cipherText[i]) == -1)
                    Console.WriteLine("vse och pogano");
                int j = key.IndexOf(cipherText[i]) + 65;
                chars[i] = (char)j;
            }
            return new string(chars);
        }

        private string GetFirstPopulationItem()
        {
            StringBuilder item = new StringBuilder();
            Random random = new Random();
            char letter;
            for (int i = 0; i < alphabet.Length; i++)
            {
                do
                {
                    int index = random.Next(alphabet.Length);
                    letter = alphabet[index];
                } while (item.ToString().Contains(letter));
                item.Append(letter);
            }
            return item.ToString();
        }

        private List<string> GetFirstPopulation(int populationLength)
        {
            List<string> population = new List<string>();
            for (int i = 0; i < populationLength; i++)
            {
                string item = GetFirstPopulationItem();
                population.Add(item);
            }
            return population;
        }

        private double EstimatePopulationItem(string populationItem)
        {
            return TextEstimator.Chi2(DecryptSubstitution(cyphrotext, populationItem));
        }

        private List<double> EstimatePopulation(List<string> population)
        {
            List<double> estimates = new List<double>();
            foreach (var item in population)
            {
                estimates.Add(EstimateBasedOfThreegrams(item));
            }
            return estimates;
        }

        private List<string> GetBest(List<string> population, int aliveCount)
        {
            List<string> bestItems = new List<string>();
            List<double> estimates = EstimatePopulation(population);
            for (int i = 0; i < aliveCount; i++)
            {
                int minIndex = GetMinIndex(estimates);

                bestItems.Add(population[minIndex]);
                estimates.RemoveAt(minIndex);
            }
            return bestItems;
        }

        private int GetMinIndex(List<double> list)
        {
            int index = 0;
            double min = double.MaxValue;
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i] < min)
                {
                    min = list[i];
                    index = i;
                }
            }
            return index;
        }

        /// <summary>
        /// bestFromPopulation have to be even
        /// </summary>
        /// <param name="bestFromPopulation"></param>
        /// <returns></returns>
        private List<string> Crossing(List<string> bestFromPopulation)
        {
            List<string> childs = new List<string>();
            for (int i = 1; i < bestFromPopulation.Count * 2; i++)
            {
                Random random = new Random();
                int index1 = random.Next(bestFromPopulation.Count);
                int index2 = random.Next(bestFromPopulation.Count);
                while(index1 == index2)
                    index2 = random.Next(bestFromPopulation.Count);
                childs.Add(Cross(bestFromPopulation[index1], bestFromPopulation[index2]));
            }
            return childs;
        }

        private void MutatePopulation(List<string> population)
        {
            for (int i = 0; i < population.Count; i++)
            {
                population[i] = Mutate(population[i]);
            }
        }

        private string Mutate(string item)
        {
            Random random = new Random();
            int index1 = random.Next(item.Length);
            int index2 = random.Next(item.Length);
            item = Swap(index1, index2, item);
            return item;
        }

        private string Swap(int index1, int index2, string item)
        {
            StringBuilder sb = new StringBuilder(item);
            char temp = sb[index1];
            sb[index1] = sb[index2];
            sb[index2] = temp;
            return sb.ToString();
        }

        private string Cross(string firstParent, string secondParent)
        {
            StringBuilder child = new StringBuilder();
            Random random = new Random();
            for (int i = 0; i < firstParent.Length; i++)
            {
                int parentNumber = random.Next(2);
                if (child.ToString().Contains(firstParent[i]) && child.ToString().Contains(secondParent[i]))
                {
                    child = MakeAnotherDecisionInPast(child, firstParent, secondParent, i);
                }

                if (child.ToString().Contains(firstParent[i]))
                {
                    child.Append(secondParent[i]);
                }
                else if (child.ToString().Contains(secondParent[i]))
                {
                    child.Append(firstParent[i]);
                }
                else if (parentNumber == 0)
                {
                    child.Append(firstParent[i]);
                    
                }
                else
                {
                    child.Append(secondParent[i]);
                }
            }

           return child.ToString();
        }

        private StringBuilder MakeAnotherDecisionInPast(StringBuilder child, string firstParent, string secondParent, int index)
        {
            if (!child.ToString().Contains(firstParent[index]))
            {
                return child;
            }
            int index1 = child.ToString().IndexOf(firstParent[index]);
            child[index1] = ' ';
            child = MakeAnotherDecisionInPast(child, firstParent, secondParent, index1);
            child[index1] = firstParent[index1];
            return child;
        }
        private bool GetNotContainingLetter(string key, out char letter)
        {
            foreach (var let in alphabet)
            {
                if (!key.Contains(let))
                {
                    letter = let;
                    return true;
                }
            }
            letter = alphabet[0];
            return false;
        }
    }
}
