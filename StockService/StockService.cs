using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Newtonsoft.Json.Linq;

namespace Interview.StockService
{
    public class StockService : IStockService
    {
        const string _url = "https://www.alphavantage.co/query?apikey={apiKey}&function=TIME_SERIES_DAILY_ADJUSTED&symbol=";

        public string GetStockPrice(string symbol, int nDay, string apiKey)
        {
            var stockDetailJson = getResponse(symbol, apiKey);

            var data = getNDay(stockDetailJson, nDay);
            var average = data.Average();
            var result = getResponseString(symbol, data, average);

            return result;
        }

        private string getResponseString(string symbol, List<double> data, double average)
        {
            return symbol + " data=[" + string.Join(", ", data) + "], average=" + Math.Round(average, 2).ToString();
        }

        private JObject getResponse(string symbol, string apiKey)
        {
            string result = string.Empty;
            string stockUrl = _url.Replace("{apiKey}", apiKey) + symbol;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(stockUrl);

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                result = reader.ReadToEnd();
            }

            result = new StringBuilder(result).ToString();
            return JObject.Parse(result);
        }

        private List<double> getNDay(JObject stockDetailJson, int nDay)
        {
            var result = new List<double>();

            foreach (var dailyPrice in stockDetailJson["Time Series (Daily)"])
            {
                var price = dailyPrice.Children()["4. close"].Values<string>().First();
                result.Add(double.Parse(price));
                nDay--;
                if (nDay == 0)
                    break;
            }

            return result;
        }
    }
}
