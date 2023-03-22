using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UE15 : UnitCard
{
    private SpawnCard spawnP1Card;
    private SpawnCard spawnP2Card;
    void Start()
    {
        spawnP1Card = GameObject.Find("Player1").GetComponent<SpawnCard>();
        spawnP2Card = GameObject.Find("Player2").GetComponent<SpawnCard>();
        InitializeCardStats();
        UpdateCardUI();
    }
    public override void UnitSkill()
    {
        Debug.Log("Elf 15 Skill !");
        isSkillDone = true;
    }
    public override void UnitHighlight()
    {
        isSkillDone = true;
        tileManager.NoHighlightUnit();
        Debug.Log("Highlight from Elf 15");
    }

    public override void RangeAttack(UnitCard unitAttacked)
    {
        if(unitAttacked.GetHealth() > attack)
        {
            if (playerNo == 1)
            {
                spawnP1Card.SpawnUnit();
            }
            else if (playerNo == 2)
            {
                spawnP2Card.SpawnUnit();
            }
            isSkillDone = true;
        }
        unitAttacked.TakeDamage(this, attack);
        RangeAttackAnimation(unitAttacked.gameObject);

        
    }

}
