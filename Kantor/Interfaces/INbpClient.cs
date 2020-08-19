using Kantor.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kantor.Interfaces
{
    public interface INbpClient
    {
         List<RateTable> GetCurrencyRates(string currency, DateTime fromDate, DateTime toDate);
         List<string> GetReloadDictionary();
    }
}
