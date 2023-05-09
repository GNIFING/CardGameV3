using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class Api : MonoBehaviour
{

    public static string accessToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VySWQiOjMsImlhdCI6MTY4MzYzMjQ4OSwiZXhwIjoxNjgzNjM2MDg5fQ.oXXEBXOnbuKv16zazFOwTBPqtHVeWy5B6wnyaV5VLHc";
    public static string apiPrefix = "http://18.140.116.224:8080/api";
    //public static string s3Prefix = "https://thanat-sun-storage.s3.ap-southeast-1.amazonaws.com/capstone/images/";

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
