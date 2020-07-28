using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Kantor.Client
{

    public class Rate
    {
        public string no { get; set; }
        public string effectiveDate { get; set; }
        public double bid { get; set; }
        public double ask { get; set; }

    }

    public class Response
    {
        public string table { get; set; }
        public string currency { get; set; }
        public string code { get; set; }
        public List<Rate> rates { get; set; }

    }
    public class NbpClient
    {
        // mamy tu do czynienia z RestSharp czyli wywolanie rest api 
        // mamy metode ktora zwraca nam liste "rates" czyli tego co otrzymujemy do wyslaniu rzadania 
        // a w parametrach mamy rzeczy o ktore chcemy sie zapytac 
        public List<Rate> GetCurrencyRates(string currency, DateTime fromDate, DateTime toDate)
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

            var response = client.Get<Response>(request);

            return response.Data.rates;
        }

    }
}
