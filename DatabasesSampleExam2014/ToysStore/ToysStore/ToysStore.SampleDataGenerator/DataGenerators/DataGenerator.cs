namespace ToysStore.SampleDataGenerator.DataGenerators
{
    using Contracts;
    using Data;

    internal abstract class DataGenerator : IDataGenerator
    {
        private readonly IRandomDataGenerator random;
        private readonly ToysStoreEntities db;
        private readonly int count;

        public DataGenerator(IRandomDataGenerator randomGenerator, ToysStoreEntities toysStoreEntities, int countOfGeneratedObjects)
        {
            this.random = randomGenerator;
            this.db = toysStoreEntities;
            this.count = countOfGeneratedObjects;
        }

        protected IRandomDataGenerator Random
        {
            get { return this.random; }
        }

        protected ToysStoreEntities Database
        {
            get { return this.db; }
        }

        protected int Count
        {
            get { return this.count; }
        }

        public abstract void Generate();
    }
}
