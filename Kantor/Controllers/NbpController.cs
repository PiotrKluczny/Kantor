using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Kantor.Client;
using Kantor.Interfaces;
using Kantor.Logic;
using Kantor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Serilog;

namespace Kantor.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NbpController : ControllerBase
    {
        private readonly ILogger<NbpController> _logger;
        private readonly INbpLogic _nbpLogic;
        private readonly INbpCurrencyLogic _nbpCurrencyLogic;

        public NbpController(ILogger<NbpController> logger, INbpLogic nbpLogic, INbpCurrencyLogic nbpCurrencyLogic)
        {
            _logger = logger;
            _nbpLogic = nbpLogic;
            _nbpCurrencyLogic = nbpCurrencyLogic;
        }

        [HttpGet("{currency}/{from}/{to}")]
        public IActionResult Get(string currency, string from, string to)
        {

           var result = _nbpLogic.GetBack(currency, from, to);
            _logger.LogInformation("Test logging");
           // Log.Information("information level");
            return Ok(result);
        }

        [HttpGet("reload")]
        
        public void ReloadCurrencyValue()
        {
            _logger.LogInformation("DUPA");
             _nbpCurrencyLogic.SaveCurrencyValues();
           
        }


    }


}
