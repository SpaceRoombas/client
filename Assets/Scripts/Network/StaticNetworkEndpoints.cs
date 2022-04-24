using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Network.Statics
{
    public class StaticNetworkEndpoints
    {
        public const string LOGIN_ENDPOINT = "http://localhost:9000/login";
        public const string JOIN_ENDPOINT = "http://localhost:9000/joinmatch";
        public const string LISTING_ENDPOINT = "http://localhost:9000/fetchmatches";
        public const string REGISTER_ENDPOINT = "http://localhost:9000/register";
    }
}
