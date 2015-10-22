namespace ToysStore.SampleDataGenerator.DataGenerators
{
    using System;
    using Contracts;
    using ToysStore.Data;

    internal class CategoryDataGenerator : DataGenerator, IDataGenerator
    {
        public CategoryDataGenerator(IRandomDataGenerator randomGenerator, ToysStoreEntities toysStoreEntities, int countOfGeneratedObjects)
            : base(randomGenerator, toysStoreEntities, countOfGeneratedObjects)
        {
        }

        public override void Generate()
        {
            Console.WriteLine("Adding categories");
            for (int i = 0; i < this.Count; i++)
            {
                var category = new Category
                {
                    Name = this.Random.GetRandomStringWithRandomLength(5, 50)
                };

                this.Database.Categories.Add(category);

                if (i % 100 == 0)
                {
                    Console.Write(".");
                    this.Database.SaveChanges();
                }
            }

            Console.WriteLine("\nCategories added");
        }
    }
}
