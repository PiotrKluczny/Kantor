﻿using FluentValidation.Internal;
using Kantor.Client;
using Kantor.Controllers;
using Kantor.Interfaces;
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
       

        public NbpLogic(INbpFile nbpFile, INbpClient nbpClient, INbpClient getReloadDictionary)
        {
            _nbpFile = nbpFile;
            _nbpClient = nbpClient;
            _nbpClient = getReloadDictionary;
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

            _nbpFile.SaveFile(nbpCurrencyLogic);

            return nbpCurrencyLogic;
        
        }
    }

   

}
