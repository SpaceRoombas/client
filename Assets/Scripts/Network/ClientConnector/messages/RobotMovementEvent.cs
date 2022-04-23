using System.Text.Json.Serialization;

namespace ClientConnector.messages
{
    public class RobotMovementEvent
    {
        [JsonPropertyName("player_id")]
        public string PlayerId {get; set;}
        [JsonPropertyName("robot_id")]
        public string RobotId { get; set; }
        [JsonPropertyName("x")]
        public int X { get; set; }
        [JsonPropertyName("y")]
        public int Y { get; set; }

        [JsonPropertyName("old_location")]
        public EntityLocation OldLocation { get; set; }
        [JsonPropertyName("new_location")]
        public EntityLocation NewLocation { get; set; }

    }
}
