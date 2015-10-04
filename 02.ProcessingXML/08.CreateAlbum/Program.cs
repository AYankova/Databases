namespace CreateAlbum
{
    using System;
    using System.Text;
    using System.Xml;

    public class Program
    {
        private const string CataloguePath = "../../../catalogue.xml";
        private const string AlbumsPath = "../../album.xml";

        public static void Main()
        {
            using (XmlTextWriter writer = new XmlTextWriter(AlbumsPath, Encoding.UTF8))
            {
                writer.Formatting = Formatting.Indented;
                writer.IndentChar = ' ';
                writer.Indentation = 2;

                writer.WriteStartDocument();
                writer.WriteStartElement("albums");

                using (XmlReader reader = new XmlTextReader(CataloguePath))
                {
                    while (reader.Read())
                    {
                        if (reader.NodeType == XmlNodeType.Element)
                        {
                            if (reader.Name == "name")
                            {
                                writer.WriteStartElement("album");
                                writer.WriteStartElement("name");
                                writer.WriteString(reader.ReadElementString());
                                writer.WriteEndElement();
                            }
                            else if (reader.Name == "artist")
                            {
                                writer.WriteStartElement("artist");
                                writer.WriteString(reader.ReadElementString());
                                writer.WriteEndElement();
                                writer.WriteEndElement();
                            }
                        }
                    }
                }

                    writer.WriteEndElement();
                    writer.WriteEndDocument();
            }

            Console.WriteLine("Xml document with extracted albums was created succesfully.");
        }
    }
}
