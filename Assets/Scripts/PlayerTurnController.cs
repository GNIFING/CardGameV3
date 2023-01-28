using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerTurnController : MonoBehaviour
{
    private static int playerturn = 1; // P1 = 1, P2 = 2
    public PlayerController player1;
    public PlayerController player2;

    public static int CurrentTurn => playerturn;

    public void ChangeTurn()
    {
        playerturn = playerturn == 1 ? 2 : 1;
        (playerturn == 1 ? player1 : player2).SpawnUnit();
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