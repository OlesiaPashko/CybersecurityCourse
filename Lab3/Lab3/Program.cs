using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Numerics;
using System.Threading.Tasks;

namespace Lab3
{
    public class CasinoResponse
    {
        public string Message { get; set; }
        public Account Account{ get; set; }
        public BigInteger RealNumber { get; set; }
    }

    public class Account
    {
        public string Id { get; set; }
        public BigInteger Money { get; set; }
        public DateTime DeletionTime { get; set; }
    }

    class Program
    {
        static HttpClient client = new HttpClient();

        static void ShowProduct(CasinoResponse casinoResponse)
        {
            Console.WriteLine($"Message: {casinoResponse.Message}\nAccount: \n Id:" +
                $"{casinoResponse.Account.Id}\nMoney: {casinoResponse.Account.Money}" +
                $"\nDeletionTime: {casinoResponse.Account.DeletionTime}\nRealNumber: {casinoResponse.RealNumber}");
        }

        static async Task<CasinoResponse> GetProductAsync(string path)
        {
            CasinoResponse casinoResponse = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                casinoResponse = JsonConvert.DeserializeObject<CasinoResponse>(await response.Content.ReadAsStringAsync());
            }
            return casinoResponse;
        }

        static BigInteger modInverse(BigInteger a, BigInteger prime)
        {
            a = a % prime;
            for (BigInteger x = 1; x < prime; x++)
                if ((a * x) % prime == 1)
                    return x;

            return -1;
        }

        static void Main()
        {
            //uses to get all numbers

            //RunAsync().GetAwaiter().GetResult();

            
            string[] realNumbers = File.ReadAllLines(@"C:\Users\User\4 курс\CybersecurityCourse\Lab3\Lab3\realNumbers.txt");
            List<BigInteger> numbers = new List<BigInteger>();
            foreach(var number in realNumbers)
            {
                numbers.Add(BigInteger.Parse(number));
            }

           // BigInteger a = 3, m = 11;

           //Console.WriteLine(modInverse(a, m));
            /*BigInteger n = BigInteger.Pow(2, 32);
            BigInteger m = GetM(numbers.ToArray(), n);
            BigInteger increment = GetIncrement(numbers.ToArray(), n, m);*/

            Tuple<BigInteger, BigInteger> coofs = Crack(numbers);
            while(coofs.Item1 == 0&&coofs.Item2 == 0)
                coofs = Crack(numbers);
            BigInteger m = coofs.Item1;
            BigInteger increment = coofs.Item2;
            checked
            {
                //try
                //{
                    Console.WriteLine((((int)(numbers[0] * m + increment)) % m) + "  =  " + numbers[1]);
                    //Console.WriteLine((int)(object)(numbers[1] * m + increment) % m + "  =  " + numbers[2]);
                    /*Console.WriteLine((int)(numbers[2] * m + increment) % m + "  =  " + numbers[3]);
                    Console.WriteLine((int)(numbers[3] * m + increment) % m + "  =  " + numbers[4]);
                    Console.WriteLine((int)(numbers[4] * m + increment) % m + "  =  " + numbers[5]);
                    Console.WriteLine((int)(numbers[5] * m + increment) % m + "  =  " + numbers[6]);
                    Console.WriteLine((int)(numbers[6] * m + increment) % m + "  =  " + numbers[7]);
                    Console.WriteLine((int)(numbers[7] * m + increment) % m + "  =  " + numbers[8]);
                    Console.WriteLine((int)(numbers[8] * m + increment) % m + "  =  " + numbers[9]);
                    Console.WriteLine((int)(numbers[9] * m + increment) % m + "  =  " + numbers[10]);
                    Console.WriteLine((int)(numbers[10] * m + increment) % m + "  =  " + numbers[11]);
                    Console.WriteLine((int)(numbers[11] * m + increment) % m + "  =  " + numbers[12]);
                    Console.WriteLine(" m = " + m + "; increment =" + increment);*/
                //}
               // catch (Exception)
               /// {

                //}
            }
            //Console.WriteLine(GetNextNumber(numbers[numbers.Count - 2], n, m, increment));*/
        }

        private static BigInteger GetDifference(int i, BigInteger[] nums, BigInteger m) {
            // diff[0] = x1 - x0 (mod m)
            BigInteger diff = nums[i + 1] - nums[i];
            if (diff < 0)
            {
                diff += m;
            }
            return diff;
        }

        static List<BigInteger> GetDifferencesList(BigInteger[] numbers, BigInteger m) {
            List<BigInteger> differences = new List<BigInteger>();
            for(int i = 0;i<numbers.Length -1;i++) {
                differences.Add(GetDifference(i, numbers, m));
            }
            return differences;
        }

        static List<BigInteger> CalculateObservedDifferencesList(List<BigInteger> diffs, BigInteger a, BigInteger m) {
            List<BigInteger> result = new List<BigInteger>();
            foreach(var diff in diffs)
            {
                result.Add((a * diff) % m);
            }
            return result;
        }

        static BigInteger calc_c_value(BigInteger x, BigInteger xnext, BigInteger a, BigInteger m) {
            BigInteger mult_mod = (a * x) % m;
            BigInteger left = m - mult_mod;
            BigInteger c = (left + xnext) % m;
            return c;
        }


        static Tuple<BigInteger, BigInteger> Crack(List<BigInteger> sample) {
            BigInteger m = BigInteger.Pow(2, 32);

            List<BigInteger> diffs = GetDifferencesList(sample.ToArray(), m);

            //BigInteger inv = modInverse(diffs[0], BigInteger.Pow(2,32));

            BigInteger a = 1664525;//(diffs[1] * inv) % m;
            //Console.WriteLine(a);

            List<BigInteger> obs_diffs = CalculateObservedDifferencesList(diffs, a, m);

            if (IsListsEqual(diffs, obs_diffs)) 
            {
                BigInteger c = calc_c_value(sample[0], sample[1], a, m);
                Console.WriteLine(c);
                return new Tuple<BigInteger, BigInteger>(a, c);
            }

            return new Tuple<BigInteger, BigInteger>(0, 0);
        }

        static bool IsListsEqual(List<BigInteger> list1, List<BigInteger> list2)
        {
            int i = 1;
            int j = 0;
            for(; i<list1.Count;i++)
            {
                if (list1[i] != list2[j])
                    return false;
                j++;
            }
            return true;
        }
        static BigInteger GetNextNumber(BigInteger prevNumber, BigInteger n, BigInteger m, BigInteger a)
        {
            return (prevNumber * m + a) % n;
        }

        private static BigInteger GetIncrement(BigInteger[] numbers, BigInteger n, BigInteger m) {
            BigInteger increment = (n - ((numbers[0] * m) % n) + numbers[1]) % n;
            return increment;
        }

        /*static BigInteger modInverse(BigInteger a, BigInteger n)
        {
            BigInteger i = n, v = 0, d = 1;
            while (a > 0)
            {
                BigInteger t = i / a, x = a;
                a = i % x;
                i = x;
                x = d;
                d = v - t * x;
                v = x;
            }
            v %= n;
            if (v < 0) v = (v + n) % n;
            return v;
        }*/

        /*static BigInteger GetM(BigInteger[] numbers, BigInteger n)
        {
            BigInteger a = GetDifference(1, numbers, n);
            BigInteger inverted = modInverse(numbers[1] - numbers[0], n);
            BigInteger multiplier = (a * inverted) % n;
            return multiplier;
        }*/


        static async Task RunAsync()
        {
            List<string> realNumbers = new List<string>();
            client.BaseAddress = new Uri("http://95.217.177.249/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                for (BigInteger i = 0; i < 20; i++)
                {
                    CasinoResponse casinoResponse = new CasinoResponse();

                    casinoResponse = await GetProductAsync("casino/playLcg?id=4&bet=1&number=11");
                    realNumbers.Add(casinoResponse.RealNumber.ToString());
                    //ShowProduct(casinoResponse);
                }
                File.WriteAllLines(@"C:\Users\User\4 курс\CybersecurityCourse\Lab3\Lab3\realNumbers.txt", realNumbers.ToArray());
                //foreach (var element in realNumbers)
                //{
                  //  Console.Write(element + "  ");
                //}
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();
        }
    }
}