using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMove : MonoBehaviour
{
    private List<Tile> tiles = new List<Tile>();
    [SerializeField] private TileManager tileManager;
    private UnitCard unitCard;

    private void Start()
    {
        unitCard = gameObject.transform.parent.gameObject.GetComponent<UnitCard>();
        tileManager = GameObject.Find("Tiles").GetComponent<TileManager>();
    }


    enum MoveType
    {
        straightShort,
        straightFar,
        diagonalShort,
        diagonalFar,
        round,
        horizontalShort,
        horizontalFar
    }

    [SerializeField] private MoveType moveType; 
    public void UnitMovePosition(int x, int y)
    {
        if (PlayerTurnController.GetPlayerTurn() == unitCard.GetPlayerNo() && unitCard.GetCardCredit() != 0)
        {
            MoveByType(x, y);
        }
    }

    private void MoveByType(int x, int y)
    {
        switch (moveType)
        {
            case MoveType.straightShort:
                MoveStraightShort(x, y);
                break;
            case MoveType.straightFar:
                MoveStraightFar(x, y);
                break;
            case MoveType.diagonalShort:
                MoveDiagonalShort(x, y);
                break;
            case MoveType.diagonalFar:
                MoveDiagonalFar(x, y);
                break;
            case MoveType.round:
                MoveRound(x, y);
                break;
            case MoveType.horizontalShort:
                MoveHorizontalShort(x, y);
                break;
            case MoveType.horizontalFar:
                MoveHorizontalFar(x, y);
                break;
            default:
                Debug.Log("Not Select Move Type");
                break;
        }
    }

    public void UnitMoveFromP1Hand()
    {
        GenerateHighlightMove(1,0);
        GenerateHighlightMove(1,1);
        GenerateHighlightMove(1,2);
        GenerateHighlightMove(1,3);
        GenerateHighlightMove(1,4);
        GenerateHighlightMove(1,5);
    }

    public void UnitMoveFromP2Hand()
    {
        GenerateHighlightMove(5, 0);
        GenerateHighlightMove(5, 1);
        GenerateHighlightMove(5, 2);
        GenerateHighlightMove(5, 3);
        GenerateHighlightMove(5, 4);
        GenerateHighlightMove(5, 5);
    }


    private void MoveRound(int x, int y)
    {
        // give indexMove
        // 7   8   9
        // 4 unit  6
        // 1   2   3

        if (x != 0 && y != 0) GenerateHighlightMove(x - 1, y - 1); // index 1
        if (y != 0) GenerateHighlightMove(x, y - 1); // index 2
        if (x != 6 && y != 0) GenerateHighlightMove(x + 1, y - 1); // index 3
        if (x != 0) GenerateHighlightMove(x - 1, y); // index 4
        if (x != 6) GenerateHighlightMove(x + 1, y); // index 6
        if (x != 0 && y != 5) GenerateHighlightMove(x - 1, y + 1); // index 7
        if (y != 5) GenerateHighlightMove(x, y + 1); // index 8
        if (x != 6 && y != 5) GenerateHighlightMove(x + 1, y + 1); // index 9

    }

    private void MoveDiagonalFar(int x, int y)
    {
        //give indexMove
        //  21 22 23 24 25
        //  16 17 18 19 20
        //  11 12unit14 15
        //  6  7  8  9  10
        //  1  2  3  4  5  

        // Can move only 1 5 7 9 17 19 21 25

        if (x > 1 && y > 1) GenerateHighlightMove(x - 2, y - 2); // index 1
        if (x < 5 && y > 1) GenerateHighlightMove(x + 2, y - 2); // index 5
        if (x != 0 && y != 0) GenerateHighlightMove(x - 1, y - 1); // index 7
        if (x != 6 && y != 0) GenerateHighlightMove(x + 1, y - 1); // index 9
        if (x != 0 && y != 5) GenerateHighlightMove(x - 1, y + 1); // index 17
        if (x != 6 && y != 5) GenerateHighlightMove(x + 1, y + 1); // index 19
        if (x > 1 && y < 4) GenerateHighlightMove(x -2, y + 2); // index 21
        if (x < 5 && y < 4) GenerateHighlightMove(x + 2, y + 2); // index 25
    }

    private void MoveDiagonalShort(int x, int y)
    {
        //index same as MoveDiagonalFar
        //Can move only 7 9 17 19
        if (x != 0 && y != 0) GenerateHighlightMove(x - 1, y - 1); // index 7
        if (x != 6 && y != 0) GenerateHighlightMove(x + 1, y - 1); // index 9
        if (x != 0 && y != 5) GenerateHighlightMove(x - 1, y + 1); // index 17
        if (x != 6 && y != 5) GenerateHighlightMove(x + 1, y + 1); // index 19
    }

    private void MoveStraightFar(int x, int y)
    {
        //give indexMove
        //  21 22 23 24 25
        //  16 17 18 19 20
        //  11 12unit14 15
        //  6  7  8  9  10
        //  1  2  3  4  5  

        //Can move only 3 8 11 12 14 15 18 23
        if (y > 1) GenerateHighlightMove(x, y - 2); // index 3
        if (y != 0) GenerateHighlightMove(x, y - 1); // index 8
        if (x > 1) GenerateHighlightMove(x - 2, y); // index 11
        if (x != 0) GenerateHighlightMove(x - 1, y); // index 12
        if (x != 6) GenerateHighlightMove(x + 1, y); // index 14
        if (x < 5) GenerateHighlightMove(x + 2, y); // index 15
        if (y != 5) GenerateHighlightMove(x, y + 1); // index 18
        if (y < 4) GenerateHighlightMove(x, y + 2); // index 23

    }

    private void MoveStraightShort(int x, int y)
    {
        //index same as MoveStraightFar
        //Can move onleey 8 12 14 18
        if (y != 0) GenerateHighlightMove(x, y - 1); // index 8
        if (x != 0) GenerateHighlightMove(x - 1, y); // index 12
        if (x != 6) GenerateHighlightMove(x + 1, y); // index 14
        if (y != 5) GenerateHighlightMove(x, y + 1); // index 18
    }

    private void MoveHorizontalFar(int x, int y)
    {
        if (x > 1) GenerateHighlightMove(x - 2, y); // index 11
        if (x != 0) GenerateHighlightMove(x - 1, y); // index 12
        if (x != 6) GenerateHighlightMove(x + 1, y); // index 14
        if (x < 5) GenerateHighlightMove(x + 2, y); // index 15
    }

    private void MoveHorizontalShort(int x, int y)
    {
        if (x != 0) GenerateHighlightMove(x - 1, y); // index 12
        if (x != 6) GenerateHighlightMove(x + 1, y); // index 14
    }

    private void GenerateHighlightMove(int x, int y)
    {
        GameObject highlightObj = GameObject.Find($"Tile {x} {y}");
        if (highlightObj != null)
        {
            Tile tile = highlightObj.GetComponent<Tile>();
            tile.NextMoveHighlight(true);
            tiles.Add(tile);
        }
        SetHighlightUnit(this.gameObject.transform.parent.gameObject);
    }

    public void MoveFinish()
    {
        Debug.Log("Move Finish");
        foreach (var tile in tiles)
        {
            tile.NextMoveHighlight(false);
        }
        tiles = new List<Tile>();
        unitCard.ReduceCardCredit();
        //if(PlayerTurnController.GetPlayerTurn() == 1)
        //{
        //    PlayerTurnController.SetPlayerTurn(2);
        //}
        //else
        //{
        //    PlayerTurnController.SetPlayerTurn(1);
        //}
    }

    public void MoveCancel()
    {
        foreach (var tile in tiles)
        {
            tile.NextMoveHighlight(false);
        }
        tiles = new List<Tile>();
    }

    public void SetHighlightUnit(GameObject unit)
    {
        tileManager.GetComponent<TileManager>().SetActiveUnit(unit);
    }

}
