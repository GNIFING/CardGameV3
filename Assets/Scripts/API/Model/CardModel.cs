using System;

[Serializable]
public class UserCard
{
    public int id;
    public int cardId;
    public int player = 0;
    public int atk;
    public int hp;
    public int maxHp;
    public bool isReady = true;
    public ActiveFlag activeFlag;
    public Card card;
}

public class Card
{
    public int id;
    public int? UserCardId;
    public string index;
    public string name;
    public string className;
    public string description;
    public CardAttackType atkType;
    public int atk;
    public int hp;
    public int cost;
    public string imageUri;
    public CardObtainType obtainType;
    public CardType cardType;
}

public enum ActiveFlag
{
    M,
    R,
    S,
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
    Summon,
}

public enum CardType
{
    Unit,
    Magic,
}