using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UE4 : UnitCard
{
    void Start()
    {
        InitializeCardStats();
        UpdateCardUI();
    }
    public override void UnitSkill()
    {
        Debug.Log("Elf 4 Skill !");
        isSkillDone = true;
    }

    public override void UnitHighlight()
    {
        isSkillDone = true;
        tileManager.NoHighlightUnit();
        Debug.Log("Highlight from Elf 4");
    }

    public override void RangeAttack(UnitCard unitAttacked)
    {
        //If Enemy dies, it dies
        if (unitAttacked.GetHealth() <= attack)
        {
            this.TakeDamage(this, health);
        }
        //------------------------//
        unitAttacked.TakeDamage(this, attack);
        RangeAttackAnimation(unitAttacked.gameObject);


    }
}
