using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UnitCard : MonoBehaviour
{
    [SerializeField] private int playerNo;
    [SerializeField] private int maxCardCredit = 1;
    [SerializeField] private GameObject MoveCredit;

    public UnitCardStat unitCardStat;

    public TextMeshProUGUI attackText;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI manaText;
 
    public int cardCredit;

    public int health { get; set; }
    public int attack { get; set; }
    public int mana { get; set; }

    private Image unitImage;



    public bool isPlayCard;


    private void Start()
    {
        unitImage = unitCardStat.CardImage;
        health = unitCardStat.Hp;
        attack = unitCardStat.AttackDamage;
        mana = unitCardStat.ManaCost;

        cardCredit = maxCardCredit;
        attackText.text = attack.ToString();
        healthText.text = health.ToString();
        manaText.text = mana.ToString();
        
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
        if(cardCredit > 0)
        {
            cardCredit -= 1;
            if(cardCredit == 0) MoveCredit.SetActive(false);
        }

    }

    public void RefreshCredit()
    {
        cardCredit = maxCardCredit;
        MoveCredit.SetActive(true);
    }

    public UnitCardStat.MoveType GetUnitMoveType()
    {
        return unitCardStat.CurrentMoveType;
    }
}
