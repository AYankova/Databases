namespace JsonProcessing.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Xml;
    using JsonProcessing.Models;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    public static class Utils
    {
        public static void DownloadRss(string url, string pathToFile)
        {
            var webClient = new WebClient();
            webClient.DownloadFile(url, pathToFile);
        }

        public static XmlDocument GetXml(string pathToFile)
        {
            var doc = new XmlDocument();
            doc.Load(pathToFile);

            return doc;
        }

        public static JObject GetJson(XmlDocument doc)
        {
            string json = JsonConvert.SerializeXmlNode(doc);
            JObject jsonObj = JObject.Parse(json);

            return jsonObj;
        }

        public static IEnumerable<JToken> GetAllVideoTitles(JObject jsonObj)
        {
            var videoTitles = jsonObj["feed"]["entry"].Select(x => x["title"]);

            return videoTitles;
        }

        public static void PrintVideoTitles(IEnumerable<JToken> videoTitles)
        {
            foreach (var title in videoTitles)
            {
                Console.WriteLine(title);
            }
        }

        public static IEnumerable<Video> GetVideos(JObject jsonObj)
        {
            var videos = jsonObj["feed"]["entry"].Select(x => JsonConvert.DeserializeObject<Video>(x.ToString()));

            return videos;
        }

        public static string GenerateHtml(IEnumerable<Video> videos)
        {
            StringBuilder html = new StringBuilder();
            html.Append("<!DOCTYPE html><html><head><style>" +
                 "html { width:960px; margin:0 auto;}" +
                 "body { background-color:#242424;}" +
                 "div { width: 420px; height: 440px; padding:10px; margin:5px; background-color:#CCC; border-radius:15px}" +
                 "</style></head><body>");

            foreach (var video in videos)
            {
                html.AppendFormat(
                      "<div style=\"float:left\">" +
                      "<iframe width=\"400\" height=\"340\" " +
                      "src=\"http://www.youtube.com/embed/{1}?autoplay=0\" " +
                      "frameborder=\"0\" allowfullscreen></iframe>" +
                      "<h3>{2}</h3><a href=\"{0}\">Go to YouTube</a></div>",
                      video.Link.Href,
                      video.Id,
                      video.Title);
            }

            html.Append("</body></html>");

            return html.ToString();
        }

        public static void SaveHtml(string path, string html)
        {
            using (StreamWriter writer = new StreamWriter(path, false, Encoding.UTF8))
            {
                writer.Write(html);
            }
        }
    }
}
