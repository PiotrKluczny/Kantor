using System;
using System.Collections.Generic;
using System.Text;

namespace FindMyHouse
{
    class Flat
    {
        public Flat()
        {
            this.Residents = new HashSet<Resident>();
        }
        
        public Guid FlatId { get; set; }
        public int NumberOfFlat { get; set; }
        public virtual Street Street { get; set; }
        public virtual ICollection<Resident> Residents { get; set; }
    }
}
