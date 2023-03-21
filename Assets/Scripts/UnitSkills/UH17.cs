using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UH17 : UnitCard
{
    void Start()
    {
        InitializeCardStats();
        UpdateCardUI();
    }
    public override void UnitSkill()
    {
        Debug.Log("Unit 17 Skill !");
        isSkillDone = true;
    }


    public override void UnitHighlight()
    {
        isSkillDone = true;
        tileManager.NoHighlightUnit();
        Debug.Log("Highlight from unit 17");
    }
    public override void TakeDamage(UnitCard attackUnitCard, int damage)
    {
        health -= damage;
        if(damage != 0)
        {
            attack += 2;
        }
        UpdateCardUI();
        if (health <= 0) Destroy(this.gameObject, 0.5f);
    }
}
