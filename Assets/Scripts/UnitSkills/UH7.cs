using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UH7 : UnitCard
{
    private PlayerController playerController;
    void Start()
    {
        playerController = GameObject.Find("PlayerController").GetComponent<PlayerController>();
        InitializeCardStats();
        UpdateCardUI();
    }
    public override void UnitSkill()
    {
        Debug.Log("Unit 7 Skill !");
        isSkillDone = true;
    }
    public override void UnitHighlight()
    {
        isSkillDone = true;
        tileManager.NoHighlightUnit();
        Debug.Log("Highlight from unit 7");
    }

    public override void EndTurnSkill()
    {
        Tile unitTile = GetComponentInParent<Tile>();
        if (unitTile.tileType != Tile.TileType.Player1Tower && unitTile.tileType != Tile.TileType.Player2Tower)
        {
            int xPos = unitTile.GetXPos();
            int yPos = unitTile.GetYPos();
            bool hasUnit = false;
            int offset = playerNo == 1 ? 1 : -1;
            int offset2 = playerNo == 1 ? 0 : 6 - 2 * xPos;
            for (int i = 1; i < 6 - xPos - offset2; i++) //0 1 2 3 4
            {
                Tile tile = GameObject.Find($"Tile {xPos + i * offset} {yPos}").GetComponent<Tile>();
                if(tile.GetUnitInTile() != null)
                {
                    UnitCard selectedUnit = tile.GetUnitInTile().GetComponent<UnitCard>();
                    if(selectedUnit.GetPlayerNo() != playerNo)
                    {
                        selectedUnit.TakeDamage(this, 1);
                        hasUnit = true;
                        break;
                    }
                }
            }

            if (!hasUnit)
            {
                if(playerNo == 1)
                {
                    playerController.SetPlayerHP(2, playerController.GetPlayerHP(2) - 1);
                }
                if (playerNo == 2)
                {
                    playerController.SetPlayerHP(1, playerController.GetPlayerHP(1) - 1);
                }
            }        

        }
    }
}
