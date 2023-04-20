using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UE21 : UnitCard
{
    void Start()
    {
        InitializeCardStats();
        UpdateCardUI();
    }
    public override void UnitSkill()
    {
        GameObject unitInSelectTile = skillTargetUnit;
        int unitSwapAttackDmg = unitInSelectTile.GetComponent<UnitCard>().GetAttackDamage();
        int thisUnitAttackDmg = GetAttackDamage();
        unitInSelectTile.GetComponent<UnitCard>().SetAttackDamage(thisUnitAttackDmg);
        SetAttackDamage(unitSwapAttackDmg);
        isSkillDone = true;
    }
    public override void UnitHighlight()
    {
        tileManager.HighlightEnemyUnitTiles(playerNo);
        Debug.Log("Highlight from Elf 21");
    }
}
