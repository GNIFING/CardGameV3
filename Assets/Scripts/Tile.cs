using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    enum TileType
    {
        ArenaTile,
        Player1Tile,
        Player2Tile
    }

    [SerializeField] private GameObject _highlight;
    [SerializeField] private GameObject _nextMoveHighlight;
    [SerializeField] private int xPos = 0;
    [SerializeField] private int yPos = 0;
    [SerializeField] TileType tileType;

    private TileManager tileManager;
    private GameObject activeUnit;
    private GameObject unit;
    private Transform unitTransform;
    private UnitMove unitMove;

    void OnMouseEnter()
    {
        _highlight.SetActive(true);
    }
    void OnMouseExit()
    {
        _highlight.SetActive(false);    
    }

    void OnMouseDown()
    {
        Debug.Log("MouseClick on Pos :" + xPos + "," + yPos);
        unitTransform = transform.Find("Unit");

        if(unitTransform != null)
        {
            unit = unitTransform.gameObject;
            unitMove = unit.GetComponentInChildren<UnitMove>();
            //Debug.Log("First -->"+ unit);
            unitMove.SetHighlightUnit(unit);

            Debug.Log("Found Unit");
            tileManager = GameObject.Find("Tiles").GetComponent<TileManager>();
            activeUnit = tileManager.GetActiveUnit();
            activeUnit = unitTransform.gameObject;
            unitMove = activeUnit.GetComponentInChildren<UnitMove>();
            unitMove.UnitMovePosition(xPos, yPos);

        }
        else if (_nextMoveHighlight.activeSelf)
        {
            tileManager = GameObject.Find("Tiles").GetComponent<TileManager>();
            activeUnit = tileManager.GetActiveUnit();
            activeUnit.transform.parent = transform;
            activeUnit.transform.position = transform.position;
            unitMove = activeUnit.GetComponentInChildren<UnitMove>();
            unitMove.MoveFinish();
        }
        else
        {
            tileManager = GameObject.Find("Tiles").GetComponent<TileManager>();
            activeUnit = tileManager.GetActiveUnit();
            
            unitMove = activeUnit.GetComponentInChildren<UnitMove>();
            unitMove.MoveCancel();
            Debug.Log("Not Found Unit");
        }
    }

    public void NextMoveHighlight(bool isActive)
    {
        _nextMoveHighlight.SetActive(isActive);
    }
}
