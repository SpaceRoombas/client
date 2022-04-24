using System.Text.Json.Serialization;

namespace ClientConnector.messages
{
    public class MapSectorListingUpdate
    {
        [JsonPropertyName("map_sectors")]
        public MapSector[] MapSectors { get; set; }
    }
}
