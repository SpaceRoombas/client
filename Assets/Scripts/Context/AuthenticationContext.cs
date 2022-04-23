

/// <summary>
/// 
/// Hacky data holding object to persist data between scenes
/// </summary>
/// 

namespace StaticContext
{
    public class AuthenticationContext
    {

        // Because we might want a little bit of control to avoid over-writing Username and Token
        // these will be encapsulated and only settable when the strings are empty
        private static string _username = "";
        private static string _token = "";
        public static string Username
        {
            get { return _username; }
            set
            {
                if (!CanSetProperty(_username, value))
                {
                    return;
                }
                _username = value;
            }
        }
        public static string Token
        {
            get { return _token; }
            set
            {
                if (!CanSetProperty(_token, value))
                {
                    return;
                }
                _token = value;
            }
        }

        private static bool CanSetProperty(string property, string value)
        {
            // check that the property is empty and that the value is not empty
            return (property == string.Empty) && (value != string.Empty);
        }
    }
}
