using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UH18 : UnitCard
{
    void Start()
    {
        InitializeCardStats();
        UpdateCardUI();
    }
    public override void UnitSkill()
    {
        Debug.Log("Unit 18 Skill !");
        isSkillDone = true;
    }
    public override void UnitHighlight()
    {
        UnitSkill();
        isSkillDone = true;
        tileManager.NoHighlightUnit();
        Debug.Log("Highlight from unit 18");
    }
}
