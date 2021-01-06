using Lab3.Responses;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Lab3
{
    public class BetterMTCracker
    {
        private int statesCount = 624;
        private readonly HttpClient client;
        private readonly int id;

        public BetterMTCracker(HttpClient client, int id)
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

        List<long> GetMTNumbers()
        {
            List<long> realNumbers = new List<long>();
            try
            {
                for (int i = 0; i < statesCount; i++)
                {
                    BetCasinoResponse casinoResponse = new BetCasinoResponse();
                    casinoResponse = GetMTNumber($"casino/playBetterMT?id={id}&bet=1&number=1");
                    ShowMTResponse(casinoResponse);
                    realNumbers.Add(casinoResponse.RealNumber);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return realNumbers;
        }


        void ShowMTResponse(BetCasinoResponse casinoResponse)
        {
            Console.WriteLine($"Message: {casinoResponse?.Message}\nAccount: \n Id:" +
                $"{casinoResponse?.Account.Id}\nMoney: {casinoResponse?.Account.Money}" +
                $"\nDeletionTime: {casinoResponse?.Account.DeletionTime}\nRealNumber: {casinoResponse?.RealNumber}");
        }

        public ulong Untemper(ulong state)
        {
            state ^= (state >> 18);
            state ^= (state << 15) & 0xefc60000UL;
            state ^= ((state << 7) & 0x9d2c5680UL) ^
                ((state << 14) & 0x94284000UL) ^
                ((state << 21) & 0x14200000UL) ^
                ((state << 28) & 0x10000000UL);
            state ^= (state >> 11) ^ (state >> 22);

            return state;
        }

        public List<ulong> UntemperStates()
        {
            List<ulong> states = new List<ulong>();
            List<long> numbers = GetMTNumbers();
            foreach(var number in numbers)
            {
                states.Add(Untemper((ulong)number));
            }
            return states;
        }

        public void Crack()
        {
            MT19937 MT = new MT19937();
            var states = UntemperStates();
            MT.init_genrand(states.ToArray());
            var number = (long)MT.genrand_int32();
            var response = GetMTNumber($"casino/playBetterMt?id={id}&bet=100&number={number}");
            ShowMTResponse(response);
            while (response.Account.Money < 1000000)
            {
                number = (long)MT.genrand_int32();
                response = GetMTNumber($"casino/playBetterMt?id={id}&bet=100&number={number}");
                ShowMTResponse(response);
            }
            
        }
    }
}
