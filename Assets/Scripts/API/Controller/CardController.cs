using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using Newtonsoft.Json;
using TMPro;
using System;
using System.Linq;
using Assets.Scripts.API.Controller;

public class CardController : ApiController
{
    public IEnumerator GetCards(Action<List<UserCard>> callback)
    {
        string path = "/card";

        var request = Api.CreateRequest(path, "GET");

        yield return request.SendWebRequest();
        if (request.result != UnityWebRequest.Result.ConnectionError && request.result != UnityWebRequest.Result.ProtocolError)
        {
            var json = request.downloadHandler.text;
            List<UserCard> userCards = new(JsonConvert.DeserializeObject<UserCard[]>(json));

            callback(userCards);
        }
        else
        {
            CheckRequestStatus(request);
        }

        request.Dispose();
    }

    public IEnumerator GetSummonCards(Action<List<UserCard>> callback)
    {
        string path = "/card/summon";

        var request = Api.CreateRequest(path, "GET");

        yield return request.SendWebRequest();
        if (request.result != UnityWebRequest.Result.ConnectionError && request.result != UnityWebRequest.Result.ProtocolError)
        {
            var json = request.downloadHandler.text;
            List<UserCard> userCards = new(JsonConvert.DeserializeObject<UserCard[]>(json));

            callback(userCards);
        }
        else
        {
            CheckRequestStatus(request);
        }

        request.Dispose();
    }

    public IEnumerator AddCard(int deckId, int cardId, Action<string> callback)
    {
        string path = "deck/add/cardId/";

        var request = Api.CreateRequest(path, "PATCH", new UpdateDeckCardRequest() { id = deckId, cardId = cardId });

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.ConnectionError && request.result != UnityWebRequest.Result.ProtocolError)
        {
            callback("card added");
        }
        else
        {
            CheckRequestStatus(request);
        }

        request.Dispose();
    }
    public IEnumerator RemoveCard(int deckId, int cardId, Action<string> callback)
    {
        string path = "deck/remove/cardId/";

        var request = Api.CreateRequest(path, "PATCH", new UpdateDeckCardRequest() { id = deckId, cardId = cardId });

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
