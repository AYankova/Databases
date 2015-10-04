namespace CatalogueWithDomParser
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

            XmlElement rootNode = doc.DocumentElement;

            var artists = FindUniqueArtists(rootNode);

            Print(artists);
        }

        private static Dictionary<string, int> FindUniqueArtists(XmlElement rootNode)
        {
            Dictionary<string, int> artists = new Dictionary<string, int>();

            foreach (XmlNode node in rootNode.ChildNodes)
            {
                foreach (XmlNode childNode in node.ChildNodes)
                {
                    if (childNode.Name == "artist")
                    {
                        string key = childNode.InnerText;
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
            }

            return artists;
        }

        private static void Print(Dictionary<string, int> artists)
        {
            Console.WriteLine("***Using DOM Parser***");
            foreach (var artist in artists)
            {
                Console.WriteLine("Artists: {0} -->  Albums: {1}", artist.Key, artist.Value);
            }
        }
    }
}
