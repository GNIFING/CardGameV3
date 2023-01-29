using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UnitCard : MonoBehaviour
{
    [SerializeField] private int playerNo;
    [SerializeField] private int maxCardCredit = 1;

    public UnitCardStat unitCardStat;

    public TextMeshProUGUI attackText;
    public TextMeshProUGUI healthText;

    private int _health;
    private int _attack;
 
    private int cardCredit;

    private void Start()
    {
        health = unitCardStat.Hp;
        attack = unitCardStat.AttackDamage;
        cardCredit = maxCardCredit;
        attackText.text = attack.ToString();
        healthText.text = health.ToString();
        
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

    public int health { get; set; }
    public int attack { get; set; }

    public void RefreshCredit()
    {
        cardCredit = maxCardCredit;
    }

    public UnitCardStat.MoveType GetUnitMoveType()
    {
        return unitCardStat.CurrentMoveType;
    }
}
