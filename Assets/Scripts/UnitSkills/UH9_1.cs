using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UH9_1 : UnitCard
{
    void Start()
    {
        InitializeCardStats();
        UpdateCardUI();
        ReduceCardCredit();
    }

    public override void UnitSkill()
    {
        Debug.Log("Unit 9_1 Skill !");
        isSkillDone = true;
    }
    public override void UnitHighlight()
    {
        isSkillDone = true;
        tileManager.NoHighlightUnit();
        Debug.Log("Highlight from unit 9");
    }
}
