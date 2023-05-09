using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UH3 : UnitCard
{
    [SerializeField]
    private GameObject UH3_1_Prefab;

    private Tile unitTile;
    private Tile summonTile;
    
    void Start()
    {
        InitializeCardStats();
        UpdateCardUI();
    }

    public override void UnitSkill()
    {
        Debug.Log("Unit 3 Skill !");
        unitTile = this.GetComponentInParent<Tile>();
        int xPos = unitTile.GetXPos();
        int yPos = unitTile.GetYPos();
        
        if(playerNo == 1)
        {
            summonTile = GameObject.Find($"Tile {xPos + 1} {yPos}").GetComponent<Tile>();
        }
        else
        {
            summonTile = GameObject.Find($"Tile {xPos - 1} {yPos}").GetComponent<Tile>();
        }
        if (summonTile.GetUnitInTile() == null)
            {
                GameObject UH3_1 = Instantiate(UH3_1_Prefab, summonTile.transform.position, Quaternion.identity);
                UH3_1.transform.parent = summonTile.transform;
                UnitCard UH3_1Card = UH3_1.GetComponent<UnitCard>();
                UH3_1Card.SetPlayerNo(playerNo);
                UH3_1Card.isPlayCard = true;
                UH3_1Card.isSkillDone = true;
            }
        isSkillDone = true;
    }
    public override void UnitHighlight()
    {
        UnitSkill();
        isSkillDone = true;
        tileManager.NoHighlightUnit();
        Debug.Log("Highlight from unit 3");
    }
}