using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UE2 : UnitCard
{
    void Start()
    {
        InitializeCardStats();
        UpdateCardUI();
    }
    public override void UnitSkill()
    {
        Debug.Log("Elf 2 Skill !");
        isSkillDone = true;
    }

    public override void RangeAttack(UnitCard unitAttacked)
    {
        unitAttacked.TakeDamage(this, attack);
        RangeAttackAnimation(unitAttacked.gameObject);
        //------------------------//
        //Deal 1 damage to tower
        //------------------------//

        int arenaId = PlayerPrefs.GetInt("ArenaId");
        arenaId = gameController.arenaId;
        int attackerIndex = GetComponentInParent<Tile>().ConvertTilePosToIndex(GetComponentInParent<Tile>().GetXPos(), GetComponentInParent<Tile>().GetYPos());

        DataHandler dataHandler = FindObjectOfType<DataHandler>();
        int player1Id = dataHandler.player1Id;
        int player2Id = dataHandler.player2Id;

        int defenderId = gameController.GetPlayerId() == 1 ? player2Id : player1Id;

        // attack tower api
        gameController.arenaApiQueue.Enqueue(new ArenaApiQueue
        {
            path = "/attack/tower",
            arenaId = arenaId,
            defenderId = defenderId,
            attackerIndex = attackerIndex,
        });
    }
    public override void UnitHighlight()
    {
        isSkillDone = true;
        tileManager.NoHighlightUnit();
        Debug.Log("Highlight from Elf 2");
    }
}
