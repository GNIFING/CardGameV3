using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UH25 : UnitCard
{
    private PlayerController playerController;
    void Start()
    {
        playerController = GameObject.Find("PlayerController").GetComponent<PlayerController>();
        InitializeCardStats();
        UpdateCardUI();
    }
    public override void UnitSkill()
    {
        Debug.Log("Unit 25 Skill !");
        isSkillDone = true;
    }
    public override void UnitHighlight()
    {
        UnitSkill();
        isSkillDone = true;
        tileManager.NoHighlightUnit();
        Debug.Log("Highlight from unit 25");
    }

    public override IEnumerator MeleeAttack(UnitCard unitAttacked)
    {
        TakeDamage(unitAttacked, unitAttacked.GetAttackDamage());
        yield return new WaitForSeconds(0.5f);
        unitAttacked.TakeDamage(this, attack);
        MeleeAttackAnimation(unitAttacked);

        int xPos = unitAttacked.GetComponentInParent<Tile>().GetXPos();
        int yPos = unitAttacked.GetComponentInParent<Tile>().GetYPos();

        if(xPos != 0 || xPos != 6)
        {
            if(xPos == 5 && playerNo == 1)
            {
                playerController.SetPlayerHP(2, playerController.GetPlayerHP(2) - attack);
            }
            else if(xPos == 1 && playerNo == 2)
            {
                playerController.SetPlayerHP(1, playerController.GetPlayerHP(1) - attack);
            }

            int offset = this.GetComponentInParent<Tile>().GetXPos() < xPos ? 1 : -1;
            Tile pierceTile = GameObject.Find($"Tile {xPos + offset} {yPos}").GetComponent<Tile>();
            UnitCard pierceUnit;
            if (pierceTile.GetUnitInTile() != null)
            {
                pierceUnit = pierceTile.GetUnitInTile().GetComponent<UnitCard>();
                pierceUnit.TakeDamage(this, attack);
                if (pierceUnit.GetHealth() <= 0) Destroy(pierceUnit.gameObject, 0.5f);
                pierceUnit.UpdateCardUI();
            }
        }

        if (unitAttacked.GetHealth() <= 0) Destroy(unitAttacked.gameObject, 0.5f);
        if (health <= 0) Destroy(gameObject, 0.5f);

        UpdateCardUI();
        unitAttacked.UpdateCardUI();
    }
}
