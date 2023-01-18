using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public InputField outputArea;

    public void CreatePlayer() => StartCoroutine(CreatePlayerCoroutine());

    IEnumerator CreatePlayerCoroutine()
    {
        outputArea.text = "Loading...";
        string path = "player/create";

        CreatePlayerRequest body = new CreatePlayerRequest()
        {
            cards = new List<int> { 1, 3, 5 }
        };

        var request = ApiController.CreateRequest(path, "POST", body);

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
