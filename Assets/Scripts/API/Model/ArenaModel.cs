using System;
using UnityEngine;

public class GameData
{
    public Player playerOne;
    public Player playerTwo;
    public Arena arena;
    public UserCard[] cardOnBoard;
    public bool gameOver;
    public int? winner;
    public int? attackerIndex;
    public int? defenderIndex;
}

public class Player
{
    public int id;
    public int cardLeft;
    public int deckId;
    public UserCard?[] cards;
    public int hp;
    public int maxMana;
    public int mana;
    public bool isTurn;
}

public class Arena
{
    public int id;
    public int?[] arenaArray;
    public bool buffOneActive;
    public bool buffTwoActive;
}

public class ArenaApiQueue
{
    public int? afterIndex;
    public int? arenaId;
    public int? atk;
    public int? attackerIndex;
    public int? beforeIndex;
    public bool? buffOneActive;
    public bool? buffTwoActive;
    public int? cardId;
    public int? cardIndex;
    public int? defenderIndex;
    public int? defenderId;
    public int? hp;
    public int? index;
    public string path;
    public int? playerId;
}