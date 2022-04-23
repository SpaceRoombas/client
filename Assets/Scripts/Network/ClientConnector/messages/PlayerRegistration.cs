using System.Text.Json.Serialization;

namespace ClientConnector.messages
{
    public class PlayerRegistration
    {

        [JsonPropertyName("username")]
        public string Username { get; set; }
        [JsonPropertyName("password")]
        public string Password { get; set; }
        [JsonPropertyName("email")]
        public string Email { get; set; }

        public PlayerRegistration(string username, string password, string email)
        {
            Username = username;
            Password = password;
            Email = email;
        }

        public PlayerRegistration()
        {
        }
    }
}
