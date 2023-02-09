using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class User : MonoBehaviour
{
    public InputField outputArea;

    public void CreateUser() => StartCoroutine(CreateUserCoroutine());

    IEnumerator CreateUserCoroutine()
    {
        outputArea.text = "Loading...";
        string path = "player/create";

        CreateUserRequest body = new CreateUserRequest()
        {
            cards = new List<int> { 1, 3, 5 }
        };

        var request = Api.CreateRequest(path, "POST", body);

        yield return request.SendWebRequest();
        if (request.result != UnityWebRequest.Result.ConnectionError && request.result != UnityWebRequest.Result.ProtocolError)
        {
            outputArea.text = request.downloadHandler.text;
        }
        else
        {
            outputArea.text = request.error;
        }
    }
}
