using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Kantor.Models
{
    public class NbpCurrency
    {
        [Key]
        public int Id { get; set; }
        public string Currency { get; set; }
        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public double Average { get; set; }

        public double Deviation { get; set; }

        public DateTime TimeStape { get; set; }

    }
}
