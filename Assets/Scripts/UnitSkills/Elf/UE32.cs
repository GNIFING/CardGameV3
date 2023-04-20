using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UE32 : UnitCard
{
    void Start()
    {
        InitializeCardStats();
        UpdateCardUI();
    }

    public override void UnitSkill()
    {
        Tile unitTile = GetComponentInParent<Tile>();
        for (int i = 0; i < 7; i++)
        {
            Tile tile = GameObject.Find($"Tile {i} {unitTile.GetYPos()}").GetComponent<Tile>();
            if (tile.GetUnitInTile() != null && tile.GetUnitInTile().GetComponent<UnitCard>().GetPlayerNo() != playerNo)
            {
                GameObject unitCard = tile.GetUnitInTile();
                //---------destroy card---------//
                Destroy(unitCard);
            }
        }
        Debug.Log("Unit 28 Skill !");
        isSkillDone = true;
    }
    public override void UnitHighlight()
    {
        UnitSkill();
        isSkillDone = true;
        tileManager.NoHighlightUnit();
        Debug.Log("Highlight from unit 28");
    }
}
