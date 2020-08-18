using Kantor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kantor.Logic
{
    public class NbpCurrencyLogic
    {
        public void SaveCurrencyValues(NbpCurrencyDictionare nbpCurrencyDictionare)

        {
            using (var context = new NbpDbContext())
            {
                context.NbpCurrencyDictionares.Add(nbpCurrencyDictionare);
                context.SaveChanges();
            }
        }
    }
}
         