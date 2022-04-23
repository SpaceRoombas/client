using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;


namespace ClientConnector.Utils
{
    public class RequestFactory
    {
        public static UnityWebRequest createPostRequest(string endpoint, string json)
        {
            UploadHandler uploader = createJsonUploadHandler(json);
            DownloadHandler downloader = new DownloadHandlerBuffer();

            return new UnityWebRequest(endpoint, "POST", downloader, uploader);
        }

        public static UploadHandler createJsonUploadHandler(string json)
        {
            byte[] bodyBytes = System.Text.Encoding.UTF8.GetBytes(json);
            UploadHandler uploader = new UploadHandlerRaw(bodyBytes);
            uploader.contentType = "application/json";

            return uploader;
        }
    }
}
