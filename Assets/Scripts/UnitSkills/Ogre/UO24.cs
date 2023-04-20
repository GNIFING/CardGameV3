using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UO24 : UnitCard
{
    int currentSelectedUnitHP;
    bool skillDone;
    void Start()
    {
        InitializeCardStats();
        UpdateCardUI();
    }

    public override void UnitSkill()
    {
        GameObject unitInSelectTile = skillTargetUnit;
        currentSelectedUnitHP = unitInSelectTile.GetComponent<UnitCard>().GetHealth(); 
        Debug.Log("Ogre Skill !");
        isSkillDone = true;
    }
    public override void UnitHighlight()
    {
        tileManager.HighlightEnemyUnitTiles(playerNo);
        Debug.Log("Highlight from unit 11");
    }

    public override void StartTurnSkill()
    {
        GameObject unitInSelectTile = skillTargetUnit;
        if (unitInSelectTile.GetComponent<UnitCard>().GetHealth() >= currentSelectedUnitHP && !skillDone)
        {
            Destroy(skillTargetUnit, 0.5f);
            skillDone = true;
        }
        
    }
}
