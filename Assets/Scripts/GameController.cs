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
    public int arenaId;

    [SerializeField]
    private GameObject player1Arrow;
    [SerializeField]
    private GameObject player2Arrow;
    private List<UnitCard> unitCards;

    private int firstHandCards = 3;
    private float time = 0f;
    private float timeout = 10f;
    private bool alreadyLoad;

    public DataHandler dataHandler;
    public MultiPlayerController multiPlayerController;
    public GameObject endTurnButton;
    public static int CurrentTurn => playerturn;

    public Queue<ArenaApiQueue> arenaApiQueue = new();
    public bool isArenaApiUpdating;

    private void Start()
    {
        playerId = PlayerPrefs.GetInt("PlayerId");
        arenaId = PlayerPrefs.GetInt("ArenaId");
        //playerId = 1;
        //arenaId = 2;

    }

    private void Update()
    {
        // Check api queue
        if (arenaApiQueue.Count > 0 && !isArenaApiUpdating)
        {
            ArenaApiQueue apiData = arenaApiQueue.Dequeue();
            Debug.Log("Pop api queue " + apiData.path);
            StartCoroutine(CallArenaApi(apiData));
        }

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

        player1Arrow.SetActive(playerturn == 1);
        player2Arrow.SetActive(playerturn == 2);

    }

    public void UseUnitsEndturnSkill(int playerNo)
    {
        unitCards = tileManager.SelectFriendlyUnits(playerNo);
        foreach (UnitCard unitCard in unitCards)
        {
            Debug.Log("end skill of " + unitCard.name);
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


        UseUnitsEndturnSkill(GetPlayerId());

        //----------- SEND API -----------//

        // end turn api
        arenaApiQueue.Enqueue(new ArenaApiQueue
        {
            path = "/turn",
            arenaId = arenaId,
            playerId = playerId,
        });
        //StartCoroutine(multiPlayerController.EndTurn(arenaId, playerId, (response) => { Debug.Log("Api: End Turn Player " + playerId); }));


        //----------- SEND API -----------//

        UpdateIsPlay();
        playerturn = playerturn == 1 ? 2 : 1;

        if (playerturn == 1)
        {
            //spawnP1Card.SpawnUnit(); // SEND DRAW CARD API
            playerController.RefreshPlayerMana(1);
            RefreshPlayerCredit(2);
            UseUnitsStartturnSkill(1); 
        }
        else if(playerturn == 2)
        {
            //spawnP2Card.SpawnUnit();
            playerController.RefreshPlayerMana(2);
            RefreshPlayerCredit(1);
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
        return playerId == dataHandler.player1Id? 1 : 2;
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

    public void UpdateIsPlay()
    {
        List<GameObject> unitObjs = tileManager.GetAllUnits();
        foreach (GameObject unitObj in unitObjs)
        {
            unitObj.GetComponent<UnitCard>().isPlayCard = true;
        }
    }
    public void Surrender()
    {
        Debug.Log("Surrender");
        // surrender api
        arenaApiQueue.Enqueue(new ArenaApiQueue
        {
            path = "/surrender",
            arenaId = arenaId,
            playerId = playerId,
        });
        //StartCoroutine(multiPlayerController.Surrender(arenaId, playerId, (response) => { }));


    }

    public IEnumerator CallArenaApi(ArenaApiQueue apiData)
    {
        isArenaApiUpdating = true;

        int afterIndex = apiData.afterIndex ?? -1;
        int arenaId = apiData.arenaId ?? -1;
        int atk = apiData.atk ?? -1;
        int attackerIndex = apiData.attackerIndex ?? -1;
        int beforeIndex = apiData.beforeIndex ?? -1;
        bool buffOneActive = apiData.buffOneActive ?? false;
        bool buffTwoActive = apiData.buffTwoActive ?? false;
        int cardId = apiData.cardId ?? -1;
        int cardIndex = apiData.cardIndex ?? -1;
        int defenderIndex = apiData.defenderIndex ?? -1;
        int defenderId = apiData.defenderId ?? -1;
        int hp = apiData.hp ?? -1;
        int index = apiData.index ?? -1;
        int playerId = apiData.playerId ?? -1;

        yield return new WaitForSeconds(0f);

        switch (apiData.path)
        {
            case "/drawCard":
                StartCoroutine(
                    multiPlayerController.DrawCard(arenaId, playerId, (response) => {
                        Debug.Log("Api: Draw Card Player " + playerId);
                        isArenaApiUpdating = false;
                    })
                );
                break;
            case "/laydown":
                StartCoroutine(
                    multiPlayerController.LaydownCard(arenaId, cardId, index, (response) => {
                        Debug.Log("Api: Laydown from hand to tile " + index);
                        isArenaApiUpdating = false;
                    }));
                break;
            case "/surrender":
                StartCoroutine(
                    multiPlayerController.Surrender(arenaId, playerId, (response) => {
                        Debug.Log("Api: Surrender by player " + playerId);
                        isArenaApiUpdating = false;
                    }));
                break;
            case "/attack/card":
                StartCoroutine(
                    multiPlayerController.AttackCard(arenaId, attackerIndex, defenderIndex, (response) => {
                        Debug.Log("Api: Attack from index " + attackerIndex + " to " + defenderIndex);
                        isArenaApiUpdating = false;
                    }));
                break;
            case "/update/card":
                StartCoroutine(
                    multiPlayerController.UpdateCard(arenaId, cardIndex, hp, atk, (response) => {
                        Debug.Log("Api: Update from index " + cardIndex);
                        isArenaApiUpdating = false;
                    }));
                break;
            case "/attack/tower":
                StartCoroutine(
                    multiPlayerController.AttackTower(arenaId, defenderId, attackerIndex, (response) => {
                        Debug.Log("Api: Attack tower from index " + attackerIndex);
                        isArenaApiUpdating = false;
                    }));
                break;
            case "/buff/update":
                StartCoroutine(
                    multiPlayerController.UpdateBuff(arenaId, buffOneActive, buffTwoActive, (response) => {
                        Debug.Log("Api: Active buff 1 " + buffOneActive + " buff 2 " + buffTwoActive);
                        isArenaApiUpdating = false;
                    }));
                break;
            case "/move":
                StartCoroutine(
                    multiPlayerController.MoveCard(arenaId, beforeIndex, afterIndex, (response) => {
                        Debug.Log("Api: Move Card From " + beforeIndex + " To " + afterIndex);
                        isArenaApiUpdating = false;
                    }));
                break;
            case "/turn":
                StartCoroutine(
                    multiPlayerController.EndTurn(arenaId, playerId, (response) => {
                        Debug.Log("Api: End Turn Player " + playerId);
                        isArenaApiUpdating = false;
                    }));
                break;
            default:
                break;
        }
    }
}