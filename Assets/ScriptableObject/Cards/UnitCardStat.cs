using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField] private string cardName;
    [SerializeField] private Image cardImage;
    [SerializeField] private string cardDescription;
    [SerializeField] private CardClass currentCardClass;
    [SerializeField] private MoveType currentMoveType;
    [SerializeField] private List<string> specialSkill;
    [SerializeField] private int defaultManaCost;
    [SerializeField] private int defaultAttackDamage;
    [SerializeField] private int defaultHp;

    public int CardIndex => cardIndex;
    public string CardName => cardName;
    public Image CardImage => cardImage;
    public string CardDescription => cardDescription;

    // Card Type 
    public CardClass CurrentCardClass => currentCardClass;
    public MoveType CurrentMoveType => currentMoveType;
    public List<string> SpecialSkill => specialSkill;

    // Stat 
    public int ManaCost => defaultManaCost;
    public int AttackDamage => defaultAttackDamage;
    public int Hp => defaultHp;
}