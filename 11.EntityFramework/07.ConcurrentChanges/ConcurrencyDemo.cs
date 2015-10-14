namespace ConcurrentChanges
{
    using System;
    using System.Linq;
    using CreatingDbContextForNorthwind;

    public class ConcurrencyDemo
    {
        public static void Main()
        {
            using (var firstDb = new NorthwindEntities())
            {
                Console.WriteLine("First connection established.");
                var firstCategory = firstDb.Categories.FirstOrDefault();

                Console.WriteLine("Initial first category name: {0}", firstCategory.CategoryName);

                firstCategory.CategoryName = "alcohol only";
                Console.WriteLine("Category name set to: {0}", firstCategory.CategoryName);

                using (var secondDb = new NorthwindEntities())
                {
                    Console.WriteLine("Second connection established.");
                    var secondCategory = secondDb.Categories.FirstOrDefault();

                    Console.WriteLine("Initial first category name: {0}", secondCategory.CategoryName);

                    secondCategory.CategoryName = "nonalcohol only";
                    Console.WriteLine("Category name set to: {0}", secondCategory.CategoryName);

                    firstDb.SaveChanges();
                    secondDb.SaveChanges();

                    Console.WriteLine("Changes have been saved.");
                }
            }

            using (var db = new NorthwindEntities())
            {
                var category = db.Categories.FirstOrDefault();

                Console.WriteLine("What is actually saved in the database:");
                Console.WriteLine("First category name: {0}", category.CategoryName);
            }
        }
    }
}
