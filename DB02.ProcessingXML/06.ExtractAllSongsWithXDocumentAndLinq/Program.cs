namespace ExtractAllSongsWithXDocumentAndLinq
{
    using System;
    using System.Linq;
    using System.Xml.Linq;

    public class Program
    {
        private const string Path = "../../../catalogue.xml";

        public static void Main()
        {
            XDocument doc = XDocument.Load(Path);
            var albums = doc.Descendants("album");

            var titles = from title in albums.Descendants("title")
                         select title.Value;

            foreach (var title in titles)
            {
                Console.WriteLine(title);
            }
        }
    }
}
