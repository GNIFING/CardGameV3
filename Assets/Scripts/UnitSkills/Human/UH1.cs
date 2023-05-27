using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UH1 : UnitCard
{
    void Start()
    {
        InitializeCardStats();
        UpdateCardUI();
    }

    public override void UnitSkill()
    {
        //GameObject unitInSelectTile = skillTargetUnit;
        //Quaternion rotation = CalculateRotation(unitInSelectTile);
        //GameObject bullet = Instantiate(bulletPrefab, transform.position, rotation);
        //bullet.GetComponent<BulletScript>().SetTarget(unitInSelectTile.transform.parent.gameObject);

        int arenaId = gameController.arenaId;
        Tile attackerTile = this.GetComponentInParent<Tile>();
        int attackerIndex = attackerTile.ConvertTilePosToIndex(attackerTile.GetXPos(), attackerTile.GetYPos());
        Tile defenderTile = skillTargetUnit.GetComponentInParent<Tile>();
        int defenderIndex = defenderTile.ConvertTilePosToIndex(defenderTile.GetXPos(), defenderTile.GetYPos());
        multiPlayerController = FindObjectOfType<MultiPlayerController>();

        // attack card api
        gameController.arenaApiQueue.Enqueue(new ArenaApiQueue
        {
            path = "/attack/card",
            arenaId = arenaId,
            attackerIndex = attackerIndex,
            defenderIndex = defenderIndex,
        });
        // update card api

        skillTargetUnit.GetComponent<UnitCard>().TakeDamage(this, 1);
        
        isSkillDone = true;
    }

    public override void UnitHighlight()
    {
        tileManager.HighlightEnemyUnitTiles(playerNo);
        //isSkillDone = true;
    }
}
