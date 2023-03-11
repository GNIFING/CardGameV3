using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    enum TileType
    {
        ArenaTile,
        Player1Tile,
        Player2Tile,
        Player1Tower,
        Player2Tower
    }

    [SerializeField] private GameObject highlight;
    [SerializeField] private GameObject nextMoveHighlight;
    [SerializeField] private GameObject unitHighlight;


    [SerializeField] private int xPos = 0;
    [SerializeField] private int yPos = 0;
    [SerializeField] TileType tileType;

    private GameObject unit;
    private UnitCard unitCard;
    
    private GameObject selectUnit;
    private UnitCard selectUnitCard;

    private GameObject selectMagic;
    private MagicCard selectMagicCard;

    private TileManager tileManager;
    private PlayerController playerController;

    private void Start()
    {
        tileManager = GameObject.Find("Tiles").GetComponent<TileManager>();
        playerController = GameObject.Find("PlayerController").GetComponent<PlayerController>();

        highlight = transform.Find("Highlight").gameObject;
        nextMoveHighlight = transform.Find("NextMoveHightlight").gameObject;
        unitHighlight = transform.Find("UnitHighlight").gameObject;
    }

    //-------------- Hover Highlight Mouse --------------//
    void OnMouseEnter()
    {
        highlight.SetActive(true);
    }
    void OnMouseExit()
    {
       highlight.SetActive(false);    
    }

    //-------------- Click On Tile --------------//

    void OnMouseDown()
    {
        unit = GetUnitInTile();
        selectUnit = tileManager.GetSelectUnit();

        HandleClickOnTile(unit, selectUnit);    
    }

    private void HandleClickOnTile(GameObject unit, GameObject selectUnit)
    {
        //-------------- Retrieve the UnitCard component --------------//

        if (selectUnit != null) selectUnitCard = selectUnit.GetComponent<UnitCard>();

        //-------------- Click on a tile that has the unitHighlight --------------//

        if (IsHighlightFound(unitHighlight))
        {
            HandleFoundUnitHighlight(unit, selectUnit);
            return;
        }

        if (tileManager.isInSkillProcess)
        {
            return;
        }

        //-------------- Click on a tile that has the nextMoveHighlight --------------//

        if (IsHighlightFound(nextMoveHighlight))
        {
            HandleFoundNextMoveHighlight(unitCard, selectUnit);
            return;
        }

        //-------------- Click on an empty tile or an enemy unit --------------//

        if (unit == null || !IsUnitFriendly(unitCard))
        {
            tileManager.DeSelectUnit();
            tileManager.CancelNextMoveHighlight();
            return;
        }

        //-------------- Click on the same selected unit --------------//

        if (unit == selectUnit)
        {
            return;
        }

        //-------------- Click on Unit to Set SelectUnit --------------//

        else if (unit != null && selectUnit == null)
        {
            Debug.Log("1");
            tileManager.SetSelectUnit(unit);
            HighlightNextMoveUnit(unitCard, xPos, yPos);
        }

        //-------------- Click on a unit that has already moved --------------//

        else if (IsFriendlyUnitAlreadyMoved(selectUnit, unitCard))
        {
            tileManager.DeSelectUnit();
            tileManager.CancelNextMoveHighlight();
            return;
        }

        //-------------- Click on a friendly unit to set it as the new SelectUnit --------------//

        else
        {
            tileManager.DeSelectUnit();
            tileManager.CancelNextMoveHighlight();
            tileManager.SetSelectUnit(unit);
            HighlightNextMoveUnit(unitCard, xPos, yPos);
        }
    }

    private void HandleFoundUnitHighlight(GameObject unit, GameObject selectUnit)
    {
        Debug.Log("3");
        selectUnitCard = selectUnit.GetComponent<UnitCard>();
        selectUnitCard.UnitSkill(unit, xPos, yPos);
        tileManager.CancelUnitMoveHighlight();
        if (selectUnitCard.isSkillDone == true)
        {
            tileManager.DeSelectUnit();
        }
    }

    private void HandleFoundNextMoveHighlight(UnitCard unitCard, GameObject selectUnit)
    {
        //-------------- Click on Player 1 Tower Tile --------------//

        if (tileType == TileType.Player1Tower)
        {
            if (selectUnitCard.GetPlayerNo() == 2)
            {
                playerController.SetPlayerHP(1, playerController.GetPlayerHP(1) - selectUnitCard.attack);
                tileManager.CancelNextMoveHighlight();
                tileManager.DeSelectUnit();
                selectUnitCard.ReduceCardCredit();
            }
            else
            {
                tileManager.CancelNextMoveHighlight();
                tileManager.DeSelectUnit();
            }
        }

        //-------------- Click on Player 2 Tower Tile --------------//

        else if (tileType == TileType.Player2Tower)
        {
            if (selectUnitCard.GetPlayerNo() == 1)
            {
                playerController.SetPlayerHP(2, playerController.GetPlayerHP(2) - selectUnitCard.attack);
                tileManager.CancelNextMoveHighlight();
                tileManager.DeSelectUnit();
                selectUnitCard.ReduceCardCredit();
            }
            else
            {
                tileManager.CancelNextMoveHighlight();
                tileManager.DeSelectUnit();
            }
        }

        //-------------- Click on an empty tile to move the selected unit to that tile --------------//

        else if (selectUnit != null && unit == null)
        {
            if (selectUnitCard.isPlayCard)
            {
                MoveUnitToThisTile(selectUnit);
                tileManager.CancelNextMoveHighlight();
                tileManager.DeSelectUnit();
                selectUnitCard.ReduceCardCredit();
            }

            //-------------- This unit has not been played yet --------------//

            else
            {
                Debug.Log("2");
                //unit play skill here
                selectUnitCard.isPlayCard = true;
                selectUnitCard.RemoveBackCard();
                playerController.SetPlayerMana(selectUnitCard.GetPlayerNo(), selectUnitCard.mana);
                MoveUnitToThisTile(selectUnit);

                tileManager.CancelNextMoveHighlight();
                selectUnitCard.ReduceCardCredit();
                tileManager.isInSkillProcess = true;
                selectUnitCard.UnitHighlight();

            }
        }

        //-------------- Click on enemy unit to attack --------------//

        else if (!IsUnitFriendly(unitCard))
        {
            Debug.Log("Attack Enemy");
            if(selectUnitCard.isPlayCard == false)
            {
                selectUnitCard.isPlayCard = true;
                selectUnitCard.RemoveBackCard();
                playerController.SetPlayerMana(selectUnitCard.GetPlayerNo(), selectUnitCard.mana);
                // Move unit to towe tile //
                MoveUnitToSpecificTile(selectUnit, selectUnitCard.GetPlayerNo(), xPos, yPos);
            }
            AttackUnit(selectUnitCard, unitCard);
            selectUnitCard.ReduceCardCredit();
            tileManager.CancelNextMoveHighlight();
            tileManager.DeSelectUnit();
            // Attack enemy unit here
        }

        //-------------- Click on the friendly unit that has already moved --------------//

        else if (IsFriendlyUnitAlreadyMoved(selectUnit, unitCard))
        {
            // move Select unit to this tile
            Debug.Log("Click on the friendly unit that has already moved");
            tileManager.CancelNextMoveHighlight();
            tileManager.DeSelectUnit();
        }

        //-------------- Click on the friendly unit that has not moved, and select it as selectUnit --------------//

        else
        {
            Debug.Log("Click on the friendly unit that has not moved, and select it as selectUnit");
            tileManager.CancelNextMoveHighlight();
            tileManager.SetSelectUnit(unit);
            HighlightNextMoveUnit(unitCard, xPos, yPos);
        }
    }

    private bool IsFriendlyUnitAlreadyMoved(GameObject selectUnit, UnitCard unitCard)
    {
        return unitCard.GetCardCredit() <= 0;
    }

    private void HighlightNextMoveUnit(UnitCard unitCard, int xPos, int yPos)
    {
        switch (tileType)
        {
            case TileType.Player1Tower:
                tileManager.HighlightByType(unitCard, xPos, yPos);
                break;
            case TileType.Player2Tower:
                tileManager.HighlightByType(unitCard, xPos, yPos);
                break;
            case TileType.ArenaTile:
                tileManager.HighlightByType(unitCard, xPos, yPos);
                break;
            case TileType.Player1Tile:
                if (playerController.IsEnoughMana(unitCard)) tileManager.HighlightUnitMoveFromP1Hand();
                break;
            case TileType.Player2Tile:
                if (playerController.IsEnoughMana(unitCard)) tileManager.HighlightUnitMoveFromP2Hand();
                break;
            default:
                break;
        }
        
    }

    private bool IsHighlightFound(GameObject highlightObject)
    {
        return highlightObject.activeInHierarchy;
    }

    private void AttackUnit(UnitCard selectUnitCard, UnitCard unitCard)
    {
        unitCard.health -= selectUnitCard.attack;
        unitCard.healthText.text = unitCard.health.ToString();

        selectUnitCard.health -= unitCard.attack;
        selectUnitCard.healthText.text = selectUnitCard.health.ToString();

        if (unitCard.health <= 0) Destroy(unitCard.gameObject);
        if (selectUnitCard.health <= 0) Destroy(selectUnitCard.gameObject);
    }


    private void MoveUnitToThisTile(GameObject selectUnit)
    {
        selectUnit.transform.SetParent(transform);
        selectUnit.transform.position = transform.position;
    }

    public void MoveUnitToSpecificTile(GameObject unit, int playerNo, int PosX, int PosY)
    {
        GameObject specificTile;
        if (playerNo == 1)
        {
            specificTile = GameObject.Find($"Tile {PosX - 1} {PosY}");
        }
        else
        {
            specificTile = GameObject.Find($"Tile {PosX + 1} {PosY}");
        }
        unit.transform.SetParent(specificTile.transform);
        unit.transform.position = specificTile.transform.position;
    }

    public void SetNextMoveHighlight(bool isSelect)
    {
        nextMoveHighlight.SetActive(isSelect);
    }

    public void SetUnitHighlight(bool isSelect)
    {
        unitHighlight.SetActive(isSelect);
    }

    public GameObject GetUnitInTile()
    {
        foreach (Transform obj in transform)
        {
            if (obj.CompareTag("Unit"))
            {
                unitCard = obj.GetComponent<UnitCard>();
                return obj.gameObject;
            }
        }
        unitCard = null;
        return null;
    }

    private bool IsUnitFriendly(UnitCard unitCard)
    {
        return unitCard.GetPlayerNo() == GameController.CurrentTurn;
    }   
}


