namespace ExtractPricesWithLinq
{
    using System;
    using System.Linq;
    using System.Xml;
    using System.Xml.Linq;

    public class Program
    {
        private const string Path = "../../../catalogue.xml";

        public static void Main()
        {
            var doc = XDocument.Load(Path);
            var albums = doc.Descendants("album");
            var prices = from album in albums
                         where int.Parse(album.Element("year").Value) < 2000
                         select album.Element("price").Value;

            foreach (var price in prices)
            {
                Console.WriteLine(price);
            }
        }
    }
}
