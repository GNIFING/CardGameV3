using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UO1 : UnitCard
{
    void Start()
    {
        InitializeCardStats();
        UpdateCardUI();
    }

    public override void UnitSkill()
    {
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
        gameController.arenaApiQueue.Enqueue(new ArenaApiQueue
        {
            path = "/update/card",
            arenaId = arenaId,
            cardIndex = defenderIndex,
            hp = -3,
            atk = 0,
        });
        //StartCoroutine(multiPlayerController.AttackCard(arenaId, attackerIndex, defenderIndex, (response) => {
        //    StartCoroutine(multiPlayerController.UpdateCard(arenaId, defenderIndex, -3, 0, (response) => {
        //        //StartCoroutine(multiPlayerController.MarkUseCard(arenaId, attackerIndex, (response) => { }));
        //    }));
        //}));

        isSkillDone = true;
    }

    public override void UnitHighlight()
    {
        tileManager.HighlightEnemyUnitTiles(playerNo);
        //isSkillDone = true;
    }
}
