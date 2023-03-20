using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UH19_1 : UnitCard
{
    void Start()
    {
        InitializeCardStats();
        UpdateCardUI();
        ReduceCardCredit();
    }

    public override void UnitSkill()
    {
        Debug.Log("Unit 19_1 Skill !");
        isSkillDone = true;
    }
    public override void UnitHighlight()
    {
        isSkillDone = true;
        tileManager.NoHighlightUnit();
        Debug.Log("Highlight from unit 19_1");
    }
}
