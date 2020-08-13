using Kantor.Client;
using Kantor.Controllers;
using Kantor.Interfaces;
using Kantor.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Kantor.Logic
{
    public class NbpLogic : INbpLogic
    {
        private readonly INbpFile _nbpFile;

        public NbpLogic(INbpFile nbpFile)
        {
            _nbpFile = nbpFile;
        }
        public NbpCurrency GetBack(string currency, string from, string to)
        {
            var fromDate = DateTime.ParseExact(from, "yyyy-MM-dd", null);
            var toDate = DateTime.ParseExact(to, "yyyy-MM-dd", null);

            var client = new NbpClient();

            var result = client.GetCurrencyRates(currency, fromDate, toDate);

            var avg = Math.Round(result.Select(x => x.bid).Average(), 4);

            var od = Math.Round(result.Select(x => x.ask).StdDev(), 4);

            var nbpCurrencyLogic = new NbpCurrency();

            nbpCurrencyLogic.Currency = currency;
            nbpCurrencyLogic.FromDate = fromDate;
            nbpCurrencyLogic.ToDate = toDate;
            nbpCurrencyLogic.Average = avg;
            nbpCurrencyLogic.Deviation = od;
            nbpCurrencyLogic.TimeStape = DateTime.Now;

            using (var context = new NbpDbContext())
            {
                context.NbpCurrencys.Add(nbpCurrencyLogic);

                context.SaveChanges();
            }

            //string filePath = @"C:\LocalRepository\Kantor\jsonFile.txt";

            //string json = JsonConvert.SerializeObject(nbpCurrencyLogic);

            //if (!File.Exists(filePath))
            //{
            //    using (File.Create(filePath)) { };

            //    File.WriteAllText(filePath, json);
            //}
            //else
            //{
            //    string resultA = json;

            //    using (StreamReader streamReader = File.OpenText(filePath))
            //    {
            //        ;
            //        while ((json = streamReader.ReadLine()) == null)
            //        {
            //            resultA += json;
            //        }
            //    }
            //    File.AppendAllText(filePath, resultA);
            //}

            _nbpFile.SaveFile(nbpCurrencyLogic);


            return nbpCurrencyLogic;
        }
    }

   

}
