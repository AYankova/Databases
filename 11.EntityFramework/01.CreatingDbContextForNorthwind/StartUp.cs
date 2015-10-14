namespace CreatingDbContextForNorthwind
{
    using System;
    using System.Linq;

    public class StartUp
    {
        public static void Main()
        {
            using (var db = new NorthwindEntities())
            {
                var regions = db.Regions.Select(r => r.RegionDescription).ToList();
                regions.ForEach(Console.WriteLine);
            }
        }
    }
}
