using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UO30 : UnitCard
{
    void Start()
    {
        InitializeCardStats();
        UpdateCardUI();
    }
    public override void UnitSkill()
    {
        List<GameObject> allUnits = tileManager.GetAllUnits();
        foreach (GameObject unit in allUnits)
        {
            UnitCard unitCard = unit.GetComponent<UnitCard>(); 
            if (unitCard != this)
            {
                unitCard.TakeDamage(this, 5);
                unitCard.UpdateCardUI();
            }
        }
        Debug.Log("Ogre 30 Skill !");
        isSkillDone = true;
    }
    public override void UnitHighlight()
    {
        UnitSkill();
        isSkillDone = true;
        tileManager.NoHighlightUnit();
        Debug.Log("Highlight from Ogre 30");
    }
}
