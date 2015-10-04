namespace XQueryTransformToHtml
{
    using System;
    using System.IO;
    using Saxon.Api;

    public class Program
    {
        private const string XmlPath = "../../../catalogue.xml";
        private const string XsltPath = "../../catalogue.xslt";
        private const string HtmlPath = "../../catalogue.html";

        public static void Main()
        {
            using (FileStream streamXml = File.OpenRead(XmlPath))
            {
                using (FileStream streamXsl = File.OpenRead(XsltPath))
                {
                    Processor processor = new Processor();

                    DocumentBuilder builder = processor.NewDocumentBuilder();
                    Uri uri = new Uri("urn:catalogue");
                    builder.BaseUri = uri;
                    XdmNode input = builder.Build(streamXml);
                    XsltTransformer transformer = processor.NewXsltCompiler().Compile(streamXsl).Load();
                    transformer.InitialContextNode = input;
                    Serializer serializer = new Serializer();
                    serializer.SetOutputFile(HtmlPath);
                    transformer.Run(serializer);
                }
            }

            Console.WriteLine("catalogue.html created successfully");
        }
    }
}
