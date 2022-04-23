using System.Text.Json.Serialization;

namespace ClientConnector.messages
{
    class LoginDetails
    {
        [JsonPropertyName("username")]
        public string Username { get; set; }
        [JsonPropertyName("password")]
        public string Password { get; set; }

        public LoginDetails()
        {

        }

        public LoginDetails(string user, string pass)
        {
            this.Username = user;
            this.Password = pass;
        }
    }
}
