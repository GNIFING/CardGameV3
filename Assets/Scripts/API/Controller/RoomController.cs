using System;
using System.Collections;
using UnityEngine.Networking;
using Assets.Scripts.API.Controller;

public class RoomController : ApiController
{
    private readonly string controller = "/room";
    public IEnumerator MatchMaking(int playerId, Action<string> callback)
    {
        string path = "/matchmaking";

        var request = Api.CreateRequest(controller + path, "POST", new CreateMatchMakingRequest() { playerId = playerId });

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
