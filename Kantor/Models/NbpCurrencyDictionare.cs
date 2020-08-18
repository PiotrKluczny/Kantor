using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Kantor.Models
{
    public class NbpCurrencyDictionare
    {
        [Key]
        public int Id { get; set; }
        public string Currency { get; set; }

    }
}
