using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;

namespace Lab1
{
    public class Genetic
    {
        private char[] alphabet = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
        private Dictionary<string, decimal> threegrams = new Dictionary<string, decimal>();

        public void ReadThreegrams()
        {
            string[] text = File.ReadAllLines(@".\..\..\..\3grams.txt");
            foreach(var line in text)
            {
                string key = line.Substring(0, 3);
                string value = line.Substring(5, line.Length - 5);
                threegrams.Add(key.ToUpper(), decimal.Parse(value));
            }
        }

        public List<string> ParseThreegrams(string text)
        {
            List<string> threegrams = new List<string>();
            for(int i = 0; i < text.Length-2; i++)
            {
                threegrams.Add(text.Substring(i, 3));
            }
            return threegrams;
        }

        private decimal EstimateBasedOfThreegrams(string populationItem)
        {
            List<string> threegrams = ParseThreegrams(DecryptSubstitution(File.ReadAllText(@".\..\..\..\text3.txt"), populationItem));
            decimal estimation = 0;
            foreach(var threegram in threegrams)
            {
                estimation += this.threegrams[threegram];
            }
            Console.WriteLine("estimation = " + estimation);
            return estimation;
        }

        public string Decrypt()
        {
            int generation = 0;
            string text = File.ReadAllText(@".\..\..\..\text3.txt");
            ReadThreegrams();
            List<string> population = GetFirstPopulation(1000);
            do
            {
                Console.WriteLine(generation);
                List<string> bestFromPopulation = GetBest(population, 10);
                List<string> children = Crossing(bestFromPopulation);
                MutatePopulation(children);
                population = children;
                generation++;
                Console.WriteLine(DecryptSubstitution(text, GetBest(population, 1)[0]));
            } while (generation < 1000);
            string decrypted = DecryptSubstitution(text, GetBest(population, 1)[0]);
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
            return TextEstimator.Chi2(DecryptSubstitution(File.ReadAllText(@".\..\..\..\text3.txt"), populationItem));
        }

        private List<decimal> EstimatePopulation(List<string> population)
        {
            List<decimal> estimates = new List<decimal>();
            foreach (var item in population)
            {
                estimates.Add(EstimateBasedOfThreegrams(item));
            }
            return estimates;
        }

        private List<string> GetBest(List<string> population, int aliveCount)
        {
            List<string> bestItems = new List<string>();
            List<decimal> estimates = EstimatePopulation(population);
            for (int i = 0; i < aliveCount; i++)
            {
                int maxIndex = GetMaxIndex(estimates);

                bestItems.Add(population[maxIndex]);
                estimates.RemoveAt(maxIndex);
            }
            return bestItems;
        }

        private int GetMaxIndex(List<decimal> list)
        {
            int index = 0;
            decimal max = decimal.MinValue;
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i] > max)
                {
                    max = list[i];
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
            for (int i = 1; i < bestFromPopulation.Count; i += 2)
            {
                childs.Add(Cross(bestFromPopulation[i - 1], bestFromPopulation[i]));
                childs.Add(Cross(bestFromPopulation[i - 1], bestFromPopulation[i]));
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
                    continue;
                else if (parentNumber == 0)
                {
                    if (child.ToString().Contains(firstParent[i]))
                    {
                        child.Append(secondParent[i]);
                    }
                    else
                    {
                        child.Append(firstParent[i]);
                    }
                }
                else
                {
                    if (child.ToString().Contains(secondParent[i]))
                    {
                        child.Append(firstParent[i]);
                    }
                    else
                    {
                        child.Append(secondParent[i]);
                    }
                }
            }

            char notHappenedLetter;
            bool keyIsNotFull = GetNotContainingLetter(child.ToString(), out notHappenedLetter);
            while (keyIsNotFull)
            {
                child.Append(notHappenedLetter);
                keyIsNotFull = GetNotContainingLetter(child.ToString(), out notHappenedLetter);
            } 
            return child.ToString();
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
