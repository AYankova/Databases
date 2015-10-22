namespace BookStore.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public sealed class Configuration : DbMigrationsConfiguration<BookStoreDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = false;
            this.ContextKey = "BookStore.Data.BookStoreDbContext";
        }

        protected override void Seed(BookStoreDbContext context)
        {
        }
    }
}
