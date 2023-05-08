using System;
using UnityEngine;

public class GameData
{
    public Player player1;
    public Player player2;
    public Arena arena;
    public UserCard[] cardsOnBoard;
    public bool gameOver;
    public int? winner;
    public int? attackerIndex;
    public int? defenderIndex;
}

public class Player
{
    public int id;
    public int deckId;
    public UserCard[] cards;
    public int hp;
    public int mana;
    public bool isTurn;
}

public class Arena
{
    public int id;
    public int?[] arenaArray;
}