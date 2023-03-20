using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UH27 : UnitCard
{
    private PlayerController playerController;
    private int damageToTower = 3;

    void Start()
    {
        playerController = GameObject.Find("PlayerController").GetComponent<PlayerController>();
        InitializeCardStats();
        UpdateCardUI();
    }

    public override void UnitSkill()
    {
        Debug.Log("Unit 27 Skill !");
        isSkillDone = true;
    }
    public override void UnitHighlight()
    {
        isSkillDone = true;
        tileManager.NoHighlightUnit();
        Debug.Log("Highlight from unit 27");
    }

    public override void StartTurnSkill()
    {
        Tile unitTile = GetComponentInParent<Tile>();
        if (unitTile.tileType != Tile.TileType.Player1Tower && unitTile.tileType != Tile.TileType.Player2Tower)
        {
            if (playerNo == 1)
            {
                playerController.SetPlayerHP(2, playerController.GetPlayerHP(2) - damageToTower);
            }
            if (playerNo == 2)
            {
                playerController.SetPlayerHP(1, playerController.GetPlayerHP(1) - damageToTower);
            }
        }
    }
}
