namespace BookStore.Data
{
    using System.Data.Entity;
    using BookStore.Data.Migrations;
    using BookStore.Models;

    public class BookStoreDbContext : DbContext
    {
        public BookStoreDbContext()
            : base("BookStoreConnection")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<BookStoreDbContext, Configuration>());
        }

        public IDbSet<Book> Books { get; set; }

        public IDbSet<Author> Authors { get; set; }

        public IDbSet<Review> Reviews { get; set; }
    }
}
