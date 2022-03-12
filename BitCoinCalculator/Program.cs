using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;

namespace BitCoinCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter the amount of your bitcoins:");
            float userCoins = float.Parse(Console.ReadLine());
            BitCoinRate currentBitcoin = GetRates();
            Console.WriteLine("Pick USD, EUR or GBP");
            string userCurrency = Console.ReadLine();

            if (userCurrency == "USD")
            {
                float result = userCoins * currentBitcoin.bpi.USD.rate_float;
                Console.WriteLine($"Your bitcoins are worth {result} {currentBitcoin.bpi.USD.code}.");
                Console.WriteLine(currentBitcoin.disclaimer);
            }
            if (userCurrency == "EUR")
            {
                float result = userCoins * currentBitcoin.bpi.EUR.rate_float;
                Console.WriteLine($"Your bitcoins are worth {result} {currentBitcoin.bpi.EUR.code}.");
                Console.WriteLine(currentBitcoin.disclaimer);
            }
            if (userCurrency == "GBP")
            {
                float result = userCoins * currentBitcoin.bpi.GBP.rate_float;
                Console.WriteLine($"Your bitcoins are worth {result} {currentBitcoin.bpi.GBP.code}.");
                Console.WriteLine(currentBitcoin.disclaimer);
            }
            else
            {
                Console.WriteLine("Please start over.");
            }


        }

        public static BitCoinRate GetRates()
        {
            string url = "https://api.coindesk.com/v1/bpi/currentprice.json";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";

            var webResponce = request.GetResponse();
            var webStream = webResponce.GetResponseStream();

            BitCoinRate bitcoin;

            using (var responceReader = new StreamReader(webStream))
            {
                var responce = responceReader.ReadToEnd();
                bitcoin = JsonConvert.DeserializeObject<BitCoinRate>(responce);
            }

            return bitcoin;
        }
    }
}
