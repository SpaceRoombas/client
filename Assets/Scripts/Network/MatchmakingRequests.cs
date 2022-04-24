using System.Collections;
using UnityEngine.Networking;
using ClientConnector.messages;
using System.Text.Json;
using Network.Utils;
using Network.Interfaces;
using Network.Statics;
public class MatchmakingRequests
{


    public static IEnumerator PerformLoginRequest(string user, string pass, ILoginResponseHandler responseHandler)
    {
        LoginDetails loginDetails = new LoginDetails(user, pass);
        LoginToken loginToken;
        string requestJson = JsonSerializer.Serialize(loginDetails, loginDetails.GetType());

        using (UnityWebRequest req = RequestFactory.createPostRequest(StaticNetworkEndpoints.LOGIN_ENDPOINT, requestJson))
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

    public static IEnumerator PerformRegistrationRequest(string user, string pass, string email, IRegisterHandler responseHandler)
    {
        PlayerRegistration regDetails = new PlayerRegistration(user, pass, email);
        string requestJson = JsonSerializer.Serialize(regDetails, regDetails.GetType());

        using (UnityWebRequest req = RequestFactory.createPostRequest(StaticNetworkEndpoints.REGISTER_ENDPOINT, requestJson))
        {

            RegistrationError error;

            yield return req.SendWebRequest();

            switch (req.result)
            {
                case UnityWebRequest.Result.Success:
                    responseHandler.HandleRegister();
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    error = ExtractRegistrationError(req.downloadHandler);
                    responseHandler.HandleRegisterFailure(error.ErrorMessage);
                    break;
                case UnityWebRequest.Result.ConnectionError:
                    responseHandler.HandleRegisterFailure(GetErrorForHttpCode(999));
                    break;
                default:
                    responseHandler.HandleRegisterFailure(GetErrorForHttpCode(999));
                    break;
            }
        }
    }

    public static IEnumerator PerformGameJoinRequest(string user, string token, IGameJoinHandler responseHandler)
    {

        using (UnityWebRequest req = RequestFactory.createAuthedGetRequest(StaticNetworkEndpoints.JOIN_ENDPOINT, token))
        {
            MatchConnectionDetails connectionDetails;

            yield return req.SendWebRequest();

            switch (req.result)
            {
                case UnityWebRequest.Result.Success:
                    connectionDetails = ExtractMatchConnectionDetails(req.downloadHandler);
                    responseHandler.HandleGameJoin(user, token, connectionDetails.Host, connectionDetails.port);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    responseHandler.HandleJoinFailure(GetErrorForHttpCode(req.responseCode));
                    break;
                case UnityWebRequest.Result.ConnectionError:
                    responseHandler.HandleJoinFailure(GetErrorForHttpCode(999));
                    break;
                default:
                    responseHandler.HandleJoinFailure(GetErrorForHttpCode(999));
                    break;
            }
        }
    }

    public static IEnumerator PerformGameListingRequest(string user, string token, IGameListingHandler responseHandler)
    {

        using (UnityWebRequest req = RequestFactory.createAuthedGetRequest(StaticNetworkEndpoints.LISTING_ENDPOINT, token))
        {
            GameListing listing;

            yield return req.SendWebRequest();

            switch (req.result)
            {
                case UnityWebRequest.Result.Success:
                    listing = ExtractMatchListing(req.downloadHandler);
                    responseHandler.HandleGameList(listing);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    responseHandler.HandleGameListingFailure(GetErrorForHttpCode(req.responseCode));
                    break;
                case UnityWebRequest.Result.ConnectionError:
                    responseHandler.HandleGameListingFailure(GetErrorForHttpCode(999));
                    break;
                default:
                    responseHandler.HandleGameListingFailure(GetErrorForHttpCode(999));
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

    static MatchConnectionDetails ExtractMatchConnectionDetails(DownloadHandler downloader)
    {
        string contents = downloader.text;

        return (MatchConnectionDetails) JsonSerializer.Deserialize(contents, typeof(MatchConnectionDetails));
    }

    static GameListing ExtractMatchListing(DownloadHandler downloader)
    {
        string contents = downloader.text;

        return (GameListing)JsonSerializer.Deserialize(contents, typeof(GameListing));
    }

    static RegistrationError ExtractRegistrationError(DownloadHandler downloader)
    {
        string contents = downloader.text;

        return (RegistrationError)JsonSerializer.Deserialize(contents, typeof(RegistrationError));
    }
}
