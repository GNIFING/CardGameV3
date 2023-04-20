using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UE2 : UnitCard
{
    void Start()
    {
        InitializeCardStats();
        UpdateCardUI();
    }
    public override void UnitSkill()
    {
        Debug.Log("Elf 2 Skill !");
        isSkillDone = true;
    }

    public override void RangeAttack(UnitCard unitAttacked)
    {
        unitAttacked.TakeDamage(this, attack);
        RangeAttackAnimation(unitAttacked.gameObject);
        //------------------------//
        //Deal 1 damage to tower
        //------------------------//
    }
    public override void UnitHighlight()
    {
        isSkillDone = true;
        tileManager.NoHighlightUnit();
        Debug.Log("Highlight from Elf 2");
    }
}
