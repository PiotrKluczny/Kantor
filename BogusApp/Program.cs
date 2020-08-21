using Bogus;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace BogusApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello You!");

            var list = GetPersons();

            Export(list);
            var inportList = Import();

            using (var context = new BogusDbContext())
            {
                context.Persons.AddRange(inportList);
                context.SaveChanges();
            }

        }
        static string pathName = $"C:\\Database\\BogusAppData{Guid.NewGuid()}.csv";

        static void Export(IEnumerable<Person> persons)
        {

            using (var writer = new StreamWriter(pathName, true))
            {
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    csv.Configuration.RegisterClassMap<ProductMap>();
                    csv.Configuration.Delimiter = ";";

                    csv.WriteRecords(persons);
                }
            }
        }

        static IEnumerable<Person> Import()
        {
            using(var reader = new StreamReader(pathName))
            {
                using (var csvR = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    csvR.Configuration.RegisterClassMap<ProductMap>();
                    csvR.Configuration.Delimiter = ";";

                        return csvR.GetRecords<Person>().ToList();

                }
            }
        }

        static IEnumerable<Person> GetPersons()
        {
            return new Faker<Person>()
                .RuleFor(x => x.FirstName, (f, x) => f.Name.FirstName((Bogus.DataSets.Name.Gender)x.Gender))
                .RuleFor(x => x.LastName, (f, x) => f.Name.LastName((Bogus.DataSets.Name.Gender)x.Gender))
                .RuleFor(x => x.FullName, (f, x) => x.FirstName + " " + x.LastName)
                .RuleFor(x => x.Gender, (f, x) => f.PickRandom<Gender>())
                .RuleFor(x => x.Age, f => f.Random.Int(0,90))
                .RuleFor(x => x.EyeColor, f => f.PickRandom<EyeColor>())
                .RuleFor(x => x.HairColor, f => f.Commerce.Color())
                .RuleFor(x => x.PersonId, f => f.Random.Number(1, 1000))
                .RuleFor(x => x.Country, f => f.Address.Country())
                .RuleFor(x => x.City, f => f.Address.City())
                .RuleFor(x => x.State, f => f.Address.State())
                .RuleFor(x => x.ZipCode, f => f.Address.ZipCode())
                .RuleFor(x => x.Email, (f, x) => f.Internet.Email(x.FirstName, x.LastName))
                .RuleFor(x => x.Phone, f => f.Phone.PhoneNumber())
                .RuleFor(x => x.UserName, (f, x) => f.Internet.UserName(x.FirstName, x.LastName))
                .RuleFor(x => x.Company, f => f.Company.CompanyName())
                .RuleFor(x => x.Position, f => f.Name.JobTitle())
                .RuleFor(x => x.Salary, f => f.Random.Int(1000, 10000))
                .RuleFor(x => x.CompanyEmail, (f, g) => f.Internet.Email(g.LastName, g.Company))
                .RuleFor(x => x.ComapnyPhone, f => f.Phone.PhoneNumber())
                .Generate(2000);
        }

    }
}
