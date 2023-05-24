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
        GameObject unitInSelectTile = skillTargetUnit;
        Quaternion rotation = CalculateRotation(unitInSelectTile);
        GameObject bullet = Instantiate(bulletPrefab, transform.position, rotation);
        bullet.GetComponent<BulletScript>().SetTarget(unitInSelectTile.transform.parent.gameObject);
        unitInSelectTile.GetComponent<UnitCard>().TakeDamage(this, 3);
        isSkillDone = true;
    }

    public override void UnitHighlight()
    {
        tileManager.HighlightEnemyUnitTiles(playerNo);
        //isSkillDone = true;
    }
}
