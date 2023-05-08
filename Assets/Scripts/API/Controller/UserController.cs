using System;
using System.Collections;
using UnityEngine.Networking;
using Assets.Scripts.API.Controller;
using UnityEngine;
using Newtonsoft.Json;
using UnityEngine.SceneManagement;

public class UserController : ApiController
{
    private readonly string controller = "/user";
    public IEnumerator GetUser(Action<User> callback)
    {
        string path = "/";

        var request = Api.CreateRequest(controller + path, "GET");

        yield return request.SendWebRequest();
        if (request.result != UnityWebRequest.Result.ConnectionError && request.result != UnityWebRequest.Result.ProtocolError)
        {
            var json = request.downloadHandler.text;
            User user = JsonConvert.DeserializeObject<User>(json);

            callback(user);
        }
        else
        {
            CheckRequestStatus(request);
        }

        request.Dispose();
    }

    public void Logout()
    {
        Api.accessToken = null;

        SceneManager.LoadScene("LoginPage");
    }
}
