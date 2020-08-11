using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Kantor.Client;
using Kantor.Logic;
using Kantor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Kantor.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NbpController : ControllerBase
    {
        

        private readonly ILogger<NbpController> _logger;

        public NbpController(ILogger<NbpController> logger)
        {
            _logger = logger;
        }

        [HttpGet("{currency}/{from}/{to}")]
        public IActionResult Get(string currency, string from, string to)
        {
            // zadanie domowe:
            // wyniesc do klasy ale proces maganer***
            // zapisac wynik do pliku json (wszystko do jednego)***
            // zalogowac serilogiem
            // i zapisac do bazy***
            // i zwrocic odpowiedz tutaj w requescie 
            // napisac iterface proces magaere i wstrzyknąc go jako zaleznosc do kontrolera 
            // stworzyc interfaje nbp clienta i wstrzyknac go jako zaleznosc do kontrolera.

            //var fromDate = DateTime.ParseExact(from, "yyyy-MM-dd", null);
            //var toDate = DateTime.ParseExact(to, "yyyy-MM-dd", null);

            //var client = new NbpClient();

            //var result = client.GetCurrencyRates(currency, fromDate, toDate);

            //var avg = Math.Round(result.Select(x => x.bid).Average(), 4);

            //var od = Math.Round(result.Select(x => x.ask).StdDev(), 4);

            //var nbpcurrency = new NbpCurrency();

            //nbpcurrency.Currency = currency;
            //nbpcurrency.FromDate = fromDate;
            //nbpcurrency.ToDate = toDate;
            //nbpcurrency.Average = avg;
            //nbpcurrency.Deviation = od;
            //nbpcurrency.TimeStape = DateTime.Now;

            //using (var context = new NbpDbContext())
            //{
            //    context.NbpCurrencys.Add(nbpcurrency);
            //    context.SaveChanges();
            //}

            var nbpcurrency = new NbpLogic();

            var result = nbpcurrency.GetBack(currency, from, to);

            return Ok(result);
        }


    }

    public static class Extensions
    {
        public static double StdDev(this IEnumerable<double> values)
        {
            double ret = 0;
            int count = values.Count();
            if (count > 1)
            {
                double avg = values.Average();

                double sum = values.Sum(d => (d - avg) * (d - avg));

                ret = Math.Sqrt(sum / count);
            }
            return ret;
        }
    }


}
