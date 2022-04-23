using System.Text.Json.Serialization;

namespace ClientConnector.messages
{
    public class RobotErrorEvent
    {
        [JsonPropertyName("player_id")]
        public string PlayerId { get; set; }
        [JsonPropertyName("robot_id")]
        public string RobotId { get; set; }
        [JsonPropertyName("error")]
        public string Error { get; set; }
    }
}
