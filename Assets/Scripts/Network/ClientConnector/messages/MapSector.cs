using System.Text.Json.Serialization;

namespace ClientConnector.messages
{
    public class MapSector
    {

        public class MapTile
        {
            bool walkable;
        }

        [JsonPropertyName("land_map")]
        public string LandMapEncoded { get; set; }

        [JsonPropertyName("sect_up")]
        public string SectorUP { get; set; }

        [JsonPropertyName("sect_down")]
        public string SectorDown { get; set; }

        [JsonPropertyName("sect_left")]
        public string SectorLeft { get; set; }

        [JsonPropertyName("sect_right")]
        public string SectorRight { get; set; }

        [JsonPropertyName("map_rows")]
        public int MapRows { get; set; }

        [JsonPropertyName("map_cols")]
        public int MapColumns { get; set; }

        [JsonPropertyName("sector_id")]
        public string SectorId { get; set; }

        public int[,] DecodeMap()
        {
            int[,] map = new int[this.MapRows, this.MapColumns];
            int c = 0;

            for(int i = 0; i < this.MapRows; i++)
            {
                for (int j = 0; j < this.MapColumns; j++)
                {
                    map[j, i] = this.LandMapEncoded[c] == '0' ? 0 : 1;
                    c++;
                }
            }

            return map;
        }
    }
}
