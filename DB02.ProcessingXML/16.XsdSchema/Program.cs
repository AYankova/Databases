namespace XsdSchema
{
    using System;
    using System.Xml;
    using System.Xml.Linq;
    using System.Xml.Schema;

    public class Program
    {
        private const string ValidXmlPath = "../../catalogue.xml";
        private const string InvalidXmlPath = "../../invalidCatalogue.xml";
        private const string XsdPath = "../../catalogue.xsd";

        public static void Main()
        {
            var schema = new XmlSchemaSet();
            schema.Add(string.Empty, XsdPath);

            XDocument validDoc = XDocument.Load(ValidXmlPath);
            XDocument invalidDoc = XDocument.Load(InvalidXmlPath);
            
            Console.WriteLine("Valid XML according to the xsd schema: ");
            validDoc.Validate(schema, (sender, args) => Console.WriteLine(args.Message));
            Console.WriteLine("\nInvalid XML according to the xsd schema: ");
            invalidDoc.Validate(schema, (sender, args) => Console.WriteLine(args.Message));
        }
    }
}
