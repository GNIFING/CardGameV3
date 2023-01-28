using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CardStat")]
public class UnitCardStat : ScriptableObject
{
    public enum MoveType
    {
        straightShort,
        straightFar,
        diagonalShort,
        diagonalFar,
        round,
        horizontalShort,
        horizontalFar
    }

    public enum CardClass
    {
        human,
        ogre,
        elf
    }

    //------------ Name and Description ------------//
    [SerializeField] private int _cardIndex;
    public int CardIndex => _cardIndex;

    [SerializeField] private string _cardName;
    public string CardName => _cardName;

    [SerializeField] private string _cardDescription;
    public string CardDescription => _cardDescription;

    //------------ Card Type ------------// 
    [SerializeField] private CardClass _currentCardClass;
    public CardClass CurrentCardClass => _currentCardClass;

    [SerializeField] private MoveType _currentMoveType;
    public MoveType CurrentMoveType => _currentMoveType;

    [SerializeField] private List<string> _specialSkill;
    public List<string> SpecialSkill => _specialSkill;

    //------------ Stat ------------// 
    [SerializeField] private int _defaultManaCost;
    public int ManaCost => _defaultManaCost;

    [SerializeField] private int _defaultHp;
    public int Hp => _defaultHp;

    [SerializeField] private int _defaultAttackDamage;
    public int AttackDamage => _defaultAttackDamage;
}
