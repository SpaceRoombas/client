using System.Text.Json.Serialization;

namespace ClientConnector.messages
{
    public class RobotMineEvent
    {
        [JsonPropertyName("player_id")]
        public string PlayerId { get; set; }
        [JsonPropertyName("robot_id")]
        public string RobotId { get; set; }
        [JsonPropertyName("sector_id")]
        public string SectorId { get; set; }
        [JsonPropertyName("mined_x")]
        public int X { get; set; }
        [JsonPropertyName("mined_y")]
        public int Y { get; set; }


    }
}
