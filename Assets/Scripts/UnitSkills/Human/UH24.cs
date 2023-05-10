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

    public override IEnumerator MeleeAttack(UnitCard unitAttacked)
    {
        TakeDamage(unitAttacked, unitAttacked.GetAttackDamage());
        yield return new WaitForSeconds(0.5f);
        unitAttacked.TakeDamage(this, attack * 2);

        MeleeAttackAnimation(unitAttacked);

        if (unitAttacked.GetHealth() <= 0) Destroy(unitAttacked.gameObject, 0.5f);
        if (health <= 0) Destroy(gameObject, 0.5f);

        UpdateCardUI();
        unitAttacked.UpdateCardUI();
    }
}
