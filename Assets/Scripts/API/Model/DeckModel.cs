using System;
using UnityEngine;

[Serializable]
public class Deck
{
    public int id;
    public string name;
    public int userId;
    public int[] cards;
    public int[] cardsOrigin;
    public bool isActive;
    public DateTime createAt;
    public DateTime updatedAt;
}

public class DeckItem
{
    public int id;
    public string name;
}

public class CreateDeckRequest
{
    public string name;
    public int[] cards;
}
public class CreateDeckResponse
{
    public int id;
    public string name;
    public Card[] cards;
}
public class UpdateDeckCardRequest
{
    public int id;
    public int cardId;
}