using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UH19 : UnitCard
{
    [SerializeField]
    private GameObject UH19_1_Prefab;

    private Tile unitTile;
    private Tile summonTile;

    void Start()
    {
        InitializeCardStats();
        UpdateCardUI();
    }

    public override void UnitSkill()
    {
        Debug.Log("Unit 19 Skill !");
        unitTile = this.GetComponentInParent<Tile>();
        int xPos = unitTile.GetXPos();
        int yPos = unitTile.GetYPos();


        if (yPos != 5)
        {
            summonTile = GameObject.Find($"Tile {xPos} {yPos + 1}").GetComponent<Tile>();
            if (summonTile.GetUnitInTile() == null)
            {
                GameObject UH19_1 = Instantiate(UH19_1_Prefab, summonTile.transform.position, Quaternion.identity);
                UH19_1.transform.parent = summonTile.transform;
                UnitCard UH19_1Card = UH19_1.GetComponent<UnitCard>();
                UH19_1Card.SetPlayerNo(playerNo);
                UH19_1Card.isPlayCard = true;
                UH19_1Card.isSkillDone = true;
            }
        }
        isSkillDone = true;
    }
    public override void UnitHighlight()
    {
        UnitSkill();
        isSkillDone = true;
        tileManager.NoHighlightUnit();
        Debug.Log("Highlight from unit 19");
    }
}
