using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public GameObject SelectUnit;
    private List<GameObject> Tiles { get; set; } = new List<GameObject>();
    private List<Tile> NextMoveHighlightTiles = new List<Tile>();
    private List<Tile> unitHighlightTiles = new List<Tile>();

    public bool isInSkillProcess;
    public bool hasUnitHighlight;

    private int player1Offset;
    private int player2Offset;


    private void Start()
    {
        SelectUnit = null;
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

    // Get Unit Highlight in many type  ->  all unit, friendly unit only, enemy unit only, tower only // 
    public List<Tile> HighlightAllUnitTiles()
    {
        foreach(GameObject tileObject in Tiles)
        {
            Tile tile = tileObject.GetComponent<Tile>();
            if(tile.GetUnitInTile() != null)
            {

                unitHighlightTiles.Add(tile);
                GenerateUnitHighlight(tile.gameObject);
            }
        }
        if(unitHighlightTiles.Count >= 1)
        {
            hasUnitHighlight = true;
        }
        else
        {
            Debug.Log("No unit to highlight --> Skill done");
            isInSkillProcess = false;
        }
        Debug.Log(unitHighlightTiles.Count);
        return unitHighlightTiles;
    }
    public List<Tile> HighlightFriendlyUnitTiles(int playerNo)
    {
        foreach (GameObject tileObject in Tiles)
        {
            Tile tile = tileObject.GetComponent<Tile>();
            if(tile.GetUnitInTile() != null && tile.GetUnitInTile().GetComponent<UnitCard>().GetPlayerNo() == playerNo)
            {
                tile.SetUnitHighlight(true);
                unitHighlightTiles.Add(tile);
            }
        }
        if (unitHighlightTiles.Count >= 1)
        {
            hasUnitHighlight = true;
        }
        else
        {
            Debug.Log("No unit to highlight --> Skill done");
            isInSkillProcess = false;
        }
        Debug.Log(unitHighlightTiles.Count);
        return unitHighlightTiles;
    }

    public List<Tile> HighlightEnemyUnitTiles(int playerNo)
    {
        foreach (GameObject tileObject in Tiles)
        {
            Tile tile = tileObject.GetComponent<Tile>();
            if (tile.GetUnitInTile() != null && tile.GetUnitInTile().GetComponent<UnitCard>().GetPlayerNo() != playerNo)
            {
                tile.SetUnitHighlight(true);
                unitHighlightTiles.Add(tile);
            }
        }
        if (unitHighlightTiles.Count >= 1)
        {
            hasUnitHighlight = true;
        }
        else
        {
            Debug.Log("No unit to highlight --> Skill done");
            isInSkillProcess = false;
        }
        return unitHighlightTiles;
    }

    public void NoHighlightUnit()
    {
        isInSkillProcess = false;
    }
    public void SetSelectUnit(GameObject unit)
    {
        SelectUnit = unit;
    }
    
    public void DeSelectUnit()
    {
        SelectUnit = null;
        CancelNextMoveHighlight();
    }

    public GameObject GetSelectUnit()
    {
        if (SelectUnit == null) return null;
        return SelectUnit;
    }

    public void HighlightByType(UnitCard unitCard, int x, int y)
    {
        if (GameController.CurrentTurn == unitCard.GetPlayerNo() && unitCard.GetCardCredit() != 0)
        {
            Debug.Log("333");
            player1Offset = SelectUnit.GetComponent<UnitCard>().GetPlayerNo() == 1 ? 1 : 0;
            player2Offset = SelectUnit.GetComponent<UnitCard>().GetPlayerNo() == 2 ? 1 : 0;
            SetSelectUnit(unitCard.gameObject);
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

    public void HighlightUnitMoveFromP1Hand()
    {
        Debug.Log("p1");
        for (int yPos = 0; yPos < 6; yPos++)
        {
            GenerateHighlightMove(1, yPos);
        }
    }

    public void HighlightUnitMoveFromP2Hand()
    {
        for (int yPos = 0; yPos < 6; yPos++)
        {
            GenerateHighlightMove(5, yPos);
        }
    }


    private void HighlightRound(int x, int y)
    {
        Debug.Log("Round!");
        // give indexMove
        // 7   8   9
        // 4 unit  6
        // 1   2   3
        
        int[] xOffset = { -1, 0, 1, -1, 1, -1, 0, 1 };
        int[] yOffset = { -1, -1, -1, 0, 0, 1, 1, 1 };

        for (int i = 0; i < 8; i++)
        {
            int newX = x + xOffset[i];
            int newY = y + yOffset[i];
            if (newX >= 0 + player1Offset && newX <= 6 - player2Offset && newY >= 0 && newY <= 5)
            {
                GenerateHighlightMove(newX, newY);
            }
        }

        //if (x != 0 && y != 0) GenerateHighlightMove(x - 1, y - 1); // index 1
        //if (y != 0) GenerateHighlightMove(x, y - 1); // index 2
        //if (x != 6 && y != 0) GenerateHighlightMove(x + 1, y - 1); // index 3
        //if (x != 0) GenerateHighlightMove(x - 1, y); // index 4
        //if (x != 6) GenerateHighlightMove(x + 1, y); // index 6
        //if (x != 0 && y != 5) GenerateHighlightMove(x - 1, y + 1); // index 7
        //if (y != 5) GenerateHighlightMove(x, y + 1); // index 8
        //if (x != 6 && y != 5) GenerateHighlightMove(x + 1, y + 1); // index 9

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

        int[] xOffset = { -2, 2, -1, 1, -1, 1, -2, 2 };
        int[] yOffset = { -2, -2, -1, -1, 1, 1, 2, 2 };

        for (int i = 0; i < 8; i++)
        {
            int newX = x + xOffset[i];
            int newY = y + yOffset[i];
            if (newX >= 0 + player1Offset && newX <= 6 - player2Offset && newY >= 0 && newY <= 5)
            {
                GenerateHighlightMove(newX, newY);
            }
        }
    }

    private void HighlightDiagonalShort(int x, int y)
    {
        //index same as MoveDiagonalFar
        //Can move only 7 9 17 19
        int[] xOffset = { -1, 1, -1, 1 };
        int[] yOffset = { -1, -1, 1, 1 };

        for (int i = 0; i < 4; i++)
        {
            int newX = x + xOffset[i];
            int newY = y + yOffset[i];
            if (newX >= 0 + player1Offset && newX <= 6 - player2Offset && newY >= 0 && newY <= 5)
            {
                GenerateHighlightMove(newX, newY);
            }
        }
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
        int[] xOffset = { -2, 0, 2, -1, 1, -1, 0, 1 };
        int[] yOffset = { 0, -1, 0, -2, -2, 2, 1, 2 };

        for (int i = 0; i < 8; i++)
        {
            int newX = x + xOffset[i];
            int newY = y + yOffset[i];
            if (newX >= 0 + player1Offset && newX <= 6 - player2Offset && newY >= 0 && newY <= 5)
            {
                GenerateHighlightMove(newX, newY);
            }
        }

    }

    private void HighlightStraightShort(int x, int y)
    {
        //index same as MoveStraightFar
        //Can move onleey 8 12 14 18
        int[] xOffset = { -1, 0, 1, 0};
        int[] yOffset = { 0, -1, 0, 1};

        for (int i = 0; i < 4; i++)
        {
            int newX = x + xOffset[i];
            int newY = y + yOffset[i];
            if (newX >= 0 + player1Offset && newX <= 6 - player2Offset && newY >= 0 && newY <= 5)
            {
                GenerateHighlightMove(newX, newY);
            }
        }
        if(IsUnitRange())
        {
            if(SelectUnit.GetComponent<UnitCard>().GetPlayerNo() == 1 && x <= 4 && HasUnitOrTowerInTile(x + 2, y))
            {
                GenerateHighlightMove(x + 2, y);
            }
            if(SelectUnit.GetComponent<UnitCard>().GetPlayerNo() == 2 && x >= 2 && HasUnitOrTowerInTile(x - 2, y))
            {
                GenerateHighlightMove(x - 2, y);
            }
        }
    }

    private void HighlightHorizontalFar(int x, int y)
    {
        int[] xOffset = { -2, -1, 1, 2};
        int[] yOffset = { 0, 0, 0, 0 };

        for (int i = 0; i < 4; i++)
        {
            int newX = x + xOffset[i];
            int newY = y + yOffset[i];
            if (newX >= 0 + player1Offset && newX <= 6 - player2Offset && newY >= 0 && newY <= 5)
            {
                GenerateHighlightMove(newX, newY);
            }
        }
        //if (x > 1) GenerateHighlightMove(x - 2, y); // index 11
        //if (x != 0) GenerateHighlightMove(x - 1, y); // index 12
        //if (x != 6) GenerateHighlightMove(x + 1, y); // index 14
        //if (x < 5) GenerateHighlightMove(x + 2, y); // index 15
    }

    private void HighlightHorizontalShort(int x, int y)
    {
        int[] xOffset = { -1, 1 };
        int[] yOffset = { 0, 0 };

        for (int i = 0; i < 2; i++)
        {
            int newX = x + xOffset[i];
            int newY = y + yOffset[i];
            if (newX >= 0 + player1Offset && newX <= 6 - player2Offset && newY >= 0 && newY <= 5)
            {
                GenerateHighlightMove(newX, newY);
            }
        }
        //if (x != 0) GenerateHighlightMove(x - 1, y); // index 12
        //if (x != 6) GenerateHighlightMove(x + 1, y); // index 14
    }

    private void GenerateHighlightMove(int x, int y)
    {
        GameObject nextMoveHighlightObj = GameObject.Find($"Tile {x} {y}");
        if (nextMoveHighlightObj != null)
        {
            Tile tile = nextMoveHighlightObj.GetComponent<Tile>();
            tile.SetNextMoveHighlight(true);
            NextMoveHighlightTiles.Add(tile);
        }
    }

    private void GenerateUnitHighlight(GameObject tile)
    {
        tile.GetComponent<Tile>().SetUnitHighlight(true);
    }
    public void CancelNextMoveHighlight()
    {
        foreach (Tile tile in NextMoveHighlightTiles)
        {
            tile.SetNextMoveHighlight(false);
        }
        NextMoveHighlightTiles = new List<Tile>();
    }

    public void CancelUnitMoveHighlight()
    {
        foreach (Tile tile in unitHighlightTiles)
        {
            tile.SetUnitHighlight(false);
        }
        isInSkillProcess = false;
        unitHighlightTiles = new List<Tile>();
        hasUnitHighlight = false;
    }

    private bool IsUnitRange()
    {
        return SelectUnit.GetComponent<UnitCard>().GetUnitAttackType() == UnitCardStat.AttackType.Range;
    }

    private bool HasUnitOrTowerInTile(int x, int y)
    {
        GameObject SelectTile = GameObject.Find($"Tile {x} {y}");
        foreach (Transform obj in SelectTile.transform)
        {
            if (obj.CompareTag("Unit"))
            {
                return true;
            }
        }
        if(SelectTile.GetComponent<Tile>().tileType == Tile.TileType.Player1Tower || SelectTile.GetComponent<Tile>().tileType == Tile.TileType.Player2Tower)
        {
            Debug.Log("here");
            return true;
        }
        return false;
    }
}