using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UO27 : UnitCard
{
    void Start()
    {
        InitializeCardStats();
        UpdateCardUI();
    }
    public override void UnitSkill()
    {
        List<UnitCard> friendlyUnits = tileManager.SelectFriendlyUnits(playerNo);
        foreach (UnitCard unitCard in friendlyUnits)
        {
            if (unitCard != this)
            {
                unitCard.IncreaseAttackDamage(2);
                unitCard.UpdateCardUI();
            }
        }
        List<UnitCard> enemyUnits = tileManager.SelectEnemyUnits(playerNo);
        foreach (UnitCard unitCard in enemyUnits)
        {
            if (unitCard != this)
            {
                unitCard.DecreaseAttackDamage(2);
                unitCard.UpdateCardUI();
            }
        }
        Debug.Log("Ogre 27 Skill !");
        isSkillDone = true;
    }
    public override void UnitHighlight()
    {
        UnitSkill();
        isSkillDone = true;
        tileManager.NoHighlightUnit();
        Debug.Log("Highlight from Ogre 27");
    }
}
