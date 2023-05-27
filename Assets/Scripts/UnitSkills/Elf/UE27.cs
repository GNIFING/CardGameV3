using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UE27 : UnitCard
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
        int drawNumber = unitCards.Count - 1;
        DataHandler dataHandler = FindObjectOfType<DataHandler>();
        
        if(gameController.GetPlayerId() == 1 && dataHandler.player1CardLeft <= drawNumber)
        {
            drawNumber = dataHandler.player1CardLeft;
        }
        else if (gameController.GetPlayerId() == 2 && dataHandler.player2CardLeft <= drawNumber)
        {
            drawNumber = dataHandler.player2CardLeft;
        }

        for (int i = 0; i < drawNumber; i++)
        {
            if (playerNo == 1)
            {
                spawnP1Card.SpawnUnit();
            }
            else if (playerNo == 2)
            {
                spawnP2Card.SpawnUnit();
            }
        }
        isSkillDone = true;
    }
    public override void UnitHighlight()
    {
        UnitSkill();
        isSkillDone = true;
        tileManager.NoHighlightUnit();
        Debug.Log("Highlight from Elf 27");
    }
}
