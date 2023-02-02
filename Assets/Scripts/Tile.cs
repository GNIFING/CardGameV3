using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : TileManager
{
    enum TileType
    {
        ArenaTile,
        Player1Tile,
        Player2Tile
    }

    [SerializeField] private GameObject highlight;
    [SerializeField] private GameObject nextMoveHighlight;
    [SerializeField] private int xPos = 0;
    [SerializeField] private int yPos = 0;
    [SerializeField] TileType tileType;

    private GameObject unit;
    private Transform unitTransform;
    private UnitCard unitCard;
    
    private TileManager tileManager;
    private GameObject activeUnit;

    //Highlight tile in white color when mouse is hover// 
    private void Start()
    {
        highlight = transform.Find("Highlight").gameObject;
        nextMoveHighlight = transform.Find("NextMoveHightlight").gameObject;
    }

    void OnMouseEnter()
    {
        highlight.SetActive(true);
    }
    void OnMouseExit()
    {
       highlight.SetActive(false);    
    }
    //-------------------------------------------------//
    void OnMouseDown()
    {
        unit = GetUnitInTile();
        activeUnit = GetActiveUnit();

        // Not Found Unit //
        if (unit == null) return;

        // Found Unit But Enemy Unit //
        unitCard = unit.GetComponent<UnitCard>();
        if (!IsCurrentPlayerUnit(unitCard)) return;

        // Found Friendly Unit (already move) //

        // Found Friendly unit (not move) //
        if (activeUnit != null)
        {
            if (!IsCurrentPlayerUnit(activeUnit.GetComponent<UnitCard>())) return;
            HighlightByType(unitCard, xPos, yPos);
        }
        else
        {
            //active unit = null
            HighlightByType(unitCard, xPos, yPos);
            Debug.Log("highlighbytype");
        }
        // Found Highlight //
        if (nextMoveHighlight.activeInHierarchy)
        {
            //move active unit to this tile
        }
      
        // Found Highlight and Enemy Unit //
        if( nextMoveHighlight.activeInHierarchy && !IsCurrentPlayerUnit(unitCard))
        {
            //Attack enemy unit here
        }
        // Found Highlight and Friendly unit (already move) //
        if(nextMoveHighlight.activeInHierarchy && IsCurrentPlayerUnit(unitCard))
        {
            //Cancel all highlight and return active unit to null
        }

        // Found Highlight and Friendly unit (not move) //

        // Found Highlight and Tower //







    }

    public void NextMoveHighlight(bool isActive)
    {
        nextMoveHighlight.SetActive(isActive);
    }

    private GameObject GetUnitInTile()
    {
        foreach (Transform obj in transform)
        {
            if (obj.CompareTag("Unit"))
            {
                Debug.Log("Found Unit");
                return obj.gameObject;
            }
        }
        Debug.Log("Not Found Unit");
        return null;
    }

    private bool IsCurrentPlayerUnit(UnitCard unitCard)
    {
        return unitCard.GetPlayerNo() == PlayerTurnController.CurrentTurn;
    }
}


