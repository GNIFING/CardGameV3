using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DataHandler : MonoBehaviour
{
    //-------------- Player 1 --------------//
    
    public int player1Id;
    public int player1DeckId;
    public List<UserCard> player1HandCards;
    public int player1Hp;
    public int player1Mana;
    public bool isPlayer1Turn;

    //-------------- Player 2 --------------//

    public int player2Id;
    public int player2DeckId;
    public List<UserCard> player2HandCards;
    public int player2Hp;
    public int player2Mana;
    public bool isPlayer2Turn;

    //-------------- Arena --------------//

    public Arena arena;

    //-------------- CardsOnBoard --------------//

    public List<UserCard> cardsOnBoard;

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
        player1Id = gameData.player1.id;
        player1DeckId = gameData.player1.deckId;
        player1HandCards = gameData.player1.cards.ToList();
        player1Hp = gameData.player1.hp;
        player1Mana = gameData.player1.mana;
        isPlayer1Turn = gameData.player1.isTurn;

        player2Id = gameData.player2.id;
        player2DeckId = gameData.player2.deckId;
        player2HandCards = gameData.player2.cards.ToList();
        player2Hp = gameData.player2.hp;
        player2Mana = gameData.player2.mana;
        isPlayer2Turn = gameData.player2.isTurn;

        arena = gameData.arena;

        cardsOnBoard = gameData.cardsOnBoard.ToList();
        
        gameOver = gameData.gameOver;
        winner = gameData.winner;
        attackerIndex = gameData.attackerIndex;
        defenderIndex = gameData.defenderIndex;
        Debug.Log("Update Initial Data Passed");

        UpdatePlayerStat();
        Debug.Log("Update Player Stat Passed");
        UpdatePlayerHands();
        Debug.Log("Update Player Hand Passed");
        UpdateArenaCardsPosition();
        Debug.Log("Update Arena Card Passed");
        CheckWinCondition();
        Debug.Log("Update Player Stat Passed");
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
        //Mana
        playerController.SetPlayerMana(1, player1Mana);
        playerController.SetPlayerMana(2, player1Mana);
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
                    newUnitCard.SetPlayerNo(1);
                    newUnitCard.RefreshCredit();
                    newUnitCard.SetBackCard(true);
                    newUnitCard.SetAttackDamage(player1HandCards[handIndex].card.atk);
                    newUnitCard.SetHealth(player1HandCards[handIndex].card.hp);
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
                    }
                }
                else if (player2Tile[handIndex].GetUnitInTile() == null)
                {
                    GameObject newUnitCardObj = Instantiate(cardPrefabs[player1HandCards[handIndex].card.id], player2Tile[handIndex].transform.position, Quaternion.identity);
                    newUnitCardObj.transform.parent = player2Tile[handIndex].transform;
                    UnitCard newUnitCard = newUnitCardObj.GetComponent<UnitCard>();
                    newUnitCard.SetPlayerNo(2);
                    newUnitCard.RefreshCredit();
                    newUnitCard.SetBackCard(true);
                    newUnitCard.SetAttackDamage(player2HandCards[handIndex].card.atk);
                    newUnitCard.SetHealth(player2HandCards[handIndex].card.hp);
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
                }
                // If there is no unit and no card, do nothing.
            }
            else
            {
                if (tiles[arenaIndex].GetUnitInTile() != null)
                {
                    // If there is already a unit in this tile, check if it needs to be replaced.
                    UnitCard unitCard = tiles[arenaIndex].GetUnitInTile().GetComponent<UnitCard>();
                    UserCard userCard = cardsOnBoard.Where(u => u.id == arena.arenaArray[arenaIndex]).Select(u => u).FirstOrDefault();

                    if (unitCard.GetId() != userCard.id)
                    {
                        // Destroy the existing unit and spawn a new one.
                        Destroy(unitCard.gameObject);
                        GameObject newUnitCardObj = Instantiate(cardPrefabs[userCard.id], tiles[arenaIndex].transform.position, Quaternion.identity);
                        newUnitCardObj.transform.parent = tiles[arenaIndex].transform;
                        UnitCard newUnitCard = newUnitCardObj.GetComponent<UnitCard>();
                        newUnitCard.SetPlayerNo(userCard.player);
                        newUnitCard.SetBackCard(true);
                        newUnitCard.SetAttackDamage(userCard.atk);
                        newUnitCard.SetHealth(userCard.hp);

                        // Set the new unit's credit based on whether it's ready or not.
                        newUnitCard.SetCardCredit(userCard.isReady ? 1 : 0);
                    }
                    else if(unitCard.GetId() == userCard.id)
                    {
                        // Update the existing unit's attack and health.
                        unitCard.SetAttackDamage(userCard.atk);
                        unitCard.SetHealth(userCard.hp);
                    }
                }
                else if (tiles[arenaIndex].GetUnitInTile() == null)
                {
                    // If there is no unit in this tile, spawn a new one.
                    UserCard userCard = cardsOnBoard.Where(u => u.id == arena.arenaArray[arenaIndex]).Select(u => u).FirstOrDefault();
                    GameObject newUnitCardObj = Instantiate(cardPrefabs[userCard.id], tiles[arenaIndex].transform.position, Quaternion.identity);
                    newUnitCardObj.transform.parent = tiles[arenaIndex].transform;
                    UnitCard newUnitCard = newUnitCardObj.GetComponent<UnitCard>();
                    newUnitCard.SetPlayerNo(userCard.player);
                    newUnitCard.SetBackCard(true);
                    newUnitCard.SetAttackDamage(userCard.atk);
                    newUnitCard.SetHealth(userCard.hp);

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
