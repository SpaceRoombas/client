using System.Text.Json.Serialization;

namespace ClientConnector.messages
{
    public class LoginToken
    {
        [JsonPropertyName("token")]
        public string Token { get; set; }
        [JsonPropertyName("username")]
        public string Username { get; set; }
    }
}
