using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UO20 : UnitCard
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
        UnitCard selectedUnit = unitInSelectTile.GetComponent<UnitCard>();
        selectedUnit.TakeDamage(this, 4);
        if(selectedUnit.GetHealth() <= 4)
        {
            Tile tileFromSelectUnit = unitInSelectTile.GetComponentInParent<Tile>();
            StartCoroutine(DelayMove(tileFromSelectUnit));
        }
        isSkillDone = true;
    }

    public override void UnitHighlight()
    {
        tileManager.HighlightEnemyUnitTiles(playerNo);
        //isSkillDone = true;
    }

    public IEnumerator DelayMove(Tile tileFromSelectUnit)
    {
        yield return new WaitForSeconds(2f);
        tileFromSelectUnit.MoveUnitToThisTile(this.gameObject);

    }
}
