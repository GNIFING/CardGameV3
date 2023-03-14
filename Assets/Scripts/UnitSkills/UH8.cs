using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UH8 : UnitCard
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
        unitInSelectTileCard.health += 1;
        unitInSelectTileCard.healthText.text = unitInSelectTileCard.health.ToString();

        isSkillDone = true;
    }
    public override void UnitHighlight()
    {
        tileManager.HighlightFriendlyUnitTiles(playerNo);
        Debug.Log("Highlight from unit 8");
    }
}
