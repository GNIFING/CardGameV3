using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MagicCardStat")]
public class MagicCardStat : ScriptableObject
{
    public enum CardClass
    {
        Human,
        Ogre,
        Elf
    }

    [SerializeField] private int cardIndex;
    [SerializeField] private string cardName;
    [SerializeField] private Sprite cardImage;
    [SerializeField] private string cardDescription;
    [SerializeField] private CardClass currentCardClass;
    [SerializeField] private int defaultManaCost;

    public int CardIndex => cardIndex;
    public string CardName => cardName;
    public Sprite CardImage => cardImage;
    public string CardDescription => cardDescription;

    public CardClass CurrentCardClass => currentCardClass;
    public int ManaCost => defaultManaCost;

}
