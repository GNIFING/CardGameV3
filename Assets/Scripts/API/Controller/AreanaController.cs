using Assets.Scripts.API.Controller;
using Newtonsoft.Json;
using SocketIOClient;
using SocketIOClient.Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class AreanaController : ApiController
{
    public TextMeshProUGUI outputArea;

    private List<Deck> decks;

    void Start()
    {
        var uri = new Uri("http://localhost:8000");
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
            Debug.Log("Ping");
        };
        socket.OnPong += (sender, e) =>
        {
            Debug.Log("Pong: " + e.TotalMilliseconds);
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

        //socket.OnUnityThread("deck", OnDeck);
        socket.OnUnityThread("deck", OnMessage);
    }

    void OnDeck(SocketIOResponse response)
    {
        try
        {
            string responseData = response.GetValue<string>();
            decks = new(JsonConvert.DeserializeObject<Deck[]>(responseData));

            outputArea.text = decks.Aggregate("", (current, next) => current + " " + next.name);
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }
    }

    void OnMessage(SocketIOResponse response)
    {
        try
        {
            string responseData = response.GetValue<string>();
            outputArea.text = " " + responseData;
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }
    }

    public void Log()
    {
        outputArea.text = decks.Aggregate("", (current, next) => current + " " + next.name);
        decks.ForEach(deck =>
        {
            Debug.Log(deck.name);
        });
    }
}
