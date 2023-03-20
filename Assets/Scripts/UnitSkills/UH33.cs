using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UH33 : UnitCard
{
    void Start()
    {
        InitializeCardStats();
        UpdateCardUI();
    }

    public override void UnitSkill()
    {
        Debug.Log("Unit 31 Skill !");
        isSkillDone = true;
    }
    public override void UnitHighlight()
    {
        isSkillDone = true;
        tileManager.NoHighlightUnit();
        Debug.Log("Highlight from unit 31");
    }
    public override void EndTurnSkill()
    {
        Tile unitTile = GetComponentInParent<Tile>();
        if (unitTile.tileType != Tile.TileType.Player1Tower && unitTile.tileType != Tile.TileType.Player2Tower)
        {
            List<UnitCard> unitCards = tileManager.SelectFriendlyUnits(playerNo);
            foreach (UnitCard unitCard in unitCards)
            {
                if(unitCard != this)
                {
                    unitCard.health += 2;
                    unitCard.attack += 2;
                    unitCard.UpdateUICard();
                }
            }
        }
    }
}
