using System;
using UnityEngine;

[Serializable]
public class Card
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