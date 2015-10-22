namespace ToysStore.SampleDataGenerator.DataGenerators
{
    using System;
    using Contracts;
    using Data;

    internal class AgeRangeDataGenerator : DataGenerator, IDataGenerator
    {
        public AgeRangeDataGenerator(IRandomDataGenerator randomGenerator, ToysStoreEntities toysStoreEntities, int countOfGeneratedObjects)
            : base(randomGenerator, toysStoreEntities, countOfGeneratedObjects)
        {
        }

        public override void Generate()
        {
            int count = 0;
            Console.WriteLine("Adding age ranges");

            for (int i = 0; i < this.Count / 5; i++)
            {
                for (int j = i + 1; j <= i + 5; j++)
                {
                    var ageRange = new AgeRanx
                    {
                        MinimumAge = i,
                        MaximumAge = j
                    };

                    this.Database.AgeRanges.Add(ageRange);
                    count++;

                    if (count % 100 == 0)
                    {
                        Console.Write(".");
                        this.Database.SaveChanges();
                    }

                    if (count == this.Count)
                    {
                        Console.WriteLine("\nAge ranges added");
                        return;
                    }
                }
            }
        }
    }
}
