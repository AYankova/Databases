namespace ExtractAllSongs
{
    using System;
    using System.Xml;

    public class Program
    {
        private const string Path = "../../../catalogue.xml";

        public static void Main()
        {
            using (XmlReader reader = new XmlTextReader(Path))
            {
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element && reader.Name == "title")
                    {
                        Console.WriteLine(reader.ReadElementString());
                    }
                }
            }
        }
    }
}
