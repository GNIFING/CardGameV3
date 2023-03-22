using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UE13 : UnitCard
{
    void Start()
    {
        InitializeCardStats();
        UpdateCardUI();
    }

    public override void UnitSkill()
    {
        GameObject unitInSelectTile = skillTargetUnit;
        UnitCard unitInSelectTileCard = unitInSelectTile.GetComponent<UnitCard>();
        unitInSelectTileCard.IncreaseHealth(3);
        isSkillDone = true;
    }
    public override void UnitHighlight()
    {
        tileManager.HighlightFriendlyUnitTiles(playerNo);
        Debug.Log("Highlight from Elf 13");
    }
}
