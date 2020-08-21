using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace BogusApp
{
    class ProductMap : ClassMap<Person>
    {
        public ProductMap()
        {
             
            Map(x => x.FirstName).Name("FirstName");
            Map(x => x.LastName).Name("Last Name");
            Map(x => x.FullName).Name("Full Name");
            Map(x => x.Gender).Name("Gender");
            Map(x => x.Age).Name("Age");
            Map(x => x.EyeColor).Name("Eye Color");
            Map(x => x.HairColor).Name("Hair Color");
            Map(x => x.PersonId).Name("Person Id");
            Map(x => x.Country).Name("Country");
            Map(x => x.City).Name("City");
            Map(x => x.State).Name("State");
            Map(x => x.ZipCode).Name("Zip Code");
            Map(x => x.Email).Name("Email");
            Map(x => x.Phone).Name("Phone");
            Map(x => x.UserName).Name("User Name");
            Map(x => x.Company).Name("Company");
            Map(x => x.Position).Name("Position");
            Map(x => x.Salary).Name("Salary");
            Map(x => x.CompanyEmail).Name("Company Email");
            Map(x => x.ComapnyPhone).Name("Company Phone");
        }
    }
}
