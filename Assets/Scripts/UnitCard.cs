using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UnitCard : MonoBehaviour
{
    [SerializeField] private int playerNo;
    [SerializeField] protected int maxCardCredit = 1;
    [SerializeField] private GameObject MoveCredit;

    public UnitCardStat unitCardStat;

    public TextMeshProUGUI attackText;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI manaText;
 
    public int cardCredit;

    public int health { get; set; }
    public int attack { get; set; }
    public int mana { get; set; }

    public SpriteRenderer unitImage;

    public bool isPlayCard;
    public bool isSkillDone;

    public virtual void UnitSkill()
    {
        Debug.Log("Default skill");
    }

    public virtual void UnitHighlight()
    {
        Debug.Log("No highlight");
        isSkillDone = true;
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
