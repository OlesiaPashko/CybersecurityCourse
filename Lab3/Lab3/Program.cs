using MersenneTwister;
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
using System.Threading;
using System.Threading.Tasks;

namespace Lab3
{
    

    
    class Program
    {
        static HttpClient client = new HttpClient() { Timeout = TimeSpan.FromSeconds(10) };
        static void CreateAccount(int id)
        {
            string path = "/casino/createacc?id=" + id;
            client.BaseAddress = new Uri("http://95.217.177.249/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                Account accountResponse = null;
                HttpResponseMessage response = client.GetAsync(path).GetAwaiter().GetResult();
                if (response.IsSuccessStatusCode)
                {
                    accountResponse = JsonConvert.DeserializeObject<Account>(response.Content.ReadAsStringAsync().GetAwaiter().GetResult());
                }
                else
                {
                    Console.WriteLine(response);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        static void Main()
        {
            int id = 111119;
            CreateAccount(id);
            //LCGCracker lCGCracker = new LCGCracker(client, id);
            //lCGCracker.Crack();
            MTCracker mtCracker = new MTCracker(client, id);
            mtCracker.Crack();
        }


        

       /* static async Task RunMtAsync()
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
                    BetCasinoResponse casinoResponse = new BetCasinoResponse();

                    casinoResponse = GetLCGNumber("casino/playMt?id=200&bet=1&number=1");
                    realNumbers.Add(casinoResponse.RealNumber.ToString());
                    //ShowProduct(casinoResponse);
                }
                File.WriteAllLines(@"C:\Users\User\course4\CybersecurityCourse\Lab3\Lab3\realNumbersMt.txt", realNumbers.ToArray());
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
        }*/
    }
}