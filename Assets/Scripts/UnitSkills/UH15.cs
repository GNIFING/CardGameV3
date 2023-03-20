using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UH15 : UnitCard
{
    void Start()
    {
        InitializeCardStats();
        UpdateCardUI();
    }
    public override void UnitSkill()
    {
        List<Tile> tiles = tileManager.GetRoundTilesOfThisTile(transform.parent.gameObject.GetComponent<Tile>());
        foreach (Tile tile in tiles)
        {
            if(tile.GetUnitInTile() != null)
            {
                UnitCard buffUnit = tile.GetUnitInTile().GetComponent<UnitCard>();
                buffUnit.attack += 1;
                buffUnit.attackText.text = buffUnit.attack.ToString();
            }
        }
        Debug.Log("Unit 15 Skill !");
        isSkillDone = true;
    }
    public override void UnitHighlight()
    {
        UnitSkill();
        isSkillDone = true;
        tileManager.NoHighlightUnit();
        Debug.Log("Highlight from unit 15");
    }
}
