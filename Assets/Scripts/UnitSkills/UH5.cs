using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UH5 : UnitCard
{
    void Start()
    {
        InitializeCardStats();
        UpdateCardUI();
    }
    public override void UnitSkill()
    {
        Debug.Log("Unit 5 Skill !");
        isSkillDone = true;
    }
    public override void UnitHighlight()
    {
        isSkillDone = true;
        tileManager.NoHighlightUnit();
        Debug.Log("Highlight from unit 5");
    }

    public override void TakeDamage(UnitCard attackUnitCard, int damage)
    {
        health -= damage;
        attackUnitCard.health -= damage;

        if (attackUnitCard.health <= 0) Destroy(attackUnitCard.gameObject, 0.5f);
        if (health <= 0) Destroy(this.gameObject, 0.5f);

        UpdateUICard();
        attackUnitCard.UpdateUICard();
    }
}