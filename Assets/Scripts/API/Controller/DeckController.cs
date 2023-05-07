using System;
using System.Collections;
using UnityEngine.Networking;
using Assets.Scripts.API.Controller;
using Newtonsoft.Json;
using System.Collections.Generic;

public class DeckController : ApiController
{
    private readonly string controller = "/deck";

    public IEnumerator GetDeck(int deckId, Action<string> callback)
    {
        string path = "/deckId/cards" + deckId;

        var request = Api.CreateRequest(controller + path, "GET");

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
    public IEnumerator GetDecks(Action<List<Deck>> callback)
    {
        string path = "/";

        var request = Api.CreateRequest(controller + path, "GET");

        yield return request.SendWebRequest();
        if (request.result != UnityWebRequest.Result.ConnectionError && request.result != UnityWebRequest.Result.ProtocolError)
        {
            var json = request.downloadHandler.text;
            List<Deck> decks = new(JsonConvert.DeserializeObject<Deck[]>(json));

            callback(decks);
        }
        else
        {
            CheckRequestStatus(request);
        }

        request.Dispose();
    }

    public IEnumerator AddDeck(string newDeckName, Action<string> callback)
    {
        string path = "/create";
        int[] initialCardIds = new int[] { 1, 2, 3 };

        var request = Api.CreateRequest(controller + path, "POST", new CreateDeckRequest() { name = newDeckName, cards = initialCardIds });

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
