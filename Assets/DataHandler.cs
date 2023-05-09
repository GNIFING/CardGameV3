using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DataHandler : MonoBehaviour
{
    //-------------- Player 1 --------------//
    
    public int player1Id;
    public int player1DeckId;
    public List<UserCard?> player1HandCards;
    public int player1Hp;
    public int player1MaxMana;
    public int player1Mana;
    public bool isPlayer1Turn;

    //-------------- Player 2 --------------//

    public int player2Id;
    public int player2DeckId;
    public List<UserCard?> player2HandCards;
    public int player2Hp;
    public int player2MaxMana;
    public int player2Mana;
    public bool isPlayer2Turn;

    //-------------- Arena --------------//

    public Arena arena;

    //-------------- CardsOnBoard --------------//

    public List<UserCard?> cardsOnBoard;

    //-------------- Game Condition --------------//

    public bool gameOver;
    public int? winner;

    //-------------- Attack Condition --------------//

    public int? attackerIndex;
    public int? defenderIndex;

    //-------------- In Game Data --------------//

    public GameController gameController;
    public PlayerController playerController;
    public TileManager tileManager;
    public List<Tile> tiles;

    public SpawnCard player1Spawn;
    public SpawnCard player2Spawn;
    public List<Tile> player1Tile;
    public List<Tile> player2Tile;

    public List<GameObject> cardPrefabs;

    public void Start()
    {
        foreach (Transform child in tileManager.transform)
        {
            tiles.Add(child.gameObject.GetComponent<Tile>());
        }
        foreach (Transform child in player1Spawn.transform)
        {
            player1Tile.Add(child.gameObject.GetComponent<Tile>());
        }
        foreach (Transform child in player2Spawn.transform)
        {
            player2Tile.Add(child.gameObject.GetComponent<Tile>());
        }
        Debug.Log("Link objects Passed");

        //Draw 3 initial card here
    }

    public void UpdateData(GameData gameData)
    {
        player1Id = gameData.playerOne.id;
        player1DeckId = gameData.playerOne.deckId;
        player1HandCards = gameData.playerOne.cards.ToList();
        player1Hp = gameData.playerOne.hp;
        player1MaxMana = gameData.playerOne.maxMana;
        player1Mana = gameData.playerOne.mana;
        isPlayer1Turn = gameData.playerOne.isTurn;

        player2Id = gameData.playerTwo.id;
        player2DeckId = gameData.playerTwo.deckId;
        player2HandCards = gameData.playerTwo.cards.ToList();
        player2Hp = gameData.playerTwo.hp;
        player2MaxMana = gameData.playerTwo.maxMana;
        player2Mana = gameData.playerTwo.mana;
        isPlayer2Turn = gameData.playerTwo.isTurn;

        arena = gameData.arena;

        cardsOnBoard = gameData.cardOnBoard.ToList();
        
        gameOver = gameData.gameOver;
        Debug.Log("gameOver = " + gameOver);
        winner = gameData.winner;
        attackerIndex = gameData.attackerIndex;
        defenderIndex = gameData.defenderIndex;

        gameController.SetPlayerTurn(gameData.playerOne.isTurn ? 1 : 2);
        gameController.endTurnButton.SetActive(gameController.playerId == player1Id ? isPlayer1Turn : isPlayer2Turn);
        
        Debug.Log("Update Initial Data Passed");

        CheckAttackAnimation(attackerIndex);
        UpdatePlayerStat();
        Debug.Log("Update Player Stat Passed");
        UpdatePlayerHands();
        Debug.Log("Update Player Hand Passed");
        UpdateArenaCardsPosition();
        Debug.Log("Update Arena Card Passed");
        CheckWinCondition();
        Debug.Log("Update Player Stat Passed");
    }

    public void CheckAttackAnimation(int? attackerIndex)
    {
        if(attackerIndex == null)
        {
            //do nothing
        }
        else
        {
            UnitCard attakerUnit = tiles[(int)attackerIndex].GetUnitInTile().GetComponent<UnitCard>();
            UnitCard defenderUnit = tiles[(int)defenderIndex].GetUnitInTile().GetComponent<UnitCard>();

            if(attakerUnit.unitCardStat.CurrentAttackType == UnitCardStat.AttackType.Melee)
            {
                attakerUnit.MeleeAttackAnimation(defenderUnit);
            }
            else
            {
                attakerUnit.RangeAttackAnimation(defenderUnit.gameObject);
            }

            //run animation
        }
    }
    public void CheckWinCondition()
    {
        if(gameOver == true)
        {
            playerController.PlayerWin(winner);
        }
    }

    public void UpdatePlayerStat()
    {
        //Hp
        playerController.SetPlayerHP(1, player1Hp);
        playerController.SetPlayerHP(2, player2Hp);
        //MaxMana
        playerController.SetPlayerMaxMana(1, player1MaxMana);
        playerController.SetPlayerMaxMana(2, player2MaxMana);
        //Mana
        playerController.SetPlayerMana(1, player1Mana);
        playerController.SetPlayerMana(2, player2Mana);
    }

    public void UpdatePlayerHands()
    {
        //player1
        for (int handIndex = 0; handIndex < 12; handIndex++)
        {
            if (player1HandCards[handIndex] == null)
            {
                if (player1Tile[handIndex].GetUnitInTile() != null)
                {
                    GameObject unitCard = player1Tile[handIndex].GetUnitInTile();
                    Destroy(unitCard);
                }
            }
            else if (player1HandCards[handIndex] != null)
            {
                if (player1Tile[handIndex].GetUnitInTile() != null)
                {
                    UnitCard unitCard = player1Tile[handIndex].GetUnitInTile().GetComponent<UnitCard>();
                    if(unitCard.GetId() != player1HandCards[handIndex].card.id)
                    {
                        Destroy(unitCard.gameObject);
                    }
                }
                else if(player1Tile[handIndex].GetUnitInTile() == null)
                {
                    GameObject newUnitCardObj = Instantiate(cardPrefabs[player1HandCards[handIndex].card.id], player1Tile[handIndex].transform.position, Quaternion.identity);
                    newUnitCardObj.transform.parent = player1Tile[handIndex].transform;
                    UnitCard newUnitCard = newUnitCardObj.GetComponent<UnitCard>();
                    newUnitCard.SetUserCardId(player1HandCards[handIndex].id);
                    newUnitCard.SetPlayerNo(1);
                    newUnitCard.RefreshCredit();
                    newUnitCard.SetBackCard(true);
                    if (newUnitCard.GetComponentInParent<Tile>().tileType != Tile.TileType.Player1Tile)
                    {
                        newUnitCard.SetAttackDamage(player1HandCards[handIndex].atk);
                        newUnitCard.SetHealth(player1HandCards[handIndex].hp);
                        newUnitCard.UpdateCardUI();

                    }
                }
            }
        }

        //player2
        for (int handIndex = 0; handIndex < 12; handIndex++)
        {
            if (player2HandCards[handIndex] == null)
            {
                if (player2Tile[handIndex].GetUnitInTile() != null)
                {
                    GameObject unitCard = player2Tile[handIndex].GetUnitInTile();
                    Destroy(unitCard);
                    Debug.Log("Destrot case -1");

                }
            }
            else if (player2HandCards[handIndex] != null)
            {
                if (player2Tile[handIndex].GetUnitInTile() != null)
                {
                    UnitCard unitCard = player2Tile[handIndex].GetUnitInTile().GetComponent<UnitCard>();
                    if (unitCard.GetId() != player2HandCards[handIndex].card.id)
                    {
                        Destroy(unitCard.gameObject);
                        Debug.Log("Destrot case 0");

                    }
                }
                else if (player2Tile[handIndex].GetUnitInTile() == null)
                {
                    GameObject newUnitCardObj = Instantiate(cardPrefabs[player2HandCards[handIndex].card.id], player2Tile[handIndex].transform.position, Quaternion.identity);
                    newUnitCardObj.transform.parent = player2Tile[handIndex].transform;
                    UnitCard newUnitCard = newUnitCardObj.GetComponent<UnitCard>();
                    newUnitCard.SetUserCardId(player2HandCards[handIndex].id);
                    newUnitCard.SetPlayerNo(2);
                    newUnitCard.RefreshCredit();
                    newUnitCard.SetBackCard(true);
                    if (newUnitCard.GetComponentInParent<Tile>().tileType != Tile.TileType.Player2Tile)
                    {
                        newUnitCard.SetAttackDamage(player2HandCards[handIndex].atk);
                        newUnitCard.SetHealth(player2HandCards[handIndex].hp);
                        newUnitCard.UpdateCardUI();

                    }
                }
            }
        }
        //if there is unit in this tile check that it is same index as in playerhand index
        //if there is no unit check that tile should have unit or not
    }

    public void UpdateArenaCardsPosition()
    {
        for (int arenaIndex = 0; arenaIndex < 42; arenaIndex++)
        {
            if(arena.arenaArray[arenaIndex] == null)
            {
                // If there is no unit in this tile, remove any existing unit.
                if (tiles[arenaIndex].GetUnitInTile() != null)
                {
                    Destroy(tiles[arenaIndex].GetUnitInTile());
                    Debug.Log("case 1");
                }
                // If there is no unit and no card, do nothing.
                Debug.Log("case 2");
            }
            else
            {
                if (tiles[arenaIndex].GetUnitInTile() != null)
                {
                    // If there is already a unit in this tile, check if it needs to be replaced.
                    UnitCard unitCard = tiles[arenaIndex].GetUnitInTile().GetComponent<UnitCard>();
                    UserCard userCard = cardsOnBoard.SingleOrDefault(x => x.id == arena.arenaArray[arenaIndex]);

                    if (unitCard.GetUserCardId() != userCard.id)
                    {
                        Debug.Log("case 3");
                        // Destroy the existing unit and spawn a new one.
                        Destroy(unitCard.gameObject);

                        GameObject newUnitCardObj = Instantiate(cardPrefabs[userCard.card.id], tiles[arenaIndex].transform.position, Quaternion.identity);
                        newUnitCardObj.transform.parent = tiles[arenaIndex].transform;
                        UnitCard newUnitCard = newUnitCardObj.GetComponent<UnitCard>();

                        newUnitCard.isPlayCard = true;
                        newUnitCard.SetUserCardId((int)arena.arenaArray[arenaIndex]);
                        newUnitCard.SetPlayerNo(userCard.player);
                        newUnitCard.SetBackCard(false);
                        Debug.Log("userCard.atk = " + userCard.atk);
                        newUnitCard.SetAttackDamage(userCard.atk);
                        newUnitCard.SetHealth(userCard.hp);
                        newUnitCard.UpdateCardUI();

                        // Set the new unit's credit based on whether it's ready or not.
                        newUnitCard.SetCardCredit(userCard.isReady ? 1 : 0);
                    }
                    else if(unitCard.GetUserCardId() == userCard.id) ///////////////////////////////////////////////////////
                    {
                        // Update the existing unit's attack and health.
                        Debug.Log("userCard.atk = " + userCard.atk);

                        unitCard.SetAttackDamage(userCard.atk);
                        unitCard.SetHealth(userCard.hp);
                        
                    }
                }
                else if (tiles[arenaIndex].GetUnitInTile() == null)
                {
                    Debug.Log("Case 10");
                    // If there is no unit in this tile, spawn a new one.
                    UserCard userCard = cardsOnBoard.Where(u => u.id == arena.arenaArray[arenaIndex]).Select(u => u).FirstOrDefault();
                    GameObject newUnitCardObj = Instantiate(cardPrefabs[userCard.card.id], tiles[arenaIndex].transform.position, Quaternion.identity);
                    newUnitCardObj.transform.parent = tiles[arenaIndex].transform;
                    UnitCard newUnitCard = newUnitCardObj.GetComponent<UnitCard>();
                    newUnitCard.SetUserCardId((int)arena.arenaArray[arenaIndex]);
                    newUnitCard.SetPlayerNo(userCard.player);
                    newUnitCard.SetBackCard(true);
                    newUnitCard.SetAttackDamage(userCard.atk);
                    newUnitCard.SetHealth(userCard.hp);
                    newUnitCard.UpdateCardUI();

                    // Set the new unit's credit based on whether it's ready or not.
                    newUnitCard.SetCardCredit(userCard.isReady ? 1 : 0);
                }
            }
        }
    }
    //Move All unit here
    //Change All unit Stat here
    //Destroy every object here
    //Change Turn here
    //Change Player HP and MANA
    //Draw Card here
    //Give Buff here -> change stat
}
