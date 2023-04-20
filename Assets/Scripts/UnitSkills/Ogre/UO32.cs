using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UO32 : UnitCard
{
    private PlayerController playerController;
    void Start()
    {
        InitializeCardStats();
        UpdateCardUI();
    }
    public override void UnitSkill()
    {
        Debug.Log("Ogre 32 Skill !");
        isSkillDone = true;
    }
    public override void UnitHighlight()
    {
        isSkillDone = true;
        tileManager.NoHighlightUnit();
        Debug.Log("Highlight from Ogre 32");
    }

    public override void MeleeAttack(UnitCard unitAttacked)
    {
        TakeDamage(unitAttacked, unitAttacked.GetAttackDamage());
        unitAttacked.TakeDamage(this, attack);
        MeleeAttackAnimation(unitAttacked);
        
        playerController = GameObject.Find("PlayerController").GetComponent<PlayerController>();
        int playerHp = playerController.GetPlayerHP(playerNo);
        playerController.SetPlayerHP(playerNo, playerHp - 4);
    }
}
