using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public enum TileType
    {
        ArenaTile,
        Player1Tile,
        Player2Tile,
        Player1Tower,
        Player2Tower
    }

    [SerializeField] private GameObject hoverHighlight;
    [SerializeField] private GameObject nextMoveHighlight;
    [SerializeField] private GameObject unitHighlight;
    [SerializeField] private int xPos = 0;
    [SerializeField] private int yPos = 0;
    public GameObject buff;
    [SerializeField] private int buffIndex;


    private GameObject unit;
    private UnitCard unitCard;

    private GameObject selectUnit;
    private UnitCard selectUnitCard;

    private GameController gameController;
    private TileManager tileManager;
    private PlayerController playerController;
    private DataHandler dataHandler;

    private Transform descriptionBox;
    private float hoverTime;
    private float showUnitDescriptionTime = 0.5f;
    private bool isCheckPosition;

    public TileType tileType;

    public MultiPlayerController multiPlayerController;

    private void Start()
    {
        InitializeTile();
    }

    private void InitializeTile()
    {
        hoverTime = 0f;
        gameController = FindObjectOfType<GameController>();
        multiPlayerController = FindObjectOfType<MultiPlayerController>();
        dataHandler = FindObjectOfType<DataHandler>();
        tileManager = GameObject.Find("Tiles").GetComponent<TileManager>();
        playerController = GameObject.Find("PlayerController").GetComponent<PlayerController>();

        hoverHighlight = transform.Find("Highlight").gameObject;
        nextMoveHighlight = transform.Find("NextMoveHightlight").gameObject;
        unitHighlight = transform.Find("UnitHighlight").gameObject;
    }

    //-------------- Hover Highlight Mouse --------------//
    void OnMouseEnter()
    {
        hoverHighlight.SetActive(true);
    }
    void OnMouseExit()
    {
        if(descriptionBox != null)
        {
            descriptionBox.gameObject.SetActive(false);
        }
        hoverHighlight.SetActive(false);
        hoverTime = 0f;
    }

    private void Update()
    {
        if(hoverHighlight.activeInHierarchy == true)
        {
            hoverTime += Time.deltaTime;
        }
        if(hoverTime >= showUnitDescriptionTime)
        {
            unit = GetUnitInTile();
            if(unit != null)
            {
                //if(unit.GetComponent<UnitCard>().isPlayCard == true || unit.GetComponent<UnitCard>().GetPlayerNo() == GameController.CurrentTurn)
                int player = 1;
                
                bool isEnemyHandTile = false;
                if(player == 1)
                {
                    isEnemyHandTile = tileType == TileType.Player2Tile ? true : false;
                }

                if(!isEnemyHandTile)
                {
                    Transform canvas = unit.transform.Find("Canvas");
                    descriptionBox = canvas.Find("DescriptionBox");
                    descriptionBox.gameObject.SetActive(true);
                    if(tileType == TileType.Player2Tile && !isCheckPosition)
                    {
                        //unit description on right screen show in the left side instead
                        descriptionBox.position -= new Vector3(4.3f,0,0);
                        isCheckPosition = true;
                    }
                }
            }
        }
    }

    //-------------- Click On Tile --------------//
    void OnMouseDown()
    {
        if(gameController.GetPlayerId() == GameController.CurrentTurn)
        {
            unit = GetUnitInTile();
            selectUnit = tileManager.GetSelectUnit();
            HandleClickOnTile(unit, selectUnit);
        }
    }
        

    private void HandleClickOnTile(GameObject unit, GameObject selectUnit)
    {
        //-------------- Retrieve the UnitCard component --------------//

        if (selectUnit != null) selectUnitCard = selectUnit.GetComponent<UnitCard>();

        //-------------- Click on a tile that has the unitHighlight --------------//

        if (unitHighlight.activeInHierarchy)
        {
            HandleFoundUnitHighlight(unit, selectUnit);
            return;
        }

        if (tileManager.isInSkillProcess)
        {
            return;
        }

        //-------------- Click on a tile that has the nextMoveHighlight --------------//

        if (nextMoveHighlight.activeInHierarchy)
        {
            HandleFoundNextMoveHighlight(unitCard, selectUnit);
            return;
        }

        //-------------- Click on an empty tile or an enemy unit --------------//

        if (unit == null || unitCard.GetPlayerNo() != gameController.GetPlayerId())
        {
            tileManager.DeSelectUnit();
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
            tileManager.SetSelectUnit(unit);
            HighlightNextMoveUnit(unitCard, xPos, yPos);
        }

        //-------------- Click on a unit that has already moved --------------//

        else if (unitCard.GetCardCredit() <= 0)
        {
            tileManager.DeSelectUnit();
            return;
        }

        //-------------- Click on a friendly unit to set it as the new SelectUnit --------------//

        else
        {
            tileManager.DeSelectUnit();
            tileManager.SetSelectUnit(unit);
            HighlightNextMoveUnit(unitCard, xPos, yPos);
        }
    }

    private void HandleFoundUnitHighlight(GameObject unit, GameObject selectUnit)
    {
        Debug.Log("3");
        selectUnitCard = selectUnit.GetComponent<UnitCard>();
        selectUnitCard.SetSkillTarget(unit);
        selectUnitCard.UnitSkill();
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
                //hit tower 1 tile
                //playerController.SetPlayerHP(1, playerController.GetPlayerHP(1) - selectUnitCard.GetAttackDamage());

                //----------------- SEND API HERE -------------------//

                int arenaId = PlayerPrefs.GetInt("ArenaId");
                arenaId = gameController.arenaId;
                int attackerIndex = ConvertTilePosToIndex(selectUnit.GetComponentInParent<Tile>().GetXPos(), selectUnit.GetComponentInParent<Tile>().GetYPos());
                int defenderId = dataHandler.player1Id;
                StartCoroutine(multiPlayerController.AttackTower(arenaId, defenderId, attackerIndex, (response) =>
                {
                    StartCoroutine(multiPlayerController.MarkUseCard(arenaId, attackerIndex, (response) => { }));
                }));


                //----------------- Attack Animation -------------------//
                if (selectUnitCard.GetUnitAttackType() == UnitCardStat.AttackType.Melee)
                {
                    selectUnitCard.MeleeAttackAnimation(null);
                }
                else
                {
                    selectUnitCard.RangeAttackAnimation(this.gameObject);
                }
                //------------------------------------------------------//


                tileManager.DeSelectUnit();
                selectUnitCard.ReduceCardCredit();
            }
            else
            {
                tileManager.DeSelectUnit();
            }
        }

        //-------------- Click on Player 2 Tower Tile --------------//

        else if (tileType == TileType.Player2Tower)
        {
            if (selectUnitCard.GetPlayerNo() == 1)
            {
                //hit tower 2 tile
                //playerController.SetPlayerHP(2, playerController.GetPlayerHP(2) - selectUnitCard.GetAttackDamage());

                //----------------- SEND API HERE -------------------//

                int arenaId = PlayerPrefs.GetInt("ArenaId");
                arenaId = gameController.arenaId;
                int attackerIndex = ConvertTilePosToIndex(selectUnit.GetComponentInParent<Tile>().GetXPos(), selectUnit.GetComponentInParent<Tile>().GetYPos());
                int defenderId = dataHandler.player2Id;
                StartCoroutine(multiPlayerController.AttackTower(arenaId, defenderId, attackerIndex, (response) =>
                {
                    StartCoroutine(multiPlayerController.MarkUseCard(arenaId, attackerIndex, (response) => { }));
                }));

                //----------------- Attack Animation -------------------//
                if (selectUnitCard.GetUnitAttackType() == UnitCardStat.AttackType.Melee)
                {
                    selectUnitCard.MeleeAttackAnimation(null);
                }
                else
                {
                    Debug.Log("Tile name = " + this.gameObject.name);
                    selectUnitCard.RangeAttackAnimation(this.gameObject);
                }
                //------------------------------------------------------//

                tileManager.DeSelectUnit();
                selectUnitCard.ReduceCardCredit();
            }
            else
            {
                tileManager.DeSelectUnit();
            }
        }

        //-------------- Click on an empty tile to move the selected unit to that tile --------------//

        else if (selectUnit != null && unit == null)
        {
            //|| selectUnitCard.GetComponentInParent<Tile>().tileType != TileType.Player1Tile || selectUnitCard.GetComponentInParent<Tile>().tileType != TileType.Player2Tile
            if (selectUnitCard.isPlayCard )
            {
                MoveUnitToThisTile(selectUnit);
                tileManager.DeSelectUnit();
                selectUnitCard.ReduceCardCredit();
            }

            //-------------- This unit has not been played yet --------------//

            else
            {
                Debug.Log("2");
                //unit play skill here
                selectUnitCard.isPlayCard = true;
                playerController.SetPlayerMana(selectUnitCard.GetPlayerNo(), playerController.GetPlayerMana(selectUnitCard.GetPlayerNo()) - selectUnitCard.mana);
                //send API in this method
                MoveUnitFromHandToArenaTile(selectUnit);

                tileManager.CancelNextMoveHighlight();
                selectUnitCard.ReduceCardCredit();
                tileManager.isInSkillProcess = true;
                //unit use skill
                selectUnitCard.UnitHighlight();
                
            }
        }

        //-------------- Click on enemy unit to attack --------------//

        else if (unitCard.GetPlayerNo() != gameController.GetPlayerId())
        {
            if(selectUnitCard.isPlayCard == false)
            {
                selectUnitCard.isPlayCard = true;
                //playerController.SetPlayerMana(selectUnitCard.GetPlayerNo(), selectUnitCard.mana);
                playerController.SetPlayerMana(selectUnitCard.GetPlayerNo(), playerController.GetPlayerMana(selectUnitCard.GetPlayerNo()) - selectUnitCard.mana);
                // Move unit to tower tile //
                MoveUnitToTowerTile(selectUnit, selectUnitCard.GetPlayerNo(), xPos, yPos);
                Debug.Log("Move Unit Passed");
            }
            else
            {
                selectUnitCard.AttackUnit(unitCard);
                Debug.Log("Attack Enemy");
            }
            selectUnitCard.ReduceCardCredit();
            tileManager.DeSelectUnit();

        }

        //-------------- Click on the friendly unit that has already moved --------------//

        else if (unitCard.GetCardCredit() <= 0)
        {
            // move Select unit to this tile
            tileManager.DeSelectUnit();
        }

        //-------------- Click on the friendly unit that has not moved, and select it as selectUnit --------------//

        else
        {
            tileManager.DeSelectUnit();
            tileManager.SetSelectUnit(unit);
            HighlightNextMoveUnit(unitCard, xPos, yPos);
        }
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

    public void MoveUnitToThisTile(GameObject selectUnit)
    {
        //------------ SEND API HERE ---------------//
        int arenaId = PlayerPrefs.GetInt("ArenaId");
        arenaId = gameController.arenaId;
        int beforeTileIndex = ConvertTilePosToIndex(selectUnit.GetComponentInParent<Tile>().GetXPos(), selectUnit.GetComponentInParent<Tile>().GetYPos());
        int afterTileIndex = ConvertTilePosToIndex(xPos, yPos);
        StartCoroutine(multiPlayerController.MoveCard(arenaId, beforeTileIndex, afterTileIndex, (response) => 
        { 
            Debug.Log("Api: Move Card From " + beforeTileIndex + " To " + afterTileIndex);

            Delay(0.5f);

            //StartCoroutine(multiPlayerController.MarkUseCard(arenaId, afterTileIndex, (response) => { Debug.Log("Api: Mark Use Card Index " + afterTileIndex); }));
            if (buff != null && buffIndex == 1)
            {
                selectUnit.GetComponent<UnitCard>().IncreaseAttackDamage(1);
                selectUnit.GetComponent<UnitCard>().IncreaseHealth(1);

                StartCoroutine(multiPlayerController.UpdateBuff(arenaId, false, dataHandler.arena.buffTwoActive, (response) => { Debug.Log("Api: UpdateBuff"); }));
                Destroy(buff, 0.3f);
            }
            if (buff != null && buffIndex == 2)
            {
                selectUnit.GetComponent<UnitCard>().IncreaseAttackDamage(1);
                selectUnit.GetComponent<UnitCard>().IncreaseHealth(1);

                StartCoroutine(multiPlayerController.UpdateBuff(arenaId, dataHandler.arena.buffOneActive, false, (response) => { Debug.Log("Api: UpdateBuff"); }));
                Destroy(buff, 0.3f);
            }

        }));
        //------------------------------------------//
        selectUnit.transform.SetParent(transform);
        selectUnit.transform.position = transform.position;
        selectUnit.GetComponent<UnitCard>().ReduceCardCredit();
        
    }

    public void MoveUnitFromHandToArenaTile(GameObject selectUnit)
    {
        //------------ SEND API HERE ---------------//
        int arenaId = PlayerPrefs.GetInt("ArenaId");
        int cardId;
        if(selectUnit.GetComponent<UnitCard>().GetPlayerNo() == 1)
        {
            Tile selectUnitTile = selectUnit.GetComponentInParent<Tile>();
            cardId = dataHandler.player1HandCards[ConvertTilePosToIndex(selectUnitTile.GetXPos(), selectUnitTile.GetYPos())].id;
            Debug.Log("cardId = " + cardId);
        }
        else
        {
            Tile selectUnitTile = selectUnit.GetComponentInParent<Tile>();
            cardId = dataHandler.player2HandCards[ConvertTilePosToIndex(selectUnitTile.GetXPos(), selectUnitTile.GetYPos())].id;
        }

        //int cardId = selectUnit.GetComponent<UnitCard>().GetPlayerNo() == 1 ? dataHandler.player1HandCards[yPos].id : dataHandler.player2HandCards[yPos].id;
        arenaId = gameController.arenaId;
        int tileIndex = ConvertTilePosToIndex(xPos, yPos);
        StartCoroutine(multiPlayerController.LaydownCard(arenaId, cardId, tileIndex, (response) => { Debug.Log("Api: Laydown from hand to tile " + tileIndex); }));

        //------------------------------------------//
        selectUnit.transform.SetParent(transform);
        selectUnit.transform.position = transform.position;
        selectUnit.GetComponent<UnitCard>().ReduceCardCredit();

        if (buff != null)
        {
            selectUnit.GetComponent<UnitCard>().IncreaseAttackDamage(1);
            selectUnit.GetComponent<UnitCard>().IncreaseHealth(1);
            Destroy(buff, 0.3f);
        }
    }

    public void MoveUnitToTowerTile(GameObject unit, int playerNo, int PosX, int PosY)
    {
        GameObject towerTile;
        if (playerNo == 1)
        {
            towerTile = GameObject.Find($"Tile {PosX - 1} {PosY}");
        }
        else
        {
            towerTile = GameObject.Find($"Tile {PosX + 1} {PosY}");
        }
        
        //------------ SEND API HERE ---------------//
        int arenaId = PlayerPrefs.GetInt("ArenaId");
        Tile unitHandTile = unit.GetComponentInParent<Tile>();
        int handTileIndex = ConvertTilePosToIndex(unitHandTile.GetXPos(), unitHandTile.GetYPos());
        int cardId = playerNo == 1? dataHandler.player1HandCards[handTileIndex].id : dataHandler.player2HandCards[handTileIndex].id;
        arenaId = gameController.arenaId;
        int tileIndex = ConvertTilePosToIndex(towerTile.GetComponent<Tile>().GetXPos(), towerTile.GetComponent<Tile>().GetYPos());
        StartCoroutine(multiPlayerController.LaydownCard(arenaId, cardId, tileIndex, (response) =>
        {
            Debug.Log("Api: Laydown From " + tileIndex + " To Tower");
            Delay(0.5f);

            StartCoroutine(multiPlayerController.MarkUseCard(arenaId, tileIndex, (response) => { Debug.Log("Api: Mark Use Card Index " + tileIndex); }));
        }));
        //------------------------------------------//

        unit.transform.SetParent(towerTile.transform);
        unit.transform.position = towerTile.transform.position;
        selectUnit.GetComponent<UnitCard>().ReduceCardCredit();

    }

    public void SetNextMoveHighlight(bool isSelect)
    {
        nextMoveHighlight.SetActive(isSelect);
    }

    public void SetUnitHighlight(bool isSelect)
    {
        unitHighlight.SetActive(isSelect);
    }

    public int GetXPos()
    {
        return xPos;
    }

    public int GetYPos()
    {
        return yPos;
    }

    public int ConvertTilePosToIndex(int xPos, int yPos)
    {
        return xPos * 6 + yPos;
    }

    public IEnumerable Delay(float delay)
    {
        yield return new WaitForSeconds(delay);
    }
}


