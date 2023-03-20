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
            unitCards[ran1].health += 2;
            unitCards[ran2].health += 2;
            unitCards[ran1].attack += 2;
            unitCards[ran2].attack += 2;
            unitCards[ran1].UpdateUICard();
            unitCards[ran2].UpdateUICard();
        }
        
        else if(unitCards.Count == 1)
        {
            unitCards[0].health += 2;
            unitCards[0].attack += 2;
            unitCards[0].UpdateUICard();
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
