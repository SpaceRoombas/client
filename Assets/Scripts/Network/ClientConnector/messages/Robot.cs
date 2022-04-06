using System.Text.Json.Serialization;

namespace ClientConnector.messages
{
    public class Robot
    {
        [JsonPropertyName("owner")]
        public string Owner { get; set; }
        [JsonPropertyName("robot_id")]
        public string RobotId { get; set; }
        [JsonPropertyName("location")]
        public EntityLocation Location { get; set; }
        [JsonPropertyName("firmware")]
        public string Firmware { get; set; }
    }
}
