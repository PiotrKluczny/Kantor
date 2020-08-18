using FluentValidation.Internal;
using Kantor.Client;
using Kantor.Controllers;
using Kantor.Interfaces;
using Kantor.Message;
using Kantor.Models;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using RestSharp.Extensions;
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
        private readonly INbpClient _nbpClient;
        private NbpCurrencyDictionare nbpCurrencyDictionare;

        public NbpLogic(INbpFile nbpFile, INbpClient nbpClient)
        {
            _nbpFile = nbpFile;
            _nbpClient = nbpClient;
        }
        public NbpCurrency GetBack(string currency, string from, string to)
        {
            

            var fromDate = DateTime.ParseExact(from, "yyyy-MM-dd", null);
            var toDate = DateTime.ParseExact(to, "yyyy-MM-dd", null);
            var result = _nbpClient.GetCurrencyRates(currency, fromDate, toDate);
            var avg = Math.Round(result.Select(x => x.bid).Average(), 4);
            var od = Math.Round(result.Select(x => x.ask).StdDev(), 4);

            var nbpCurrencyLogic = new NbpCurrency();
            nbpCurrencyLogic.Currency = currency;
            nbpCurrencyLogic.FromDate = fromDate;
            nbpCurrencyLogic.ToDate = toDate;
            nbpCurrencyLogic.Average = avg;
            nbpCurrencyLogic.Deviation = od;
            nbpCurrencyLogic.TimeStape = DateTime.Now;

            //using (var context = new NbpDbContext())
            //{
            //    context.NbpCurrencys.Add(nbpCurrencyLogic);

            //    context.SaveChanges();
            //}

            using (var context = new NbpDbContext())
            {

                context.NbpCurrencys.Add(nbpCurrencyLogic);

                context.SaveChanges();

                var exit = context.NbpCurrencyDictionares.Any(x => x.Currency == currency);

                if (exit)
                {
                    _nbpFile.SaveFile(nbpCurrencyLogic);

                }
                else
                {
                    Console.WriteLine( "Ups, coś poszło nie tak");
                    
                }
            }

            //List<string> currencyList = new List<string>();

            //currencyList.Add("AUD");
            //currencyList.Add("CAD");
            //currencyList.Add("HUF");
            //currencyList.Add("CHF"); 
            //currencyList.Add("GBP");
            //currencyList.Add("JPY");
            //currencyList.Add("CZK");
            //currencyList.Add("DKK");
            //currencyList.Add("NOK");
            //currencyList.Add("SEK");

            //int ite = 0;

            //foreach (var item in currencyList)
            //{
            //    NbpCurrencyDictionare nbpCurrencyDictionare = new NbpCurrencyDictionare()
            //    {
            //        Currency = item
            //    };
            //    NbpCurrencyLogic nbpCurrencyLogicA = new NbpCurrencyLogic();
            //    nbpCurrencyLogicA.SaveCurrencyValues(nbpCurrencyDictionare);
            //    ite++;
            //}
            //_nbpFile.SaveFile(nbpCurrencyLogic);

            return nbpCurrencyLogic;
        
        }
    }

   

}
