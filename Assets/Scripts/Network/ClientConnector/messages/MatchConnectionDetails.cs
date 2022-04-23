using System.Text.Json.Serialization;

namespace ClientConnector.messages
{
    public class MatchConnectionDetails
    {
        [JsonPropertyName("host")]
        public string Host { get; set; }
        [JsonPropertyName("port")]
        public int port { get; set; }
        [JsonPropertyName("players")]
        public string[] Players { get; set; }
    }
}
