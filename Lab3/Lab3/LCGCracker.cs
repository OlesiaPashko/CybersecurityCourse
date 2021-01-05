using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Lab3.Responses;
using System.Net.Http.Headers;

namespace Lab3
{
    public struct LCGCoefs
    {
        public long Modulus;
        public long Multiplier;
        public long Increment;

        public LCGCoefs(long modulus, long multiplier, long increment)
        {
            Modulus = modulus;
            Increment = increment;
            Multiplier = multiplier;
        }
    }
    public class LCGCracker
    {
        private readonly HttpClient client;
        private readonly int id;

        public LCGCracker(HttpClient client, int id)
        {
            this.client = client;
            this.id = id;
        }

         void ShowLCGResponse(BetCasinoResponse casinoResponse)
        {
            Console.WriteLine($"Message: {casinoResponse?.Message}\nAccount: \n Id:" +
                $"{casinoResponse?.Account.Id}\nMoney: {casinoResponse?.Account.Money}" +
                $"\nDeletionTime: {casinoResponse?.Account.DeletionTime}\nRealNumber: {casinoResponse?.RealNumber}");
        }

         BetCasinoResponse GetLCGNumber(string path)
        {
            BetCasinoResponse casinoResponse = null;
            HttpResponseMessage response = client.GetAsync(path).GetAwaiter().GetResult();
            if (response.IsSuccessStatusCode)
            {
                casinoResponse = JsonConvert.DeserializeObject<BetCasinoResponse>(response.Content.ReadAsStringAsync().GetAwaiter().GetResult());
            }
            else
            {
                Console.WriteLine(response);
            }
            return casinoResponse;
        }
        public void Crack()
        {
            List<long> numbers = GetLCGNumbers();
            var coefs = GetCoefs(numbers);
            BetCasinoResponse casinoResponse = new BetCasinoResponse();
            do
            {
                Thread.Sleep(500);
                long num = (coefs.Multiplier * numbers[numbers.Count - 1] + coefs.Increment) % coefs.Modulus;
                if (num > Int32.MaxValue || num < Int32.MinValue)
                    num = num > 0 ? num - coefs.Modulus : num + coefs.Modulus;
                Console.WriteLine($"casino/playLcg?id={id}&bet=1&number={num}");
                casinoResponse = GetLCGNumber($"casino/playLcg?id={id}&bet=100&number={num}");
                ShowLCGResponse(casinoResponse);
                numbers[numbers.Count - 1] = casinoResponse == null ? numbers[numbers.Count - 1] : casinoResponse.RealNumber;
            } while (casinoResponse == null || casinoResponse?.Account.Money < 1000000);
        }

        public long ModInverse(long a, long m)
        {
            if (m == 1) return 0;
            long m0 = m;
            (long x, long y) = (1, 0);

            while (a > 1)
            {
                long q = a / m;
                (a, m) = (m, a % m);
                (x, y) = (y, x - q * y);
            }
            return x < 0 ? x + m0 : x;
        }

        public LCGCoefs GetCoefs(List<long> numbers)
        {
            long modulus = (long)Math.Pow(2, 32);
            long multiplier = (numbers[2] - numbers[1]) *
                ModInverse((numbers[1] - numbers[0]), modulus)
                % modulus;
            long increment = (numbers[1] - multiplier * numbers[0]) % modulus;
            return new LCGCoefs(modulus, multiplier, increment);
        }


        List<long> GetLCGNumbers()
        {
            List<long> realNumbers = new List<long>();

            try
            {
                for (int i = 0; i < 3; i++)
                {
                    BetCasinoResponse casinoResponse = new BetCasinoResponse();
                    casinoResponse = GetLCGNumber($"casino/playLcg?id={id}&bet=1&number=1");
                    ShowLCGResponse(casinoResponse);
                    realNumbers.Add(casinoResponse.RealNumber);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return realNumbers;

        }
    }
}
