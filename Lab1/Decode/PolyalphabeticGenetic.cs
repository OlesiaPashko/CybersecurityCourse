using System.IO;

namespace Lab1
{
    public class PolyalphabeticGenetic
    {
        private string cybertext;

        public PolyalphabeticGenetic()
        {
            cybertext = File.ReadAllText(@".\..\..\..\text3.txt");
        }
        public string Decrypt()
        {
            return cybertext;
            /*int generation = 0;
            List<string> population = GetFirstPopulation(1000);
            string best = GetBest(population, 1)[0];
            while (EstimateBasedOnThreegrams(best) >= 0.12)
            {
                Console.WriteLine(generation);
                List<string> bestFromPopulation = GetBest(population, 500);
                List<string> children = Crossing(bestFromPopulation);
                MutatePopulation(children);
                population = children;
                generation++;
                best = GetBest(population, 1)[0];
                Console.WriteLine("estimation = " + EstimateBasedOnThreegrams(best));
                Console.WriteLine(DecryptSubstitution(cyphrotext, best));
            }
            string decrypted = DecryptSubstitution(cyphrotext, GetBest(population, 1)[0]);
            return decrypted;*/
        }
    }
}