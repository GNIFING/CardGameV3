using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameController : MonoBehaviour
{
    private static int playerturn = 1; // P1 = 1, P2 = 2
    public SpawnCard spawnP1Card;
    public SpawnCard spawnP2Card;
    public PlayerController playerController;

    public static int CurrentTurn => playerturn;

    public void ChangeTurn()
    {
        playerturn = playerturn == 1 ? 2 : 1;
        if(playerturn == 1)
        {
            spawnP1Card.SpawnUnit();
            playerController.RefreshPlayerMana(1);
        }
        else if(playerturn == 2)
        {
            spawnP2Card.SpawnUnit();
            playerController.RefreshPlayerMana(2);
        }
        RefreshPlayerCredit(playerturn == 1 ? 2 : 1);
        Debug.Log("player turn is player " + playerturn);
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