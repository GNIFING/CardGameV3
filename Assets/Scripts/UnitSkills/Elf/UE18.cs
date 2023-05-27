using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UE18 : UnitCard
{
    void Start()
    {
        InitializeCardStats();
        UpdateCardUI();
    }
    public override void UnitSkill()
    {
        Debug.Log("Elf 9 Skill !");
        isSkillDone = true;
    }
    public override void UnitHighlight()
    {
        isSkillDone = true;
        tileManager.NoHighlightUnit();
        Debug.Log("Highlight from Elf 9");
    }

    public override IEnumerator MeleeAttack(UnitCard unitAttacked)
    {
        TakeDamage(unitAttacked, unitAttacked.GetAttackDamage());
        yield return new WaitForSeconds(0.5f);

        if (unitAttacked.GetAttackDamage() < 3)
        {
            unitAttacked.TakeDamage(this, unitAttacked.GetHealth());
        }
        unitAttacked.TakeDamage(this, attack);

        MeleeAttackAnimation(unitAttacked);
    }
}
