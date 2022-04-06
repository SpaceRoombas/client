using System.Text.Json.Serialization;

namespace ClientConnector.messages
{
    public class RobotListing
    {
        [JsonPropertyName("num_robots")]
        public int Count { get; set; }
        [JsonPropertyName("robots")]
        public Robot[] robots { get; set; }
    }
}
