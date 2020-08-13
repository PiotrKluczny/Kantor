using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Kantor.Client;
using Kantor.Interfaces;
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
        private readonly INbpLogic _nbpLogic;

        public NbpController(ILogger<NbpController> logger, INbpLogic nbpLogic)
        {
            _logger = logger;
            _nbpLogic = nbpLogic;
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

           var result = _nbpLogic.GetBack(currency, from, to);

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
