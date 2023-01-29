using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TileManager : MonoBehaviour
{
    private List<GameObject> Tiles { get; set; } = new List<GameObject>();
    private List<Tile> NextMoveHighlightTiles = new List<Tile>();
    public GameObject ActiveUnit { get; private set; }

    private void Start()
    {
        AddTilesToList();
    }

    private void AddTilesToList()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Tiles.Add(transform.GetChild(i).gameObject);
        }
    }

    public List<GameObject> GetAllTiles() => Tiles;

    public void SetActiveUnit(GameObject unit)
    {
        ActiveUnit = unit;
    }

    public GameObject GetActiveUnit() => ActiveUnit;


    public void HighlightByType(UnitCard unitCard, int x, int y)
    {
        if (PlayerTurnController.CurrentTurn == unitCard.GetPlayerNo() && unitCard.GetCardCredit() != 0)
        {
            Debug.Log("switch case");
            SetActiveUnit(unitCard.gameObject);
            switch (unitCard.GetUnitMoveType())
            {
                case UnitCardStat.MoveType.StraightShort:
                    HighlightStraightShort(x, y);
                    break;
                case UnitCardStat.MoveType.StraightFar:
                    HighlightStraightFar(x, y);
                    break;
                case UnitCardStat.MoveType.DiagonalShort:
                    HighlightDiagonalShort(x, y);
                    break;
                case UnitCardStat.MoveType.DiagonalFar:
                    HighlightDiagonalFar(x, y);
                    break;
                case UnitCardStat.MoveType.Round:
                    HighlightRound(x, y);
                    break;
                case UnitCardStat.MoveType.HorizontalShort:
                    HighlightHorizontalShort(x, y);
                    break;
                case UnitCardStat.MoveType.HorizontalFar:
                    HighlightHorizontalFar(x, y);
                    break;
                default:
                    Debug.Log("Not Select Move Type");
                    break;
            }
        }
            
    }

    public void UnitMoveFromP1Hand()
    {
        GenerateHighlightMove(1, 0);
        GenerateHighlightMove(1, 1);
        GenerateHighlightMove(1, 2);
        GenerateHighlightMove(1, 3);
        GenerateHighlightMove(1, 4);
        GenerateHighlightMove(1, 5);
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


    private void HighlightRound(int x, int y)
    {
        Debug.Log("Rounf!");
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

    private void HighlightDiagonalFar(int x, int y)
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
        if (x > 1 && y < 4) GenerateHighlightMove(x - 2, y + 2); // index 21
        if (x < 5 && y < 4) GenerateHighlightMove(x + 2, y + 2); // index 25
    }

    private void HighlightDiagonalShort(int x, int y)
    {
        //index same as MoveDiagonalFar
        //Can move only 7 9 17 19
        if (x != 0 && y != 0) GenerateHighlightMove(x - 1, y - 1); // index 7
        if (x != 6 && y != 0) GenerateHighlightMove(x + 1, y - 1); // index 9
        if (x != 0 && y != 5) GenerateHighlightMove(x - 1, y + 1); // index 17
        if (x != 6 && y != 5) GenerateHighlightMove(x + 1, y + 1); // index 19
    }

    private void HighlightStraightFar(int x, int y)
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

    private void HighlightStraightShort(int x, int y)
    {
        //index same as MoveStraightFar
        //Can move onleey 8 12 14 18
        if (y != 0) GenerateHighlightMove(x, y - 1); // index 8
        if (x != 0) GenerateHighlightMove(x - 1, y); // index 12
        if (x != 6) GenerateHighlightMove(x + 1, y); // index 14
        if (y != 5) GenerateHighlightMove(x, y + 1); // index 18
    }

    private void HighlightHorizontalFar(int x, int y)
    {
        if (x > 1) GenerateHighlightMove(x - 2, y); // index 11
        if (x != 0) GenerateHighlightMove(x - 1, y); // index 12
        if (x != 6) GenerateHighlightMove(x + 1, y); // index 14
        if (x < 5) GenerateHighlightMove(x + 2, y); // index 15
    }

    private void HighlightHorizontalShort(int x, int y)
    {
        if (x != 0) GenerateHighlightMove(x - 1, y); // index 12
        if (x != 6) GenerateHighlightMove(x + 1, y); // index 14
    }

    private void GenerateHighlightMove(int x, int y)
    {
        Debug.Log("Ok it is");
        GameObject nextMoveHighlightObj = GameObject.Find($"Tile {x} {y}");
        Debug.Log(nextMoveHighlightObj.name);
        if (nextMoveHighlightObj != null)
        {
            Debug.Log("Ok it is true");

            Tile tile = nextMoveHighlightObj.GetComponent<Tile>();
            tile.NextMoveHighlight(true);
            NextMoveHighlightTiles.Add(tile);
        }
    }


}