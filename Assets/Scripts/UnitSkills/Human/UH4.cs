using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UH4 : UnitCard
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
        Debug.Log("Player NO = " + playerNo);

        if (playerNo == 1)
        {
            spawnP1Card.SpawnUnit();
            Debug.Log("SpawnUnit1");
        }else if(playerNo == 2)
        {
            spawnP2Card.SpawnUnit();
            Debug.Log("SpawnUnit2");
        }
        isSkillDone = true;
    }
    public override void UnitHighlight()
    {
        UnitSkill();
        tileManager.NoHighlightUnit();
        Debug.Log("Highlight from unit 4");
    }

    private IEnumerator Delay(float delay)
    {
        yield return new WaitForSeconds(delay);
    }
}