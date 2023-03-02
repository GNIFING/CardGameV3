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

    //Highlight tile in white color when mouse is hover// 
    private void Start()
    {
        tileManager = GameObject.Find("Tiles").GetComponent<TileManager>();
        playerController = GameObject.Find("PlayerController").GetComponent<PlayerController>();

        highlight = transform.Find("Highlight").gameObject;
        nextMoveHighlight = transform.Find("NextMoveHightlight").gameObject;
        unitHighlight = transform.Find("MagicHighlight").gameObject;
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
        selectUnit = tileManager.GetSelectUnit();

        HandleUnitCard(unit, selectUnit);
             
    }

    private void HandleUnitCard(GameObject unit, GameObject selectUnit)
    {
        if (selectUnit != null)
        {
            selectUnitCard = selectUnit.GetComponent<UnitCard>();
        }

        if (IsHighlightFound(unitHighlight))
        {
            HandleUnitSkill();
        }

        if (IsHighlightFound(nextMoveHighlight) && selectUnitCard.GetCardCredit() >= 0)
        {
            HandleFoundHighlight(unitCard, selectUnit);
            return;
        }

        if (unit == null || !IsCurrentPlayerUnit(unitCard))
        {
            tileManager.DeSelectUnit();
            tileManager.CancelHighlightMove();
            return;
        }

        if (unit == selectUnit)
        {
            Debug.Log("Same Selected");
            return;
        }

        else if (unit != null && selectUnit == null)
        {
            tileManager.SetSelectUnit(unit);
            HighlightNextMoveUnit(unitCard, xPos, yPos);
        }

        else if (IsFriendlyUnitAlreadyMoved(selectUnit, unitCard))
        {
            tileManager.DeSelectUnit();
            tileManager.CancelHighlightMove();
            return;
        }

        else//friendly unit -> change select unit to this unit, cancel all nextmove highlight and highlight new next move
        {
            tileManager.CancelHighlightMove();
            tileManager.SetSelectUnit(unit);
            HighlightNextMoveUnit(unitCard, xPos, yPos);
        }
    }

    private void HandleUnitSkill()
    {

    }

    private bool IsFriendlyUnitAlreadyMoved(GameObject selectUnit, UnitCard unitCard)
    {
        return selectUnit != null && !IsCurrentPlayerUnit(selectUnit.GetComponent<UnitCard>());
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
                if (playerController.IsEnoughMana(unitCard)) tileManager.UnitMoveFromP1Hand();
                else Debug.Log("Not enough mana!");
                break;
            case TileType.Player2Tile:
                if (playerController.IsEnoughMana(unitCard)) tileManager.UnitMoveFromP2Hand();
                else Debug.Log("Not enough mana!");
                break;
            default:
                break;
        }
        
    }

    private bool IsHighlightFound(GameObject highlightObject)
    {
        return highlightObject.activeInHierarchy;
    }

    private void HandleFoundHighlight(UnitCard unitCard, GameObject selectUnit)
    {
        if (IsTowerTile() == 1) //tower of P1
        {
            Debug.Log("Hit Tower");
            if(selectUnitCard.GetPlayerNo() == 2)
            {
                playerController.SetPlayerHP(1, playerController.GetPlayerHP(1) - selectUnitCard.attack);
                tileManager.CancelHighlightMove();
                tileManager.DeSelectUnit();
                selectUnitCard.ReduceCardCredit();
            }
            else
            {
                tileManager.CancelHighlightMove();
                tileManager.DeSelectUnit();
            }
            //Attack Tower Player
        }

        else if(IsTowerTile() == 2)
        {
            Debug.Log("Hit Tower");
            if (selectUnitCard.GetPlayerNo() == 1)
            {
                playerController.SetPlayerHP(2, playerController.GetPlayerHP(2) - selectUnitCard.attack);
                tileManager.CancelHighlightMove();
                tileManager.DeSelectUnit();
                selectUnitCard.ReduceCardCredit();
            }
            else
            {
                tileManager.CancelHighlightMove();
                tileManager.DeSelectUnit();
            }
            //Attack Tower Player
        }

        else if(selectUnit != null && unit == null)
        {
            if (selectUnitCard.isPlayCard)
            {
                //Move select unit to this tile
                MoveUnitToThisTile(selectUnit);
                tileManager.CancelHighlightMove();
                tileManager.DeSelectUnit();
                selectUnitCard.ReduceCardCredit();
            }
            else
            {
                selectUnitCard.isPlayCard = true;
                playerController.SetPlayerMana(selectUnitCard.GetPlayerNo(), selectUnitCard.mana);
                MoveUnitToThisTile(selectUnit);
                tileManager.CancelHighlightMove();
                selectUnitCard.ReduceCardCredit();
                selectUnitCard.UnitHighlight();

                if(selectUnitCard.isSkillDone == true)
                {
                    tileManager.DeSelectUnit();
                    Debug.Log("deSelect Unit");
                }

            }
        }
        else if (!IsCurrentPlayerUnit(unitCard))
        {
            Debug.Log("Attack Enemy");
            AttackUnit(selectUnitCard, unitCard);
            tileManager.CancelHighlightMove();
            tileManager.DeSelectUnit();
            // Attack enemy unit here
        }

        else if(IsFriendlyUnitAlreadyMoved(selectUnit, unitCard))
        {
            // move Select unit to this tile
            tileManager.CancelHighlightMove();
            tileManager.DeSelectUnit();
        }

        else
        {
            Debug.Log("Last case");

            // Cancel all highlight and return Select unit to null
            tileManager.CancelHighlightMove();
            tileManager.SetSelectUnit(unit);
            HighlightNextMoveUnit(unitCard, xPos, yPos);
        }

    }

    //-------------------------------------//

    public int IsTowerTile()
    {
        if(tileType == TileType.Player1Tower)
        {
            return 1;
        }

        if (tileType == TileType.Player2Tower)
        {
            return 2;
        }
        return 0; 
    }

    public void AttackUnit(UnitCard selectUnitCard, UnitCard unitCard)
    {
        unitCard.health -= selectUnitCard.attack;
        unitCard.healthText.text = unitCard.health.ToString();

        selectUnitCard.health -= unitCard.attack;
        selectUnitCard.healthText.text = selectUnitCard.health.ToString();

        if (unitCard.health <= 0) Destroy(unitCard.gameObject);
        if (selectUnitCard.health <= 0) Destroy(selectUnitCard.gameObject);
    }


    public void MoveUnitToThisTile(GameObject selectUnit)
    {
        selectUnit.transform.SetParent(transform);
        selectUnit.transform.position = transform.position;
    }

    public void NextMoveHighlight(bool isSelect)
    {
        nextMoveHighlight.SetActive(isSelect);
    }

    public GameObject GetUnitInTile()
    {
        foreach (Transform obj in transform)
        {
            if (obj.CompareTag("Unit"))
            {
                Debug.Log("Found Unit name :" + obj.name);
                unitCard = obj.GetComponent<UnitCard>();
                return obj.gameObject;
            }
        }
        Debug.Log("Not Found Unit");
        unitCard = null;
        return null;
    }

    private bool IsCurrentPlayerUnit(UnitCard unitCard)
    {
        return unitCard.GetPlayerNo() == GameController.CurrentTurn;
    }

    //================ Magic ================//
    public void SetMagicHighlight(bool isActive)
    {
        unitHighlight.SetActive(isActive);
    }

}


