using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FindMyHouse
{
    class Street
    {
        public Street()
        {
            this.Flats = new HashSet<Flat>();
        }

        public Guid Id { get; set; }
        public string StreetName {get; set;}
        public string ZipCode { get; set; }
        public int BuildingNumber { get; set; }
        public virtual ICollection<Flat> Flats { get; set; }
    }
}
