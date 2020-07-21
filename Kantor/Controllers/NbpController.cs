using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Kantor.Client;
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

            var fromDate = DateTime.ParseExact(from, "yyyy-MM-dd", null);
            var toDate = DateTime.ParseExact(to, "yyyy-MM-dd", null);

            var client = new NbpClient();
   
            var result = client.GetCurrencyRates(currency, fromDate, toDate);

            var avg = Math.Round(result.Select(x => x.bid).Average(),4);

            var od = Math.Round(result.Select(x => x.ask).StdDev(),4);

            var nbpcurrency = new NbpCurrency();

            nbpcurrency.Currency = currency;
            nbpcurrency.FromDate = fromDate;
            nbpcurrency.ToDate = toDate;
            nbpcurrency.Average = avg;
            nbpcurrency.Deviation = od;
            nbpcurrency.TimeStape = DateTime.Now;

            using (var context = new NbpDbContext())
            {
                context.NbpCurrencys.Add(nbpcurrency);
                context.SaveChanges();
            }


            return Ok(nbpcurrency);
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
                //Compute the Average
                double avg = values.Average();

                //Perform the Sum of (value-avg)^2
                double sum = values.Sum(d => (d - avg) * (d - avg));

                //Put it all together
                ret = Math.Sqrt(sum / count);
            }
            return ret;
        }
    }


}
