using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UO7 : UnitCard
{
    void Start()
    {
        InitializeCardStats();
        UpdateCardUI();
    }
    public override void UnitSkill()
    {
        Tile unitTile = GetComponentInParent<Tile>();

        int xPos = unitTile.GetXPos();
        int yPos = unitTile.GetYPos();
        int offset = playerNo == 1 ? 1 : -1;
        int offset2 = playerNo == 1 ? 0 : 6 - 2 * xPos;
        Tile hookedTile = new Tile();
        UnitCard selectedUnit = new UnitCard();


        for (int i = 1; i < 6 - xPos - offset2; i++) //0 1 2 3 4
        {
            Tile tile = GameObject.Find($"Tile {xPos + i * offset} {yPos}").GetComponent<Tile>();
            if(tile.GetUnitInTile() != null)
            {
                selectedUnit = tile.GetUnitInTile().GetComponent<UnitCard>();
            }
            if (tile.GetUnitInTile() == null)
            {
                hookedTile = tile;
                break;
            }
            else if (selectedUnit.GetPlayerNo() != playerNo)
            {
                hookedTile = tile;
                break;
            }
            else
            {
                hookedTile = tile;
            }
        }
        for (int i = 1; i < 6 - xPos - offset2; i++) //0 1 2 3 4
        {
            Tile tile = GameObject.Find($"Tile {xPos + i * offset} {yPos}").GetComponent<Tile>();
            if (tile.GetUnitInTile() != null)
            {
                selectedUnit = tile.GetUnitInTile().GetComponent<UnitCard>();
                if (selectedUnit.GetPlayerNo() != playerNo)
                {
                    Debug.Log("Hooked");

                    StartCoroutine(WaitTime(0.5f));
                    hookedTile.MoveUnitToThisTile(selectedUnit.gameObject);
                    StartCoroutine(WaitTime(1f));

                    int arenaId = gameController.arenaId;
                    multiPlayerController = FindObjectOfType<MultiPlayerController>();
                    Tile tileOnThisUnit = GetComponentInParent<Tile>();
                    int tileIndex = tileOnThisUnit.ConvertTilePosToIndex(tileOnThisUnit.GetXPos(), tileOnThisUnit.GetYPos());

                    StartCoroutine(multiPlayerController.UpdateCard(arenaId, tileIndex, 0, 0, (response) => { Debug.Log("Update Card Done"); }));
                    //selectedUnit.TakeDamage(this, 1);
                    break;
                }
            }
        }
        Debug.Log("Ogre 7 Skill !");
        isSkillDone = true;
    }

    public IEnumerator WaitTime(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
    }
    public override void UnitHighlight()
    {
        UnitSkill();
        isSkillDone = true;
        tileManager.NoHighlightUnit();
        Debug.Log("Highlight from Ogre 7");
    }
}
