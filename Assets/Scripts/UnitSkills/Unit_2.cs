using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_2 : UnitCard
{
    void Start()
    {
        InitializeCardStats();
        UpdateCardUI();
    }

    public override void UnitSkill(GameObject unitInSelectTile, int tileXPos, int tileYPos)
    {
        Debug.Log("Unit 2 Skill !");
        isSkillDone = true;
    }
    public override void UnitHighlight()
    {
        tileManager.NoHighlightUnit();
        Debug.Log("Highlight from unit 2");
    }
}
