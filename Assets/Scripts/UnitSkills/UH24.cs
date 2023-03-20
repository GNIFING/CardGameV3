using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UH24 : UnitCard
{
    void Start()
    {
        InitializeCardStats();
        UpdateCardUI();
    }
    public override void UnitSkill()
    {
        Debug.Log("Unit 24 Skill !");
        isSkillDone = true;
    }
    public override void UnitHighlight()
    {
        UnitSkill();
        isSkillDone = true;
        tileManager.NoHighlightUnit();
        Debug.Log("Highlight from unit 24");
    }

    public override void MeleeAttack(UnitCard unitAttacked)
    {
        TakeDamage(unitAttacked, unitAttacked.attack);
        unitAttacked.TakeDamage(this, attack * 2);

        if (unitAttacked.health <= 0) Destroy(unitAttacked.gameObject, 0.5f);
        if (health <= 0) Destroy(gameObject, 0.5f);

        UpdateUICard();
        unitAttacked.UpdateUICard();
    }
}
