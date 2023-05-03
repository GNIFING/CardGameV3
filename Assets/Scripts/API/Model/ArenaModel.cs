using System;
using UnityEngine;

public class ArenaData
{
    public Player Me;
    public Player Opponent;
    public CardPlacement Arena;
    public UserCard[] CardOnBoard;
}

public class Player
{
    public int Id;
    public int DeckId;
    public UserCard[] Cards;
}

public class CardPlacement
{
    public int Id;
    public int?[] ArenaArray;
}