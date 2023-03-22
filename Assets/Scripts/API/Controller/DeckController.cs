using System;
using System.Collections;
using UnityEngine.Networking;
using Assets.Scripts.API.Controller;

public class DeckController : ApiController
{
    public IEnumerator GetDeck(int deckId, Action<string> callback)
    {
        string path = "deck/deckId/cards/" + deckId;

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
    public IEnumerator GetDecks(Action<string> callback)
    {
        string path = "deck";

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

    public IEnumerator AddDeck(string newDeckName, Action<string> callback)
    {
        string path = "deck/create/";
        int[] initialCardIds = new int[] { 1, 2, 3 };

        var request = Api.CreateRequest(path, "POST", new CreateDeckRequest() { name = newDeckName, cards = initialCardIds });

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
