using Kantor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kantor.Interfaces
{
    public interface INbpLogic
    {
        public NbpCurrency GetBack(string currency, string from, string to);
        
    }
}
