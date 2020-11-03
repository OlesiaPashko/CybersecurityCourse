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

       

        static void Main()
        {
            //RunAsync().GetAwaiter().GetResult();
            string[] realNumbers = File.ReadAllLines(@"C:\Users\User\4 курс\CybersecurityCourse\Lab3\Lab3\realNumbers.txt");
            List<BigInteger> numbers = new List<BigInteger>();
            foreach(var number in realNumbers)
            {
                numbers.Add(BigInteger.Parse(number));
            }


            BigInteger n = GetN(numbers.ToArray());
            BigInteger m = GetM(numbers.ToArray(), n);
            BigInteger increment = GetIncrement(numbers.ToArray(), n, m);
            Console.WriteLine((numbers[0] * m + increment) % n + "  =  " + numbers[1]);
        }

        static BigInteger gcd(BigInteger a, BigInteger b)
        {
            if (b == 0) return a;
            return gcd(b, a % b);
        }

        static BigInteger GetN(BigInteger[] numbers)
        {
            List<BigInteger> u = GetU(GetT(numbers));
            BigInteger N = u.Aggregate(gcd);
            Console.WriteLine(BigInteger.Abs(N));
            return BigInteger.Abs(N);
        }

        private static BigInteger GetIncrement(BigInteger[] numbers, BigInteger n, BigInteger m) {
            BigInteger increment = (numbers[1] - numbers[0] * m) % n;
            return increment;
        }

        static BigInteger GetM(BigInteger[] numbers, BigInteger n)
        {
            BigInteger a = (numbers[2] - numbers[1]);
            BigInteger inverted = BigInteger.ModPow(numbers[1] - numbers[0]%n, n - 2, n);
            BigInteger multiplier = (a * inverted) % n;
            return multiplier;
        }

        static List<BigInteger> GetT(BigInteger[] numbers)
        {
            List<BigInteger> T = new List<BigInteger>();
            for(int i = 1; i < numbers.Length; i++)
            {
                T.Add(numbers[i] - numbers[i - 1]);
            }
            return T;
        }

        static List<BigInteger> GetU(List<BigInteger> t)
        {
            List<BigInteger> u = new List<BigInteger>();
            for (int i = 2; i < t.Count; i++)
            {
                u.Add(t[i] * t[i-2] - t[i-1] * t[i-1]);
            }
            return u;
        }

        static async Task RunAsync()
        {
            List<string> realNumbers = new List<string>();
            // Update port # in the following line.
            client.BaseAddress = new Uri("http://95.217.177.249/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                for (BigInteger i = 0; i < 990; i++)
                {
                    CasinoResponse casinoResponse = new CasinoResponse();

                    casinoResponse = await GetProductAsync("casino/playLcg?id=1&bet=1&number=11");
                    realNumbers.Add(casinoResponse.RealNumber.ToString());
                    ShowProduct(casinoResponse);
                }
                File.WriteAllLines(@"C:\Users\User\4 курс\CybersecurityCourse\Lab3\Lab3\realNumbers.txt", realNumbers.ToArray());
                foreach (var element in realNumbers)
                {
                    Console.Write(element + "  ");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();
        }
    }
}