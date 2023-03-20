using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UH30 : UnitCard
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

        unitInSelectTileCard.health = 1;
        unitInSelectTileCard.attack = 1;

        unitInSelectTileCard.UpdateUICard();
        isSkillDone = true;
    }

    public override void UnitHighlight()
    {
        tileManager.HighlightEnemyUnitTiles(playerNo);
        //isSkillDone = true;
    }
}
