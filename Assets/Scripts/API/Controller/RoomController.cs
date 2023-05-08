using System;
using System.Collections;
using UnityEngine.Networking;
using Assets.Scripts.API.Controller;
using Newtonsoft.Json;

public class RoomController : ApiController
{
    private readonly string controller = "/room";

    public IEnumerator GetRoom(string roomId, Action<CreateMatchMakingResponse> callback)
    {
        string path = "/roomId/" + roomId;

        var request = Api.CreateRequest(controller + path, "GET");

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.ConnectionError && request.result != UnityWebRequest.Result.ProtocolError)
        {
            var json = request.downloadHandler.text;
            CreateMatchMakingResponse response = JsonConvert.DeserializeObject<CreateMatchMakingResponse>(json);
            
            callback(response);
        }
        else
        {
            CheckRequestStatus(request);
        }

        request.Dispose();
    }

    public IEnumerator MatchMaking(int playerId, Action<CreateMatchMakingResponse> callback)
    {
        string path = "/matchmaking";

        var request = Api.CreateRequest(controller + path, "POST", new CreateMatchMakingRequest() { playerId = playerId });

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.ConnectionError && request.result != UnityWebRequest.Result.ProtocolError)
        {
            var json = request.downloadHandler.text;
            CreateMatchMakingResponse response = JsonConvert.DeserializeObject<CreateMatchMakingResponse>(json);
            callback(response);
        }
        else
        {
            CheckRequestStatus(request);
        }

        request.Dispose();
    }
    public IEnumerator CancelMatchMaking(string roomId, int playerId, Action<CancelMatchMakingResponse> callback)
    {
        string path = "/matchmaking/cancel";

        var request = Api.CreateRequest(controller + path, "POST", new CancelMatchMakingRequest() {
            roomId = roomId,
            playerId = playerId,
        });

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.ConnectionError && request.result != UnityWebRequest.Result.ProtocolError)
        {
            var json = request.downloadHandler.text;
            CancelMatchMakingResponse response = JsonConvert.DeserializeObject<CancelMatchMakingResponse>(json);

            callback(response);
        }
        else
        {
            CheckRequestStatus(request);
        }

        request.Dispose();
    }
}
