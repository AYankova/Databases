namespace JsonProcessing.Models
{
    using Newtonsoft.Json;

    public class Video
    {
        [JsonProperty("yt:videoId")]
        public string Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("link")]
        public Link Link { get; set; }
    }
}
