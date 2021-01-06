using Lab3.Responses;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Lab3
{
    public class MTCracker
    {
        long seed = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        private readonly HttpClient client;
        private readonly int id;

        public MTCracker(HttpClient client, int id)
        {
            this.client = client;
            this.id = id;
        }
        BetCasinoResponse GetMTNumber(string path)
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

        void ShowMTResponse(BetCasinoResponse casinoResponse)
        {
            Console.WriteLine($"Message: {casinoResponse?.Message}\nAccount: \n Id:" +
                $"{casinoResponse?.Account.Id}\nMoney: {casinoResponse?.Account.Money}" +
                $"\nDeletionTime: {casinoResponse?.Account.DeletionTime}\nRealNumber: {casinoResponse?.RealNumber}");
        }

        public void Crack()
        {
            MT19937 MT;
            long number = 0;
            var response = GetMTNumber($"casino/playMt?id={id}&bet=1&number=1");
            do
            {
                MT = new MT19937();
                MT.init_genrand((ulong)seed);
                number = (long)MT.genrand_int32();
                seed++;
            } while (response.RealNumber != number);

            while (response.Account.Money < 1000000)
            {
                number = (long)MT.genrand_int32();
                response = GetMTNumber($"casino/playMt?id={id}&bet=100&number={number}");
                ShowMTResponse(response);
            }
        }
    }
}
