using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UH21 : UnitCard
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
        List<UnitCard> unitCards = tileManager.SelectFriendlyUnits(playerNo);
        foreach (UnitCard friendlyUnit in unitCards)
        {
            if(friendlyUnit != this)
            {
                friendlyUnit.health += 1;
                friendlyUnit.UpdateUICard();
            }
        }

        if (playerNo == 1)
        {
            spawnP1Card.SpawnUnit();
        }
        else if (playerNo == 2)
        {
            spawnP2Card.SpawnUnit();
        }

        isSkillDone = true;
        Debug.Log("Unit 21 Skill !");
        isSkillDone = true;
    }
    public override void UnitHighlight()
    {
        UnitSkill();
        isSkillDone = true;
        tileManager.NoHighlightUnit();
        Debug.Log("Highlight from unit 21");
    }
}
