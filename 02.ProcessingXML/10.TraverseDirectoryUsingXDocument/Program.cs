namespace TraverseDirectoryUsingXDocument
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Xml.Linq;

    public class Program
    {
        private const string DirPath = "../../dir.xml";

        public static void Main()
        {
            string targetPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            XDocument doc = new XDocument();
            var rootElement = new XElement("directories");

            try
            {
                XElement directories = TraverseDirectory(targetPath);
                rootElement.Add(directories);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Change the targetPath.");
            }

            doc.Add(rootElement);
            doc.Save(DirPath);
            Console.WriteLine("Xml document created successfully.");
        }

        private static XElement TraverseDirectory(string path)
        {
            var element = new XElement("dir", new XAttribute("path", path));

            foreach (var dir in Directory.GetDirectories(path))
            {
                element.Add(TraverseDirectory(dir));
            }

            foreach (var file in Directory.GetFiles(path))
            {
                element.Add(new XElement("file", new XAttribute("name", Path.GetFileName(file))));
            }

            return element;
        }
    }
}
