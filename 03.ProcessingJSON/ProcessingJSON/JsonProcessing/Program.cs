namespace JsonProcessing
{
    using System;
    using JsonProcessing.Utilities;

    public class Program
    {
        private const string RssUrl = "https://www.youtube.com/feeds/videos.xml?channel_id=UCLC-vbm7OWvpbqzXaoAMGGw";
        private const string XmlLocalPath = "../../OutputFiles/telerikVideos.xml";
        private const string HtmlPath = "../../OutputFiles/videos.html";

        public static void Main()
        {
            // Download the content of the feed programatically
            Utils.DownloadRss(RssUrl, XmlLocalPath);
            Console.WriteLine("RSS has been saved to {0}", XmlLocalPath);

            // Parse the XML from the feed to JSON
            var xmlDoc = Utils.GetXml(XmlLocalPath);
            var jsonObj = Utils.GetJson(xmlDoc);

            // Using LINQ-to-JSON select all the video titles and print them on the console
            var titles = Utils.GetAllVideoTitles(jsonObj);
            /* Console.OutputEncoding = Encoding.UTF8; */
            Console.WriteLine();
            Utils.PrintVideoTitles(titles);

            // Parse the videos' JSON to POCO
            var videos = Utils.GetVideos(jsonObj);

            // Using the POCOs create a HTML page that shows all videos from the RSS
            // Use <iframe>
            // Provide a links, that nagivate to their videos in YouTube
            var html = Utils.GenerateHtml(videos);
            Utils.SaveHtml(HtmlPath, html);
            Console.WriteLine("\nSuccessfully creates video.html");
        }
    }
}
