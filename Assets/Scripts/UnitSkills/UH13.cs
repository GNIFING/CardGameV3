using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UH13 : UnitCard
{
    private PlayerController playerController;
    void Start()
    {
        InitializeCardStats();
        UpdateCardUI();
    }

    public override void UnitSkill()
    {
        playerController = GameObject.Find("PlayerController").GetComponent<PlayerController>();
        int playerHp = playerController.GetPlayerHP(playerNo);
        if(playerHp >= 17)
        {
            playerController.SetPlayerHP(playerNo, 20);
        }
        else
        {
            playerController.SetPlayerHP(playerNo, playerHp + 3);
        }
        Debug.Log("Unit 13 Skill !");
        isSkillDone = true;
    }
    public override void UnitHighlight()
    {
        UnitSkill();
        isSkillDone = true;
        tileManager.NoHighlightUnit();
        Debug.Log("Highlight from unit 13");
    }
}
