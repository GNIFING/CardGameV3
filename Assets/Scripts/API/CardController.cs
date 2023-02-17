using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using Newtonsoft.Json;
using TMPro;

public class CardController : MonoBehaviour
{
    public List<Card> cards = new List<Card>();

    // public List<Card> GetCard() => StartCoroutine(GetCardCoroutine());

    private void Start()
    {
        // StartCoroutine(GetCardCoroutine());
        // StartCoroutine(GetCards());
    }

    // private IEnumerator GetCardCoroutine()
    // {
    //     string path = "card";

    //     var request = Api.CreateRequest(path, "GET");

    //     yield return request.SendWebRequest();
    //     if (request.result != UnityWebRequest.Result.ConnectionError && request.result != UnityWebRequest.Result.ProtocolError)
    //     {
    //         var json = request.downloadHandler.text;
    //         cards = new List<Card>(JsonConvert.DeserializeObject<Card[]>(json));
    //         cards.AddRange(JsonConvert.DeserializeObject<Card[]>(json));
    //         cards.AddRange(JsonConvert.DeserializeObject<Card[]>(json));
            
    //         return cards;
    //     }
    //     else 
    //     {
    //         Debug.Log(request.result);
    //     }
    // }

    public List<Card> GetCards()
    {
        string path = "card";

        var request = Api.CreateRequest(path, "GET");

        request.SendWebRequest();
        if (request.result != UnityWebRequest.Result.ConnectionError && request.result != UnityWebRequest.Result.ProtocolError)
        {
            var json = request.downloadHandler.text;
            cards = new List<Card>(JsonConvert.DeserializeObject<Card[]>(json));
            cards.AddRange(JsonConvert.DeserializeObject<Card[]>(json));
            cards.AddRange(JsonConvert.DeserializeObject<Card[]>(json));
        }
        else 
        {
            Debug.Log(request.result);
        }

        return cards;
    }
}
