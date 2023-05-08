using Assets.Scripts.API.Controller;
using Newtonsoft.Json;
using SocketIOClient;
using SocketIOClient.Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Playables;

public class ArenaController : ApiController
{
    public TextMeshProUGUI outputArea;
    public DataHandler dataHandler;

    private readonly string controller = "/arena";

    void Start()
    {
        var uri = new Uri("http://18.140.116.224:8080/arena/socket");
        SocketIOUnity socket = new(uri, new SocketIOOptions
        {
            Query = new Dictionary<string, string>
        {
            {"token", "UNITY" }
        },
            Transport = SocketIOClient.Transport.TransportProtocol.WebSocket
        })
        {
            JsonSerializer = new NewtonsoftJsonSerializer()
        };
        ///// reserved socketio events
        socket.OnConnected += (sender, e) =>
        {
            Debug.Log("Socket Connected");
        };
        socket.OnPing += (sender, e) =>
        {
            //Debug.Log("Ping");
        };
        socket.OnPong += (sender, e) =>
        {
            //Debug.Log("Pong: " + e.TotalMilliseconds);
        };
        socket.OnDisconnected += (sender, e) =>
        {
            Debug.Log("Disconnect: " + e);
        };
        socket.OnReconnectAttempt += (sender, e) =>
        {
            Debug.Log($"{DateTime.Now} Reconnecting: attempt = {e}");
        };

        Debug.Log("Connecting...");
        socket.Connect();


        int arenaId = PlayerPrefs.GetInt("ArenaId");
        socket.OnUnityThread(arenaId.ToString(), OnMessage);

        StartCoroutine(this.GetArena(arenaId));
    }

    void OnMessage(SocketIOResponse response)
    {
        Debug.Log("response = " + response);
        try
        {
            GameData gameData = response.GetValue<GameData>();
            outputArea.text = $"Player 1 : {gameData.playerOne.id}, Player 2 : {gameData.playerTwo.id}";
            dataHandler.UpdateData(gameData);
            //outputArea.text = $"Me: {arenaData.Me.Cards}";
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }
    }


    public IEnumerator GetArena(int arenaId)
    {
        yield return new WaitForSeconds(2);
        string path = "/arenaId/" + arenaId;

        var request = Api.CreateRequest(controller + path, "GET");

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.ConnectionError && request.result != UnityWebRequest.Result.ProtocolError)
        {
            var json = request.downloadHandler.text;
            //DrawCardResponse response = JsonConvert.DeserializeObject<DrawCardResponse>(json);

            //callback(response);
        }
        else
        {
            CheckRequestStatus(request);
        }

        request.Dispose();
    }


}