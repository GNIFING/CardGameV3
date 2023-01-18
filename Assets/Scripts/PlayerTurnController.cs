using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurnController : MonoBehaviour
{
    private static int playerturn = 1; // P1 = 1, P2 = 2
    
    public static int GetPlayerTurn()
    {
        return playerturn;
    }
    public static void SetPlayerTurn(int setPlayerTurn)
    {
        playerturn = setPlayerTurn;
    }

    public void ChangeTurn()
    {
        if(playerturn == 1)
        {
            playerturn = 2;
            RefreshPlayerCredit(1);
        }
        else
        {
            playerturn = 1;
            RefreshPlayerCredit(2);
        }
        Debug.Log("player turn is player " + playerturn);
    }

    private void RefreshPlayerCredit(int playerNo)
    {
        GameObject[] units = GameObject.FindGameObjectsWithTag("Unit");
        foreach (GameObject unit in units)
        {
            UnitCard unitCard = unit.GetComponent<UnitCard>();
            if(unitCard.GetPlayerNo() == playerNo)
            {
                unitCard.RefreshCredit();
            }
        }
    }
}
