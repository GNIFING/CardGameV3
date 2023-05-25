using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UO18 : UnitCard
{
    void Start()
    {
        InitializeCardStats();
        UpdateCardUI();
    }
    public override void UnitSkill()
    {
        Debug.Log("Ogre 18 Skill !");
        isSkillDone = true;
    }
    public override void UnitHighlight()
    {
        isSkillDone = true;
        tileManager.NoHighlightUnit();
        Debug.Log("Highlight from Ogre 18");
    }

    public override void TakeDamage(UnitCard attackUnitCard, int damage)
    {
        DecreaseHealth(damage);
        IncreaseAttackDamage(damage);

        UpdateCardUI();
        attackUnitCard.UpdateCardUI();
    }
}
