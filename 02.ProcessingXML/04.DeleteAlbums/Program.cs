namespace DeleteAlbums
{
    using System;
    using System.Collections.Generic;
    using System.Xml;

    public class Program
    {
        private const string Path = "../../../catalogue.xml";
        private const string NewPath = "../../updatedCatalogue.xml";

        public static void Main()
        {
            XmlDocument doc = new XmlDocument();

            try
            {
                doc.Load(Path);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            XmlElement rootNode = doc.DocumentElement;
            XmlNodeList priceList = doc.GetElementsByTagName("price");

            var albumsToDelete = FilterAlbumsByPriceGreaterThan20(priceList);

            foreach (var album in albumsToDelete)
            {
                rootNode.RemoveChild(album);
            }

            doc.Save(NewPath);
            Console.WriteLine("Updates have been made successfully.");
        }

        private static List<XmlNode> FilterAlbumsByPriceGreaterThan20(XmlNodeList priceList)
        {
          List<XmlNode> albumsToDelete = new List<XmlNode>();

            foreach (XmlNode priceNode in priceList)
            {
                var price = double.Parse(priceNode.InnerText);

                if (price > 20.00)
                {
                    albumsToDelete.Add(priceNode.ParentNode);
                }
            }

            return albumsToDelete;
        }
    }
}
