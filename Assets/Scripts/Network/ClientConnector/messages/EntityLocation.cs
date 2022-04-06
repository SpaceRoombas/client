
using System.Text.Json.Serialization;

namespace ClientConnector.messages
{
    public class EntityLocation
    {
        [JsonPropertyName("sector_id")]
        public string SectorId { get; set; }
        [JsonPropertyName("x")]
        public int X { get; set; }
        [JsonPropertyName("y")]
        public int Y { get; set; }
    }
}
