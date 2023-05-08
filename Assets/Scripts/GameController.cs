using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameController : MonoBehaviour
{
    private static int playerturn = 1;
    public int playerId; // P1 = 1, P2 = 2

    public SpawnCard spawnP1Card;
    public SpawnCard spawnP2Card;
    public PlayerController playerController;
    public TileManager tileManager;

    [SerializeField]
    private GameObject player1Arrow;
    [SerializeField]
    private GameObject player2Arrow;
    private List<UnitCard> unitCards;

    private int firstHandCards = 3;
    private float time = 0f;
    private float timeout = 10f;
    private bool alreadyLoad;

    public delegate void ChangeCardBackDelegate(int playerNo);
    public event ChangeCardBackDelegate OnChangeCardBack;
    public static int CurrentTurn => playerturn;

    private void Start()
    {
        playerId = PlayerPrefs.GetInt("PlayerId");
        playerId = 1;
    }

    private void Update()
    {
        time += Time.deltaTime;
        if(time >= timeout)
        {
            //throw load error go back scene
        }
        else if(!spawnP1Card.isLoading || !spawnP2Card.isLoading)
        {
            return;
        }
        else if(alreadyLoad == false)
        {
            for (int i = 0; i < firstHandCards; i++)
            {
                spawnP1Card.InitialSpawn(true);
                spawnP2Card.InitialSpawn(false);
            }
            playerController.RefreshPlayerMana(1);
            alreadyLoad = true;
        }
    }

    public void UseUnitsEndturnSkill(int playerNo)
    {
        unitCards = tileManager.SelectFriendlyUnits(playerNo);
        foreach (UnitCard unitCard in unitCards)
        {
            unitCard.EndTurnSkill();
        }
    }

    public void UseUnitsStartturnSkill(int playerNo)
    {
        unitCards = tileManager.SelectFriendlyUnits(playerNo);
        foreach (UnitCard unitCard in unitCards)
        {
            unitCard.StartTurnSkill();
        }
    }

    public void ChangeTurn()
    {
        tileManager.DeSelectUnit();
        tileManager.CancelNextMoveHighlight();
        tileManager.CancelUnitMoveHighlight();

        UseUnitsEndturnSkill(playerturn);
        
        playerturn = playerturn == 1 ? 2 : 1;
        if(playerturn == 1)
        {
            if(OnChangeCardBack != null)
            {
                OnChangeCardBack(1);
            }
            player1Arrow.SetActive(true);
            player2Arrow.SetActive(false);
            spawnP1Card.SpawnUnit();
            playerController.RefreshPlayerMana(1);
            RefreshPlayerCredit(1);
            UseUnitsStartturnSkill(1);
        }
        else if(playerturn == 2)
        {
            if (OnChangeCardBack != null)
            {
                OnChangeCardBack(2);
            }
            player1Arrow.SetActive(false);
            player2Arrow.SetActive(true);
            spawnP2Card.SpawnUnit();
            playerController.RefreshPlayerMana(2);
            RefreshPlayerCredit(2);
            UseUnitsStartturnSkill(2);
        }
        //RefreshPlayerCredit(playerturn == 1 ? 2 : 1);
    }

    public void SetPlayerTurn(int newPlayerTurn)
    {
        playerturn = newPlayerTurn;
    }

    public int GetPlayerId()
    {
        return playerId;
    }
    private void RefreshPlayerCredit(int playerNo)
    {
        GameObject[] units = GameObject.FindGameObjectsWithTag("Unit");
        foreach (GameObject unit in units)
        {
            UnitCard unitCard = unit.GetComponent<UnitCard>();
            if (unitCard.GetPlayerNo() == playerNo)
            {
                unitCard.RefreshCredit();
            }
        }
    }
}