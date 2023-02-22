using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MagicCard : MonoBehaviour
{
    // Common properties for all MagicCards
    public string cardName;
    public string description;
    public int manaCost;
    public Sprite image;

    public TileManager tileManager;

    protected List<GameObject> highlightTargets;
    protected List<GameObject> selectTargets;

    protected HighlightUnitType highlightUnitType;

    public enum HighlightUnitType
    {
        FriendlyOnly,
        EnemyOnly,
        FriendlyUnitOnly,
        EnemyUnitOnly,
        UnitOnly,
        AllTarget,
        OwnMagic
    }



    // Common methods for all MagicCards
    public virtual void Play()
    {
        // Play the card
    }

    public void HighlightUnits(HighlightUnitType highlightType, int playerNo)
    {
        switch (highlightType)
        {
            case HighlightUnitType.FriendlyOnly:
                HighlightFriendlyUnit(playerNo);
                // highlight friendly units only
                break;
            case HighlightUnitType.EnemyOnly:
                // highlight enemy units only
                break;
            case HighlightUnitType.AllTarget:
                // highlight all units (friendly and enemy)
                break;
            case HighlightUnitType.UnitOnly:
                // highlight all units (friendly and enemy) and non-units
                break;
            case HighlightUnitType.EnemyUnitOnly:
                // highlight enemy units only
                break;
            case HighlightUnitType.FriendlyUnitOnly:
                // highlight friendly units only
                break;
            case HighlightUnitType.OwnMagic:
                break;
            default:
                // handle invalid highlight type
                break;
        }
    }

    private void HighlightFriendlyUnit(int playerNo)
    {
        highlightTargets = tileManager.HighlightFriendlyUnitTiles(playerNo);
    }
}