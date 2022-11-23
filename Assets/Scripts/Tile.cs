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
            Debug.Log("Found Unit");
            unit = unitTransform.gameObject;
            unitMove = unit.GetComponentInChildren<UnitMove>();
            unitMove.UnitMovePosition(xPos, yPos);

        }
        else if (_nextMoveHighlight.activeSelf)
        {
            unit = GameObject.Find("Unit");
            unit.transform.parent = transform;
            unit.transform.position = transform.position;
            unitMove = unit.GetComponentInChildren<UnitMove>();
            unitMove.MoveFinish();
        }
        else
        {
            //unit = GameObject.Find("Unit(Clone)");
            //unitMove = unit.GetComponentInChildren<UnitMove>();
            //unitMove.MoveFinish();
            Debug.Log("Not Found Unit");
        }
    }

    public void NextMoveHighlight(bool isActive)
    {
        _nextMoveHighlight.SetActive(isActive);
    }
}
