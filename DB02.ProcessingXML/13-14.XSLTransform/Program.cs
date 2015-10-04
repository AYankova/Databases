namespace XSLTransform
{
    using System;
    using System.Xml.Xsl;

    public class Program
    {
        private const string CataloguePath = "../../../catalogue.xml";
        private const string XsltPath = "../../catalogue.xslt";
        private const string HtmlPath = "../../catalogue.html";

        public static void Main()
        {
            XslCompiledTransform xslt = new XslCompiledTransform();
            xslt.Load(XsltPath);
            xslt.Transform(CataloguePath, HtmlPath);
            Console.WriteLine("Successfully transformed in catalogue.html");
        }
    }
}
