using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UH26 : UnitCard
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
        unitInSelectTileCard.health += 4;
        unitInSelectTileCard.healthText.text = unitInSelectTileCard.health.ToString();
        
        Debug.Log("Unit 26 Skill !");
        isSkillDone = true;
    }
    public override void UnitHighlight()
    {
        tileManager.HighlightFriendlyUnitTiles(playerNo);
        Debug.Log("Highlight from unit 26");
    }
}
