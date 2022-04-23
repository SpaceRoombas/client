using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Network.Interfaces
{
    public interface ILoginResponseHandler
    {

        void LoginSuccess(string username, string token);
        void LoginFailure(string reason);
    }
}
