namespace Cars.JsonImporterSecondVersion
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Cars.Data;
    using Cars.JsonImporterSecondVersion.Models;
    using Cars.Models;
    using Newtonsoft.Json;

    public static class JsonImporter
    {
        public static void Import()
        {
            var carsToAdd = Directory
                          .GetFiles(Directory.GetCurrentDirectory() + "/JsonFiles/")
                          .Where(f => f.EndsWith("json"))
                          .Select(f => File.ReadAllText(f))
                          .SelectMany(str => JsonConvert.DeserializeObject<IEnumerable<JsonCar>>(str))
                          .ToList();

            var addedCities = new HashSet<string>();
            var addedManufacturers = new HashSet<string>();

            var addedCars = 0;
            var db = new CarsDbContext();
            db.Configuration.AutoDetectChangesEnabled = false;
            Console.WriteLine("Adding cars");
            foreach (var car in carsToAdd)
            {
                var cityName = car.Dealer.City;
                if (!addedCities.Contains(car.Dealer.City))
                {
                    var city = new City
                    {
                        Name = cityName
                    };

                    db.Cities.Add(city);
                    db.SaveChanges();
                    addedCities.Add(cityName);
                }

                var manufacturer = car.ManufacturerName;

                if (!addedManufacturers.Contains(car.ManufacturerName))
                {
                    var newManufacturer = new Manufacturer
                    {
                        Name = manufacturer
                    };
                    addedManufacturers.Add(manufacturer);
                    db.Manufacturers.Add(newManufacturer);
                    db.SaveChanges();
                }

                var dealerToAdd = new Cars.Models.Dealer
                {
                    Name = car.Dealer.Name
                };

                var dbCity = db.Cities
                    .FirstOrDefault(c => c.Name == cityName);
                dealerToAdd.Cities.Add(dbCity);

                var dbManufacturer = db.Manufacturers
                    .FirstOrDefault(m => m.Name == car.ManufacturerName);

                var carToAdd = new Car
                {
                    Manufacturer = dbManufacturer,
                    Dealer = dealerToAdd,
                    Model = car.Model,
                    Price = car.Price,
                    TransmissionType = car.TransmissionType,
                    Year = (short)car.Year
                };

                db.Cars.Add(carToAdd);
                if (addedCars % 100 == 0)
                {
                    Console.Write(".");
                    db.SaveChanges();
                    db.Dispose();
                    db = new CarsDbContext();
                    db.Configuration.AutoDetectChangesEnabled = false;
                }

                addedCars++;
            }

            db.SaveChanges();
            db.Configuration.AutoDetectChangesEnabled = true;
        }
    }
}
