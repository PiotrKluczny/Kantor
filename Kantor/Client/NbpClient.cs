using Kantor.Interfaces;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using static Kantor.Client.ResponseTable.NbpClient;

namespace Kantor.Client
{
    public class RateTable
    {
        public string no { get; set; }
        public string effectiveDate { get; set; }
        public double bid { get; set; }
        public double ask { get; set; }
    }

    public class ResponseTable
    {
        public string table { get; set; }
        public string currency { get; set; }
        public string code { get; set; }
        public List<RateTable> rates { get; set; }


        public class NbpClient : INbpClient
        {

            // mamy tu do czynienia z RestSharp czyli wywolanie rest api 
            // mamy metode ktora zwraca nam liste "rates" czyli tego co otrzymujemy do wyslaniu rządania 
            // a w parametrach mamy rzeczy o ktore chcemy sie zapytac 
            public List<RateTable> GetCurrencyRates(string currency, DateTime fromDate, DateTime toDate)
            {
                //tworzymy klienta z adresem url
                var client = new RestClient("http://api.nbp.pl/");

                //tworzymy zapytanie 
                var request = new RestRequest("api/exchangerates/rates/{table}/{code}/{startDate}/{endDate}/")
                    //addurlsegment zamienia nam poszczegolne skladowe {} na parametry 
                    .AddUrlSegment("table", "C")
                    .AddUrlSegment("code", currency)
                    .AddUrlSegment("startDate", fromDate.ToString("yyyy-MM-dd"))
                    .AddUrlSegment("endDate", toDate.ToString("yyyy-MM-dd"));

                var response = client.Get<ResponseTable>(request);

                return response.Data.rates;
            }

            public class Rate
            {
                public string currency { get; set; }
                public string code { get; set; }
                public double bid { get; set; }
                public double ask { get; set; }
            }

            public class MyArray
            {
                public string table { get; set; }
                public string no { get; set; }
                public string tradingDate { get; set; }
                public string effectiveDate { get; set; }
                public List<Rate> rates { get; set; }
            }

       
            public List<string> GetReloadDictionary()
            {
                var client = new RestClient("http://api.nbp.pl/");
                var request = new RestRequest("api/exchangerates/tables/C");

                var response = client.Get<List<MyArray>>(request);

                return response.Data.First().rates.Select(x => x.code).ToList();
            }
        }
    }
}
