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

        StartCoroutine(multiPlayerController.AttackCard(arenaId, attackerIndex, defenderIndex, (response) => { Debug.Log("Skill Card Done"); }));
        StartCoroutine(multiPlayerController.UpdateCard(arenaId, defenderIndex, -1, 0, (response) => { Debug.Log("Update Card Done"); }));
        StartCoroutine(multiPlayerController.MarkUseCard(arenaId, attackerIndex, (response) => { }));

        isSkillDone = true;
    }

    public override void UnitHighlight()
    {
        tileManager.HighlightEnemyUnitTiles(playerNo);
        //isSkillDone = true;
    }
}
