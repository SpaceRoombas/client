using System;
using UnityEngine.Networking;


namespace Network.Utils
{
    public class RequestFactory
    {
        public static UnityWebRequest createPostRequest(string endpoint, string json)
        {
            UploadHandler uploader = createJsonUploadHandler(json);
            DownloadHandler downloader = new DownloadHandlerBuffer();

            return new UnityWebRequest(endpoint, "POST", downloader, uploader);
        }

        public static UnityWebRequest createAuthedGetRequest(string endpoint, string token, string json = "{}")
        {
            UploadHandler uploader;
            DownloadHandler downloader;
            UnityWebRequest request;

            if (token == null || token == string.Empty)
            {
                throw new ArgumentException("Auth token missing, cannot create request");
            }

            uploader = createJsonUploadHandler(json);
            downloader = new DownloadHandlerBuffer();
            request = new UnityWebRequest(endpoint, "GET", downloader, uploader);

            request.SetRequestHeader("Authorization", $"Bearer {token}");

            return request;
        }

        /// <summary>
        /// Creates an upload handler that sends raw json
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static UploadHandler createJsonUploadHandler(string json)
        {
            byte[] bodyBytes = System.Text.Encoding.UTF8.GetBytes(json);
            UploadHandler uploader = new UploadHandlerRaw(bodyBytes);
            uploader.contentType = "application/json";

            return uploader;
        }
    }
}
