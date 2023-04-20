using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UO12 : UnitCard
{
    private int damageSkill = 1;
    void Start()
    {
        InitializeCardStats();
        UpdateCardUI();
    }

    public override void UnitSkill()
    {
        Tile unitTile = GetComponentInParent<Tile>();
        for (int i = 0; i < 7; i++)
        {
            Tile tile = GameObject.Find($"Tile {i} {unitTile.GetYPos()}").GetComponent<Tile>();
            if (tile.GetUnitInTile() != null && tile.GetUnitInTile().GetComponent<UnitCard>().GetPlayerNo() != playerNo)
            {
                UnitCard unitCard = tile.GetUnitInTile().GetComponent<UnitCard>();
                unitCard.TakeDamage(this, damageSkill);
            }
        }
        Debug.Log("Ogre 12 Skill !");
        isSkillDone = true;
    }
    public override void UnitHighlight()
    {
        UnitSkill();
        isSkillDone = true;
        tileManager.NoHighlightUnit();
        Debug.Log("Highlight from Ogre 12");
    }
}
