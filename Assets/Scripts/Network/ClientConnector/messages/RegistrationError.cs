using System.Text.Json.Serialization;

namespace ClientConnector.messages
{
    public class RegistrationError
    {
        [JsonPropertyName("error")]
        public string ErrorMessage { get; set; }
    }
}
