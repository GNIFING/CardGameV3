using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UE1 : UnitCard
{
    void Start()
    {
        InitializeCardStats();
        UpdateCardUI();
    }
    public override void UnitSkill()
    {
        Tile unitTile = GetComponentInParent<Tile>();

        int xPos = unitTile.GetXPos();
        int yPos = unitTile.GetYPos();
        int offset = playerNo == 1 ? 1 : -1;
        int offset2 = playerNo == 1 ? 0 : 6 - 2 * xPos;
        for (int i = 1; i < 6 - xPos - offset2; i++) //0 1 2 3 4
        {
            Tile tile = GameObject.Find($"Tile {xPos + i * offset} {yPos}").GetComponent<Tile>();
            if (tile.GetUnitInTile() != null)
            {
                UnitCard selectedUnit = tile.GetUnitInTile().GetComponent<UnitCard>();
                if (selectedUnit.GetPlayerNo() != playerNo)
                {
                    selectedUnit.TakeDamage(this, 2);
                    break;
                }
            }
        }
        Debug.Log("Elf 1 Skill !");
        isSkillDone = true;
    }
    public override void UnitHighlight()
    {
        UnitSkill();
        isSkillDone = true;
        tileManager.NoHighlightUnit();
        Debug.Log("Highlight from Elf 1");
    }
}
