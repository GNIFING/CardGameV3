using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_1 : UnitCard
{
    public GameObject bulletPrefab;

    void Start()
    {
        InitializeCardStats();
        UpdateCardUI();
    }

    public override void UnitSkill(GameObject unitInSelectTile, int tileXPos, int tileYPos)
    {
        Quaternion rotation = CalculateRotation(unitInSelectTile);
        GameObject bullet = Instantiate(bulletPrefab, transform.position, rotation);
        bullet.GetComponent<BulletScript>().SetTarget(unitInSelectTile.transform.parent.gameObject);
        DealDamageToUnit(unitInSelectTile, 1);
        isSkillDone = true;
    }

    public override void UnitHighlight()
    {
        tileManager.HighlightEnemyUnitTiles(playerNo);
        //isSkillDone = true;
    }
}
