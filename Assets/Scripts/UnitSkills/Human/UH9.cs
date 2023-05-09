using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UH9 : UnitCard
{
    [SerializeField]
    private GameObject UH9_1_Prefab;

    private Tile unitTile;
    private Tile summonTile;

    void Start()
    {
        InitializeCardStats();
        UpdateCardUI();
    }

    public override void UnitSkill()
    {
        Debug.Log("Unit 9 Skill !");
        unitTile = this.GetComponentInParent<Tile>();
        int xPos = unitTile.GetXPos();
        int yPos = unitTile.GetYPos();


        if (yPos != 0)
        {
            summonTile = GameObject.Find($"Tile {xPos} {yPos - 1}").GetComponent<Tile>();
            if (summonTile.GetUnitInTile() == null)
            {
                GameObject UH9_1 = Instantiate(UH9_1_Prefab, summonTile.transform.position, Quaternion.identity);
                UH9_1.transform.parent = summonTile.transform;
                UnitCard UH9_1Card = UH9_1.GetComponent<UnitCard>();
                UH9_1Card.SetPlayerNo(playerNo);
                UH9_1Card.isPlayCard = true;
                UH9_1Card.isSkillDone = true;
            }
        }
        isSkillDone = true;
    }
    public override void UnitHighlight()
    {
        UnitSkill();
        isSkillDone = true;
        tileManager.NoHighlightUnit();
        Debug.Log("Highlight from unit 9");
    }
}
