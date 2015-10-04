namespace ExtractPrices
{
    using System;
    using System.Xml;

    public class Program
    {
        private const string Path = "../../../catalogue.xml";

        public static void Main()
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(Path);

            var query = "/catalogue/album/price[../year<2000]";

            XmlNodeList prices = doc.SelectNodes(query);

            foreach (XmlNode price in prices)
            {
                Console.WriteLine(price.InnerText);
            }
        }
    }
}
