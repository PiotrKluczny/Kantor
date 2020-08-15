using Kantor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kantor.Interfaces
{
    public interface INbpFile
    {
         void SaveFile(NbpCurrency nbpCurrencyLogic);
    }
}
