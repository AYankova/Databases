namespace ToysStore.SampleDataGenerator.DataGenerators
{
    using System;
    using System.Collections.Generic;
    using Contracts;
    using Data;

    internal class ManufacturerDataGenerator : DataGenerator, IDataGenerator
    {
        public ManufacturerDataGenerator(IRandomDataGenerator randomGenerator, ToysStoreEntities toysStoreEntities, int countOfGeneratedObjects)
            : base(randomGenerator, toysStoreEntities, countOfGeneratedObjects)
        {
        }

        public override void Generate()
        {
            var manufacturersToBeAdded = new HashSet<string>();

            while (manufacturersToBeAdded.Count != this.Count)
            {
                manufacturersToBeAdded.Add(this.Random.GetRandomStringWithRandomLength(5, 50));
            }

            int index = 0;
            Console.WriteLine("Adding manufacturers");
            foreach (var manufacturerName in manufacturersToBeAdded)
            {
                var manufacturer = new Manufacturer
                {
                    Name = manufacturerName,
                    Country = this.Random.GetRandomStringWithRandomLength(2, 100)
                };

                if (index % 100 == 0)
                {
                    Console.Write(".");
                    this.Database.SaveChanges();
                }

                this.Database.Manufacturers.Add(manufacturer);
                index++;
            }

            Console.WriteLine("\nManufacturers added");
        }
    }
}
