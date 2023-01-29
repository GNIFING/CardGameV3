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
        if (unit == null) return; //not found unit

        unitCard = unit.GetComponent<UnitCard>();
        if (!IsCurrentPlayerUnit(unitCard)) return; //have unit but not your turn

        activeUnit = GetActiveUnit();
        if (activeUnit != null)
        {
            if (!IsCurrentPlayerUnit(activeUnit.GetComponent<UnitCard>())) return;
            HighlightByType(unitCard ,xPos, yPos);
        }
        else
        {
            //active unit = null
            HighlightByType(unitCard ,xPos, yPos);
            Debug.Log("highlighbytype");
            
            //unitTransform.gameObject.GetComponent<UnitMove>().MoveToArena();
        }
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

    private GameObject GetActiveUnit()
    {
        tileManager = GameObject.Find("Tiles").GetComponent<TileManager>();
        return tileManager.GetActiveUnit();
    }
}


