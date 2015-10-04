namespace CatalogueWithXPath
{
    using System;
    using System.Collections.Generic;
    using System.Xml;

    public class Program
    {
        private const string XmlPath = "../../../catalogue.xml";

        public static void Main()
        {
            XmlDocument doc = new XmlDocument();

            try
            {
                doc.Load(XmlPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            string query = "/catalogue/album/artist";

            XmlNodeList artistsList = doc.SelectNodes(query);

            var artists = FindUniqueArtists(artistsList);

            Print(artists);
        }

        private static Dictionary<string, int> FindUniqueArtists(XmlNodeList nodeList)
        {
            Dictionary<string, int> artists = new Dictionary<string, int>();

            foreach (XmlNode node in nodeList)
            {
                if (node.Name == "artist")
                {
                    string key = node.InnerText;
                    if (artists.ContainsKey(key))
                    {
                        ++artists[key];
                    }
                    else
                    {
                        artists.Add(key, 1);
                    }
                }
            }

            return artists;
        }

        private static void Print(Dictionary<string, int> artists)
        {
            Console.WriteLine("***Using xPath***");
            foreach (var artist in artists)
            {
                Console.WriteLine("Artists: {0} -->  Albums: {1}", artist.Key, artist.Value);
            }
        }
    }
}
