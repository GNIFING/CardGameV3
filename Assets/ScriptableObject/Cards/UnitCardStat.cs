using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "UnitCardStat")]
public class UnitCardStat : ScriptableObject
{
    public enum MoveType
    {
        StraightShort,
        StraightFar,
        DiagonalShort,
        DiagonalFar,
        Round,
        HorizontalShort,
        HorizontalFar
    }

    public enum CardClass
    {
        Human,
        Ogre,
        Elf
    }

    // Name and Description 
    [SerializeField] private int cardIndex;
    public int CardIndex => cardIndex;

    [SerializeField] private string cardName;
    public string CardName => cardName;

    [SerializeField] private string cardDescription;
    public string CardDescription => cardDescription;

    // Card Type 
    [SerializeField] private CardClass currentCardClass;
    public CardClass CurrentCardClass => currentCardClass;

    [SerializeField] private MoveType currentMoveType;
    public MoveType CurrentMoveType => currentMoveType;

    [SerializeField] private List<string> specialSkill;
    public List<string> SpecialSkill => specialSkill;

    // Stat 
    [SerializeField] private int defaultManaCost;
    public int ManaCost => defaultManaCost;

    [SerializeField] private int defaultAttackDamage;
    public int AttackDamage => defaultAttackDamage;
    
    [SerializeField] private int defaultHp;
    public int Hp => defaultHp;
}