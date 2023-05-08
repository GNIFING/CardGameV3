using System;
using System.Collections;
using Assets.Scripts.API.Controller;
using Newtonsoft.Json;
using UnityEngine.Networking;

public class MultiPlayerController : ApiController
{
    private readonly string controller = "/player";
    
    public IEnumerator DrawCard(int arenaId, int playerId, Action<DrawCardResponse> callback)
    {
        string path = "/drawCard";

        var request = Api.CreateRequest(controller + path, "POST", new ArenaPlayerRequest()
        {
            arenaId = arenaId,
            playerId = playerId,
        });

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.ConnectionError && request.result != UnityWebRequest.Result.ProtocolError)
        {
            var json = request.downloadHandler.text;
            DrawCardResponse response = JsonConvert.DeserializeObject<DrawCardResponse>(json);

            callback(response);
        }
        else
        {
            CheckRequestStatus(request);
        }

        request.Dispose();
    }

    public IEnumerator CreatePlayer(int deckId, Action<CreatePlayerResponse> callback)
    {
        string path = "/create";

        var request = Api.CreateRequest(controller + path, "POST", new CreatePlayerRequest() { deckId = deckId });

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.ConnectionError && request.result != UnityWebRequest.Result.ProtocolError)
        {
            var json = request.downloadHandler.text;
            CreatePlayerResponse newPlayer = JsonConvert.DeserializeObject<CreatePlayerResponse>(json);

            callback(newPlayer);
        }
        else
        {
            CheckRequestStatus(request);
        }

        request.Dispose();
    }

    public IEnumerator LaydownCard(int arenaId, int cardId, int index, Action<Arena> callback)
    {
        string path = "/laydown";

        var request = Api.CreateRequest(controller + path, "POST", new LaydownCardRequest()
        {
            arenaId = arenaId,
            cardId = cardId,
            index = index,
        });

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.ConnectionError && request.result != UnityWebRequest.Result.ProtocolError)
        {
            var json = request.downloadHandler.text;
            Arena cardPlacement = JsonConvert.DeserializeObject<Arena>(json);

            callback(cardPlacement);
        }
        else
        {
            CheckRequestStatus(request);
        }

        request.Dispose();
    }

    public IEnumerator Surrender(int arenaId, int playerId, Action<SurrenderResponse> callback)
    {
        string path = "/surrender";

        var request = Api.CreateRequest(controller + path, "POST", new ArenaPlayerRequest()
        {
            arenaId = arenaId,
            playerId = playerId,
        });

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.ConnectionError && request.result != UnityWebRequest.Result.ProtocolError)
        {
            var json = request.downloadHandler.text;

            SurrenderResponse response = JsonConvert.DeserializeObject<SurrenderResponse>(json);

            callback(response);
        }
        else
        {
            CheckRequestStatus(request);
        }

        request.Dispose();
    }

    public IEnumerator AttackCard(int arenaId, int attackerIndex, int defenderIndex, Action<AttackCardReponse> callback)
    {
        string path = "/attack/card";

        var request = Api.CreateRequest(controller + path, "POST", new AttackCardRequest()
        {
            arenaId = arenaId,
            attackerIndex = attackerIndex,
            defenderIndex = defenderIndex,
        });

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.ConnectionError && request.result != UnityWebRequest.Result.ProtocolError)
        {
            var json = request.downloadHandler.text;
            AttackCardReponse response = JsonConvert.DeserializeObject<AttackCardReponse>(json);

            callback(response);
        }
        else
        {
            CheckRequestStatus(request);
        }

        request.Dispose();
    }

    // Unused
    public IEnumerator UpdateCard(int arenaId, int cardIndex, int hp, int atk, Action<UserCard> callback)
    {
        string path = "/update/card";

        var request = Api.CreateRequest(controller + path, "POST", new UpdateCardRequest()
        {
            arenaId = arenaId,
            cardIndex = cardIndex,
            hp = hp,
            atk = atk,
        });

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.ConnectionError && request.result != UnityWebRequest.Result.ProtocolError)
        {
            var json = request.downloadHandler.text;
            UserCard response = JsonConvert.DeserializeObject<UserCard>(json);

            callback(response);
        }
        else
        {
            CheckRequestStatus(request);
        }

        request.Dispose();
    }

    public IEnumerator MarkUseCard(int arenaId, int cardIndex, Action<UserCard> callback)
    {
        string path = "/used/card";

        var request = Api.CreateRequest(controller + path, "POST", new MarkUseCardRequest()
        {
            arenaId = arenaId,
            cardIndex = cardIndex,
        });

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.ConnectionError && request.result != UnityWebRequest.Result.ProtocolError)
        {
            var json = request.downloadHandler.text;
            UserCard userCard = JsonConvert.DeserializeObject<UserCard>(json);

            callback(userCard);
        }
        else
        {
            CheckRequestStatus(request);
        }

        request.Dispose();
    }

    public IEnumerator AttackTower(int arenaId, int defenderId, int attackerIndex, Action<AttackTowerResponse> callback)
    {
        string path = "/attack/tower";

        var request = Api.CreateRequest(controller + path, "POST", new AttackTowerRequest()
        {
            arenaId = arenaId,
            defenderId = defenderId,
            attackerIndex = attackerIndex,
        });

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.ConnectionError && request.result != UnityWebRequest.Result.ProtocolError)
        {
            var json = request.downloadHandler.text;
            AttackTowerResponse response = JsonConvert.DeserializeObject<AttackTowerResponse>(json);

            callback(response);
        }
        else
        {
            CheckRequestStatus(request);
        }

        request.Dispose();
    }
    
    public IEnumerator MoveCard(int arenaId, int beforeIndex, int afterIndex, Action<Arena> callback)
    {
        string path = "/move";

        var request = Api.CreateRequest(controller + path, "PATCH", new MoveCardRequest()
        {
            arenaId = arenaId,
            beforeIndex = beforeIndex,
            afterIndex = afterIndex
        });

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.ConnectionError && request.result != UnityWebRequest.Result.ProtocolError)
        {
            var json = request.downloadHandler.text;
            Arena response = JsonConvert.DeserializeObject<Arena>(json);

            callback(response);
        }
        else
        {
            CheckRequestStatus(request);
        }

        request.Dispose();
    }
    
    public IEnumerator EndTurn(int arenaId, int playerId, Action<EndTurnResponse> callback)
    {
        string path = "/turn";

        var request = Api.CreateRequest(controller + path, "POST", new ArenaPlayerRequest()
        {
            arenaId = arenaId,
            playerId = playerId,
        });

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.ConnectionError && request.result != UnityWebRequest.Result.ProtocolError)
        {
            var json = request.downloadHandler.text;
            EndTurnResponse response = JsonConvert.DeserializeObject<EndTurnResponse>(json);

            callback(response);
        }
        else
        {
            CheckRequestStatus(request);
        }

        request.Dispose();
    }
}
