using Bogus;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace FindMyHouse
{
    class Program
    {
        static void Main(string[] args)
        {
            var listOfStreets = GetStreets();

            using (var context = new FindMyHouseDbContext())
            {
                context.Streets.AddRange(listOfStreets);
                context.SaveChanges();
            }

           var result = GetResidentsFromDB();
            Export(result);

        }

        static IEnumerable<Street> GetStreets()
        {
            var streets = new Faker<Street>()
            .RuleFor(x => x.StreetName, f => f.Address.StreetName())
            .RuleFor(x => x.ZipCode, f => f.Address.ZipCode())
            .RuleFor(x => x.BuildingNumber, f => f.Random.Int(1, 200))
            .RuleFor(x =>x.Flats,GetFlats().ToList())
            .Generate(50)
            .ToList();

            return streets;
        }

        static IEnumerable<Flat> GetFlats()
        {
            var flats =new Faker<Flat>()
                .RuleFor(x => x.NumberOfFlat, f => f.Random.Int(1, 99))
                .RuleFor(x=>x.Residents, GetResidents())
                .Generate(10)
                .ToList();

            return flats;
        }

        static IEnumerable<Resident> GetResidents()
        {
            var residents = 
            new Faker<Resident>()
                .RuleFor(x => x.FirstName, (f, x) => f.Name.FirstName((Bogus.DataSets.Name.Gender)x.Gender))
                .RuleFor(x => x.LastName, (f, x) => f.Name.LastName((Bogus.DataSets.Name.Gender)x.Gender))
                .RuleFor(x => x.Gender, f => f.PickRandom<Gender>())
                .RuleFor(x => x.Age, f => f.Random.Int(1, 99))
                .Generate(10)
                .ToList();
            return residents;
        }

        static void Export(IEnumerable<Resident> residents)
        {

            string path = @"C:\Database\Residents.csv";

            using (var streamWriter = File.CreateText(path))
            {
                var writer = new CsvWriter(streamWriter, CultureInfo.InvariantCulture);

                writer.WriteRecord(residents);
            }
        }

        static IEnumerable<Resident> GetResidentsFromDB()
        {
            using(var context = new FindMyHouseDbContext())
            {
                
                return context.Residents.ToList();
            }

        }



    }
}
