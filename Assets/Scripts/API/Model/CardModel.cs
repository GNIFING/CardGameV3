using System;

public class CardModel
{
    public int id;
    public string index;
    public string unitName;
    public string magicName;
    public string className;
    public string unitDescription;
    public string magicDescription;
    public int userId;
    public string atkType;
    public int atk;
    public int hp;
    public int maxHp;
    public int cost;
    public bool isActive;
    public DateTime createAt;
    public DateTime updatedAt;
}

public class DeckModel
{
    public CardModel[] cards;
}
