using Assets.Scripts.API.Controller;
using SocketIOClient;
using SocketIOClient.Newtonsoft.Json;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ArenaController : ApiController
{
    public TextMeshProUGUI outputArea;

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

        socket.OnUnityThread("2", OnMessage);
    }

    void OnMessage(SocketIOResponse response)
    {
        try
        {
            ArenaData arenaData = response.GetValue<ArenaData>();
            outputArea.text = $"Me: {arenaData.Me.Id}, Opponent: {arenaData.Opponent.Id}";
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }
    }
}
