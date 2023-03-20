using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UH2 : UnitCard
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
        unitInSelectTileCard.attack += 1;
        unitInSelectTileCard.attackText.text = unitInSelectTileCard.attack.ToString();

        isSkillDone = true;
    }
    public override void UnitHighlight()
    {
        tileManager.HighlightFriendlyUnitTiles(playerNo);
        Debug.Log("Highlight from unit 2");
    }
}
