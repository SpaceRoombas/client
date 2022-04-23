using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaticContext
{
    public class GameConnectionContext
    {
        public static string Host = "localhost";
        public static int Port = 9001;
        public static string Username = "FirstBot";
        public static string Token;

        public static void SetContext(string username, string token, string host, int port)
        {
            Username = username;
            Token = token;
            Host = host;
            Port = port;
        }
    }
}
