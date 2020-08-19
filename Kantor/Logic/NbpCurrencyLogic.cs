using Kantor.Interfaces;
using Kantor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Kantor.Client.ResponseTable;

namespace Kantor.Logic
{
    public class NbpCurrencyLogic : INbpCurrencyLogic
    {
 
        public void SaveCurrencyValues()

        {
            var nbpClient = new NbpClient();

            List<string> list = nbpClient.GetReloadDictionary();

            using (var context = new NbpDbContext())
            {
                
                var toRemove = context.NbpCurrencyDictionares.ToList();
                //usuwamy wszytskie rekodry z bazy danych dajac liste obiektow z NbpCurrencyDictionares
                context.NbpCurrencyDictionares.RemoveRange(toRemove);
                context.SaveChanges();

                //dodajemy liste obiektow do tabeli 
                var toSave = list.Select(x => new NbpCurrencyDictionare { Currency = x, Id = Guid.NewGuid() });

                context.NbpCurrencyDictionares.AddRange(toSave);
                context.SaveChanges();
            }
        }
    }
}
         