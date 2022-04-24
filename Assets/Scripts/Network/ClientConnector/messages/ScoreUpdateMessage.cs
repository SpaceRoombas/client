using System.Text.Json.Serialization;

namespace ClientConnector.messages
{
    public class ScoreUpdateMessage
    {
        [JsonPropertyName("player_id")]
        public string PlayerId { get; set; }
        [JsonPropertyName("robot_id")]
        public string RobotId { get; set; }
        [JsonPropertyName("score")]
        public int score { get; set; }
    }
}
