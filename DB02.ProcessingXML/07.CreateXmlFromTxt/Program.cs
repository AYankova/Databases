namespace CreateXmlFromTxt
{
    using System;
    using System.IO;
    using System.Xml.Linq;

    public class Program
    {
        private const string TxtPath = "../../person.txt";
        private const string XmlPath = "../../person.xml";

        public static void Main()
        {
            var person = new Person();

            using (StreamReader reader = new StreamReader(TxtPath))
            {
                person.Name = reader.ReadLine();
                person.Phone = reader.ReadLine();
                person.Address = reader.ReadLine();
            }

            var personElement = new XElement(
                                            "person",
                                             new XElement("name", person.Name),
                                             new XElement("phone", person.Phone),
                                             new XElement("address", person.Address));

            personElement.Save(XmlPath);
            Console.WriteLine("Xml document was created successfully.");
        }
    }
}
