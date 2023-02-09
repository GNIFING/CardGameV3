using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEditor.PackageManager.Requests;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.Sqlite;
using System.Text;

public class Card : MonoBehaviour
{
    public InputField outputArea;

    public void GetCard() => StartCoroutine(GetCardCoroutine());

    IEnumerator GetCardCoroutine()
    {
        outputArea.text = "Loading...";
        string path = "card";

        var request = Api.CreateRequest(path, "GET");

        yield return request.SendWebRequest();
        if (request.result != UnityWebRequest.Result.ConnectionError && request.result != UnityWebRequest.Result.ProtocolError)
        {
            var json = request.downloadHandler.text;
            outputArea.text = json;
            CardModel[] cards = JsonHelper.FromJson<CardModel>(json);
            Debug.Log(cards);
            //foreach (CardModel card in cards)
            //{
            //    Debug.Log(card);
            //}
            //outputArea.text = cards[0].unitName + "\n" + cards[1].unitName;
        }
        else
        {
            outputArea.text = request.error;
        }
    }
}
