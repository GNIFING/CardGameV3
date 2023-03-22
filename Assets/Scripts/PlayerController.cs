using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private static int player1HP;
    private static int player2HP;
    private static int player1Mana;
    private static int player2Mana;
    private static int player1MaxMana;
    private static int player2MaxMana;
    private static int startingMana = 1;


    public TextMeshProUGUI player1HPText;
    public TextMeshProUGUI player2HPText;
    public ShowMana player1ShowMana;
    public ShowMana player2ShowMana;
    public TextMeshProUGUI playerWinText;

    // Start is called before the first frame update
    void Start()
    {
        player1HP = 20;
        player2HP = 20;
        player1MaxMana = startingMana;
        player2MaxMana = startingMana;
        player1Mana = player1MaxMana;
        player2Mana = player2MaxMana;

        player1HPText.text = player1HP.ToString();
        player2HPText.text = player2HP.ToString();
    }

    public int GetPlayerHP(int playerNo)
    {
        return playerNo == 1 ? player1HP : player2HP;
    }

    public void SetPlayerHP(int playerNo, int newPlayerHp)
    {
        if (playerNo == 1)
        {
            player1HP = newPlayerHp;
            player1HPText.text = player1HP.ToString();
            CheckWinCondition(player1HP, 2);
        }
        else if (playerNo == 2)
        {
            player2HP = newPlayerHp;
            player2HPText.text = player2HP.ToString();
            CheckWinCondition(player2HP, 1);
        }
    }

    public void DecreaseP1HP()
    {
        Debug.Log("decrease HP 2!");
        player1HP -= 2;
        player1HPText.text = player1HP.ToString();
        CheckWinCondition(player1HP, 2);
    }

    public void DecreaseP2HP()
    {
        Debug.Log("decrease HP 2!");
        player2HP -= 2;
        player2HPText.text = player2HP.ToString();
        CheckWinCondition(player2HP, 1);
    }

    private void CheckWinCondition(int Hp, int player)
    {
        if(Hp <= 0)
        {
            playerWinText.text = "Player " + player + "Win!";
        }
    }

    public int GetPlayerMana(int playerNo)
    {
        return playerNo == 1 ? player1Mana : player2Mana;
    }

    public int GetPlayerMaxMana(int playerNo)
    {
        return playerNo == 1 ? player1MaxMana : player2MaxMana;

    }

    public void SetPlayerMana(int PlayerNo, int cardMana)
    {
        if (PlayerNo == 1)
        {
            player1Mana -= cardMana;
            player1ShowMana.SetManaColor(player1Mana, player1MaxMana);
        }
        else if (PlayerNo == 2)
        {
            player2Mana -= cardMana;
            player2ShowMana.SetManaColor(player2Mana, player2MaxMana);
        }
    }
    public void RefreshPlayerMana(int PlayerNo)
    {
        if(PlayerNo == 1)
        {
            if (player1MaxMana == 10) player1Mana = 10;
            else
            {
                player1MaxMana += 1;
                player1Mana = player1MaxMana;
                player1ShowMana.SetManaColor(player1Mana, player1MaxMana);
            }
        }
        else if(PlayerNo == 2)
        {
            if (player2MaxMana == 10) player2Mana = 10;
            else
            {
                player2MaxMana += 1;
                player2Mana = player2MaxMana;
                player2ShowMana.SetManaColor(player2Mana, player2MaxMana);
            }
        }
    }

    public bool IsEnoughMana(UnitCard unitCard)
    {
        if(unitCard.GetPlayerNo() == 1)
        {
            return player1Mana >= unitCard.mana;
        }
        else if (unitCard.GetPlayerNo() == 2)
        {
            return player2Mana >= unitCard.mana;
        }
        return false;
    }
}
