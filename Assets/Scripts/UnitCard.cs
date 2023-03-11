using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UnitCard : MonoBehaviour
{
    [SerializeField] protected int playerNo;
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

    protected TileManager tileManager;
    public GameObject backCard;
    protected GameController gameController;

    public virtual void UnitSkill(GameObject unitInSelectTile, int tileXPos, int tileYPos )
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

    protected Quaternion CalculateRotation(GameObject unitInSelectTile)
    {
        Vector2 direction = unitInSelectTile.transform.parent.transform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        return Quaternion.AngleAxis(angle, Vector3.forward);
    }

    protected void InitializeCardStats()
    {
        gameController = FindObjectOfType<GameController>();
        gameController.OnChangeCardBack += ChangeBackCard;

        tileManager = GameObject.Find("Tiles").GetComponent<TileManager>();
        unitImage.sprite = unitCardStat.CardImage;
        health = unitCardStat.Hp;
        attack = unitCardStat.AttackDamage;
        mana = unitCardStat.ManaCost;
        cardCredit = maxCardCredit;
    }
    protected void UpdateCardUI()
    {
        attackText.text = attack.ToString();
        healthText.text = health.ToString();
        manaText.text = mana.ToString();
    }
    protected void DealDamageToUnit(GameObject unitInSelectTile, int damage)
    {
        UnitCard unitInSelectTileCard = unitInSelectTile.GetComponent<UnitCard>();
        unitInSelectTileCard.health -= damage;
        unitInSelectTileCard.healthText.text = unitInSelectTileCard.health.ToString();
        if (unitInSelectTileCard.health <= 0) Destroy(unitInSelectTile);
    }

    protected void ChangeBackCard(int playerTurn)
    {
        if(backCard != null)
        {
            if (playerNo == 1)
            {
                backCard.SetActive(playerTurn == 2);
            }
            if (playerNo == 2)
            {
                backCard.SetActive(playerTurn == 1);
            }
        }
        
    }

    public void RemoveBackCard()
    {
        Destroy(backCard);
    }

}
