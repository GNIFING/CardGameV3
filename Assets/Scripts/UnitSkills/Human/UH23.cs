using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UH23 : UnitCard
{
    void Start()
    {
        InitializeCardStats();
        UpdateCardUI();
    }
    public override void UnitSkill()
    {
        List<UnitCard> unitCards = tileManager.SelectFriendlyUnits(playerNo);
        unitCards.Remove(this);
        Debug.Log("UnitCard Count = " + unitCards.Count);
        if(unitCards.Count >= 2)
        {
            int ran1 = Random.Range(0, unitCards.Count);
            int ran2;
            do
            {
                ran2 = Random.Range(0, unitCards.Count);
            } while (ran2 == ran1);
            unitCards[ran1].IncreaseHealth(2);
            unitCards[ran2].IncreaseHealth(2);
            unitCards[ran1].IncreaseAttackDamage(2);
            unitCards[ran2].IncreaseAttackDamage(2);
        }
        
        else if(unitCards.Count == 1)
        {
            unitCards[0].IncreaseHealth(2);
            unitCards[0].IncreaseAttackDamage(2);
        }
        Debug.Log("Unit 23 Skill !");
        isSkillDone = true;
    }
    public override void UnitHighlight()
    {
        UnitSkill();
        isSkillDone = true;
        tileManager.NoHighlightUnit();
        Debug.Log("Highlight from unit 23");
    }
}
