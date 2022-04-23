using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
using ClientConnector.messages;
using System.Text.Json;
using ClientConnector.Utils;
public class MatchmakingRequests
{

    const string LOGIN_ENDPOINT = "http://localhost:9000/login";

    public static IEnumerator PerformLoginRequest(string user, string pass, ILoginResponseHandler responseHandler)
    {
        LoginDetails loginDetails = new LoginDetails(user, pass);
        LoginToken loginToken;
        string requestJson = JsonSerializer.Serialize(loginDetails, loginDetails.GetType());

        using (UnityWebRequest req = RequestFactory.createPostRequest(LOGIN_ENDPOINT, requestJson))
        {

            yield return req.SendWebRequest();

            switch (req.result)
            {
                case UnityWebRequest.Result.Success:
                    loginToken = ExtractLoginToken(req.downloadHandler);
                    responseHandler.LoginSuccess(loginToken.Username, loginToken.Token);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    responseHandler.LoginFailure(GetErrorForHttpCode(req.responseCode));
                        break;
                case UnityWebRequest.Result.ConnectionError:
                    responseHandler.LoginFailure(GetErrorForHttpCode(999));
                    break;
                default:
                    responseHandler.LoginFailure(GetErrorForHttpCode(999));
                    break;
            }
        }
    }

    static string GetErrorForHttpCode(long code)
    {
        switch (code)
        {
            case 200:
                return "OK";
            case 400:
                return "Somethings broken";
            case 403:
                return "Username or Password Incorrect";
            default:
                return "Cannot contact server";
        }
    }

    static LoginToken ExtractLoginToken(DownloadHandler downloader)
    {
        string contents = downloader.text;

        return (LoginToken) JsonSerializer.Deserialize(contents, typeof(LoginToken));
    }
}
