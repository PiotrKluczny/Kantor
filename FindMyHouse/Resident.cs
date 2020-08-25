using System;
using System.Collections.Generic;
using System.Text;

namespace FindMyHouse
{
    class Resident
    {
        public Guid ResidentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public int Age { get; set; }
        public virtual Flat Flat { get; set; }
    }

    public enum Gender
    {
        Male = 0,
        Femal = 1
    }
}
