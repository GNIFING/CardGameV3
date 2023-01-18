using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitCard : MonoBehaviour
{
    [SerializeField] private int playerNo;
    [SerializeField] private int maxCardCredit = 1;

    private int health;
    private int attack;
 
    private int cardCredit;

    private void Start()
    {
        cardCredit = maxCardCredit;
    }

    public int GetPlayerNo()
    {
        return playerNo;
    }

    public int GetCardCredit()
    {
        return cardCredit;
    }

    public void SetCardCredit(int setCardCredit)
    {
        cardCredit = setCardCredit;
    }

    public void ReduceCardCredit()
    {
        if(cardCredit >= 0)
        {
            cardCredit -= 1;
        }
    }

    public void RefreshCredit()
    {
        cardCredit = maxCardCredit;
    }

}
