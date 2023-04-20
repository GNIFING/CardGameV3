using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UH29 : UnitCard
{
    void Start()
    {
        InitializeCardStats();
        UpdateCardUI();
    }

    public override void UnitSkill()
    {
        Debug.Log("Unit 11 Skill !");
        isSkillDone = true;
    }
    public override void UnitHighlight()
    {
        isSkillDone = true;
        tileManager.NoHighlightUnit();
        Debug.Log("Highlight from unit 11");
    }

    public override void StartTurnSkill()
    {
        Tile unitTile = GetComponentInParent<Tile>();
        if (unitTile.tileType != Tile.TileType.Player1Tower && unitTile.tileType != Tile.TileType.Player2Tower)
        {
            SetAttackDamage(GetAttackDamage() + 2);
            SetHealth(GetHealth() + 2);
            UpdateCardUI();
        }
    }
}
