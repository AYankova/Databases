namespace FindAllSales
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using CreatingDbContextForNorthwind;

    public class FindAllSalesDemo
    {
        public static void Main()
        {
            var sales = FindAllSalesByRegionAndPeriod("ID", new DateTime(1997, 1, 1), new DateTime(1998, 1, 1));

            foreach (var sale in sales)
            {
                Console.WriteLine(
                        string.Format(
                        "OrderID: {0}, ShipRegion: {1}, OrderDate: {2}, ShippedDate: {3}",
                        sale.OrderID, 
                        sale.ShipRegion, 
                        sale.OrderDate.Value.ToString("yyyy-MM-dd"), 
                        sale.ShippedDate.Value.ToString("yyyy-MM-dd")));
            }
        }

        private static ICollection<Order> FindAllSalesByRegionAndPeriod(string region, DateTime start, DateTime end)
        {
            using (var db = new NorthwindEntities())
            {
                var sales = db.Orders
                            .Where(o => o.ShipRegion == region)
                            .Where(o => o.OrderDate > start && o.ShippedDate < end)
                            .ToList();

                return sales;
            }
        }
    }
}
