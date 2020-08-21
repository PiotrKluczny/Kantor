using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BogusApp
{
    [Table("Persons")]
    public class Person
    {
        public Person()
        {
            Id = Guid.NewGuid();
        }

        [Key]
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public Gender Gender { get; set; }
        public int Age { get; set; }
        public EyeColor EyeColor { get; set; }
        public string HairColor { get; set; }
        public int PersonId { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string UserName { get; set; }
        public string Company { get; set; }
        public string Position { get; set; }
        public decimal Salary { get; set; }
        public string CompanyEmail { get; set; }
        public string ComapnyPhone { get; set; }
    }

    public enum Gender
    {
        Male = 0,
        Femal = 1
    }

    public enum EyeColor
    {
        green = 0,
        blue = 1,
        brown = 2,
        hazel = 3,
        gray = 4
    }
}
