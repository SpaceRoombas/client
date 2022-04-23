using System.Text.Json.Serialization;

namespace ClientConnector.messages
{
    public class GameListing
    {
        [JsonPropertyName("match_details")]
        public MatchConnectionDetails[] ConnectionDetails { get; set; }
    }
}
