using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class ApiController : MonoBehaviour
{
    public static string accessToken;
    public static string apiPrefix = "http://localhost:3000/";

    public static UnityWebRequest CreateAuthRequest(string path, string method, object body = null)
    {
        var request = new UnityWebRequest(apiPrefix + path, method.ToString());

        if (body != null)
        {
            var requestBody = Encoding.UTF8.GetBytes(JsonUtility.ToJson(body));
            request.uploadHandler = new UploadHandlerRaw(requestBody);
        }

        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        request.disposeUploadHandlerOnDispose = true;
        request.disposeDownloadHandlerOnDispose = true;

        return request;
    }


    public static UnityWebRequest CreateRequest(string path, string method, object body = null)
    {
        var request = new UnityWebRequest(apiPrefix + path, method.ToString());

        if (body != null)
        {
            var requestBody = Encoding.UTF8.GetBytes(JsonUtility.ToJson(body));
            request.uploadHandler = new UploadHandlerRaw(requestBody);
        }

        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("Authorization", "Bearer " + accessToken);

        request.disposeUploadHandlerOnDispose = true;
        request.disposeDownloadHandlerOnDispose = true;

        return request;
    }
}
