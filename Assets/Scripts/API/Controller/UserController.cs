using System;
using System.Collections;
using UnityEngine.Networking;
using Assets.Scripts.API.Controller;

public class UserController : ApiController
{
    public IEnumerator GetUser(Action<string> callback)
    {
        string path = "user";

        var request = Api.CreateRequest(path, "GET");

        yield return request.SendWebRequest();
        if (request.result != UnityWebRequest.Result.ConnectionError && request.result != UnityWebRequest.Result.ProtocolError)
        {
            var json = request.downloadHandler.text;
            callback(json);
        }
        else
        {
            CheckRequestStatus(request);
        }

        request.Dispose();
    }
}
