using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UH16 : UnitCard
{
    void Start()
    {
        InitializeCardStats();
        UpdateCardUI();
    }

    public override void UnitSkill()
    {
        Debug.Log("Unit 16 Skill !");
        isSkillDone = true;
    }
    public override void UnitHighlight()
    {
        isSkillDone = true;
        tileManager.NoHighlightUnit();
        Debug.Log("Highlight from unit 16");
    }

    public override void EndTurnSkill()
    {
        Tile unitTile = GetComponentInParent<Tile>();
        if(unitTile.tileType != Tile.TileType.Player1Tower && unitTile.tileType != Tile.TileType.Player2Tower)
        {
            for (int i = 0; i < 7; i++)
            {
                Tile tile = GameObject.Find($"Tile {i} {unitTile.GetYPos()}").GetComponent<Tile>();
                if (tile.GetUnitInTile() != null && tile.GetUnitInTile() != this.gameObject)
                {
                    UnitCard unitCard = tile.GetUnitInTile().GetComponent<UnitCard>();
                    unitCard.DecreaseHealth(2);

                }
            }
            DecreaseHealth(2);
            UpdateCardUI();
        }
    }
}
