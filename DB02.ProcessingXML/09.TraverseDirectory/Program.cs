namespace TraverseDirectory
{
    using System;
    using System.IO;
    using System.Text;
    using System.Xml;

    public class Program
    {
        private const string DirPath = "../../dir.xml";

        public static void Main()
        {
            using (XmlTextWriter writer = new XmlTextWriter(DirPath, Encoding.UTF8))
            {
                writer.Formatting = Formatting.Indented;
                writer.IndentChar = ' ';
                writer.Indentation = 2;

                string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                writer.WriteStartDocument();
                writer.WriteStartElement("directories");

                try
                {
                    TraverseDirectory(desktopPath, writer);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine("Change the desktopPath.");
                }

                writer.WriteEndDocument();
            }

            Console.WriteLine("Xml document successfully created.");
        }

        private static void TraverseDirectory(string path, XmlTextWriter writer)
        {
            foreach (var dir in Directory.GetDirectories(path))
            {
                writer.WriteStartElement("dir");
                writer.WriteAttributeString("path", dir);

                TraverseDirectory(dir, writer);

                foreach (var file in Directory.GetFiles(dir))
                {
                    writer.WriteStartElement("file");
                    writer.WriteAttributeString("name", Path.GetFileName(file));
                    writer.WriteEndElement();
                }

                writer.WriteEndElement();
            }
        }
    }
}
