using System;
using System.Collections;
using UnityEngine.Networking;
using Assets.Scripts.API.Controller;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;

public class DeckController : ApiController
{
    private readonly string controller = "/deck";

    public IEnumerator GetDeck(int deckId, Action<Deck> callback)
    {
        string path = "/deckId/" + deckId;

        var request = Api.CreateRequest(controller + path, "GET");

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.ConnectionError && request.result != UnityWebRequest.Result.ProtocolError)
        {
            var json = request.downloadHandler.text;
            Deck deck = JsonConvert.DeserializeObject<Deck>(json);

            callback(deck);
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

    public IEnumerator AddDeck(string newDeckName, string className, Action<Deck> callback)
    {
        string path = "/create";
        int[] initialCardIds = new int[] { };

        var request = Api.CreateRequest(controller + path, "POST", new CreateDeckRequest() {
            name = newDeckName,
            className = className,
            cards = initialCardIds
        });

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.ConnectionError && request.result != UnityWebRequest.Result.ProtocolError)
        {
            var json = request.downloadHandler.text;
            Deck deck = JsonConvert.DeserializeObject<Deck>(json);

            callback(deck);
        }
        else
        {
            CheckRequestStatus(request);
        }

        request.Dispose();
    }
    public IEnumerator AddCard(int deckId, int cardId, Action<Deck> callback)
    {
        string path = "/add/cardId";

        var request = Api.CreateRequest(controller + path, "PATCH", new UpdateDeckCardRequest() { id = deckId, cardId = cardId });

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.ConnectionError && request.result != UnityWebRequest.Result.ProtocolError)
        {
            var json = request.downloadHandler.text;
            Deck deck = JsonConvert.DeserializeObject<Deck>(json);

            callback(deck);
        }
        else
        {
            CheckRequestStatus(request);
        }

        request.Dispose();
    }
    public IEnumerator RemoveCard(int deckId, int cardId, Action<Deck> callback)
    {
        string path = "/remove/cardId";

        var request = Api.CreateRequest(controller + path, "PATCH", new UpdateDeckCardRequest() { id = deckId, cardId = cardId });

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.ConnectionError && request.result != UnityWebRequest.Result.ProtocolError)
        {
            var json = request.downloadHandler.text;
            Deck deck = JsonConvert.DeserializeObject<Deck>(json);

            callback(deck);
        }
        else
        {
            CheckRequestStatus(request);
        }

        request.Dispose();
    }
}
