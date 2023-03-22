using System;
using UnityEngine;

[Serializable]
public class Card
{
    public int id;
    public string index;
    public string name;
    public string className;
    public string description;
    public string magicDescription;
    public CardAttackType atkType;
    public int atk;
    public int hp;
    public int cost;
    public string imageUri;
    public CardObtainType obtainType;
    public CardType cardType;
    public bool isActive;
    public DateTime createAt;
    public DateTime updatedAt;
}

public enum CardClass
{
    Human,
    Elf,
    Ogre,
}

public enum CardAttackType
{
    Melee,
    Range,
}

public enum CardObtainType
{
    Market,
    Initial,
}

public enum CardType
{
    Unit,
    Magic,
}

public class UserCard
{
    public int id;
    public int userId;
    public int cardId;
    public int atk;
    public int hp;
    public int maxHp;
    public bool isActive;
    public DateTime createdAt;
    public DateTime updatedAt;
    public Card card;
}