using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UH20 : UnitCard
{
    private Tile buffTile;
    private Tile unitTile;
    void Start()
    {
        InitializeCardStats();
        UpdateCardUI();
    }
    public override void UnitSkill()
    {
        Debug.Log("Unit 20 Skill !");
        unitTile = this.GetComponentInParent<Tile>();
        int xPos = unitTile.GetXPos();
        int yPos = unitTile.GetYPos();

        if (playerNo == 1)
        {
            buffTile = GameObject.Find($"Tile {xPos + 1} {yPos}").GetComponent<Tile>();
        }
        else
        {
            buffTile = GameObject.Find($"Tile {xPos - 1} {yPos}").GetComponent<Tile>();
        }
        if (buffTile.GetUnitInTile() != null && buffTile.GetUnitInTile().GetComponent<UnitCard>().GetPlayerNo() == playerNo)
        {
            UnitCard buffUnit = buffTile.GetUnitInTile().GetComponent<UnitCard>();
            buffUnit.attack += 4;
            buffUnit.health += 4;
            buffUnit.UpdateUICard();
        }
        isSkillDone = true;
    }
    public override void UnitHighlight()
    {
        UnitSkill();
        isSkillDone = true;
        tileManager.NoHighlightUnit();
        Debug.Log("Highlight from unit 20");
    }
}
