using System;
using UnityEngine;

[Serializable]
public class Deck
{
    public int Id;
    public string Name;
    public UserCard[] UserCards;
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