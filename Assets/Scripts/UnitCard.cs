using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UnitCard : MonoBehaviour
{
    [SerializeField] protected int playerNo;
    [SerializeField] protected int maxCardCredit = 1;
    [SerializeField] private GameObject selectMoveCredit;
    [SerializeField] private GameObject moveCredit1;
    [SerializeField] private GameObject moveCredit2;
    [SerializeField] private GameObject edgeImage;


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

    protected GameObject skillTargetUnit;
    protected TileManager tileManager;
    public GameObject backCard;
    protected GameController gameController;

    public virtual void UnitSkill()
    {
        Debug.Log("Default skill");
    }

    public virtual void UnitHighlight()
    {
        Debug.Log("Default highlight");
        tileManager.NoHighlightUnit();
        isSkillDone = true;
    }

    public void SetSkillTarget(GameObject unitTarget)
    {
        skillTargetUnit = unitTarget;
    }

    public GameObject GetSkillTargetUnit()
    {
        return skillTargetUnit;
    }

    public int GetPlayerNo()
    {
        return playerNo;
    }

    public int GetCardCredit()
    {
        return cardCredit;
    }

    public void SelectMoveCredit(int playerNo)
    {
        if(playerNo == 1)
        {
            selectMoveCredit = moveCredit1;
            edgeImage.GetComponent<SpriteRenderer>().color = Color.blue;
            
        }
        else
        {
            selectMoveCredit = moveCredit2;
            edgeImage.GetComponent<SpriteRenderer>().color = Color.red;
        }
        moveCredit1.SetActive(false);
        moveCredit2.SetActive(false);
    }

    public void SetCardCredit(int setCardCredit)
    {
        cardCredit = setCardCredit;
    }

    public void SetPlayerNo(int playerNo)
    {
        this.playerNo = playerNo;
        SelectMoveCredit(playerNo);
    }

    public void ReduceCardCredit()
    {
        if(cardCredit > 0)
        {
            cardCredit -= 1;
            if(cardCredit == 0) selectMoveCredit.SetActive(false);
        }

    }

    public void RefreshCredit()
    {
        cardCredit = maxCardCredit;
        selectMoveCredit.SetActive(true);
    }

    public UnitCardStat.MoveType GetUnitMoveType()
    {
        return unitCardStat.CurrentMoveType;
    }

    public UnitCardStat.AttackType GetUnitAttackType()
    {
        return unitCardStat.CurrentAttackType;
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
        unitInSelectTileCard.TakeDamage(this, damage);
        
    }
    public virtual void TakeDamage(UnitCard attackUnitCard, int damage)
    {
        health -= damage;
        if (health <= 0) Destroy(this.gameObject, 0.5f);
    }

    public void AttackUnit(UnitCard unitAttacked)
    {
        if(unitCardStat.CurrentAttackType == UnitCardStat.AttackType.Melee)
        {
            Debug.Log("Melee Attack");
            MeleeAttack(unitAttacked);
        }
        if(unitCardStat.CurrentAttackType == UnitCardStat.AttackType.Range)
        {
            Debug.Log("Range Attack");
            RangeAttack(unitAttacked);
        }
    }

    public void MeleeAttack(UnitCard unitAttacked)
    {
        unitAttacked.TakeDamage(this, attack);
        TakeDamage(unitAttacked, unitAttacked.attack);

        if (unitAttacked.health <= 0) Destroy(unitAttacked.gameObject, 0.5f);
        if (health <= 0) Destroy(gameObject, 0.5f);
        
        UpdateUICard();
        unitAttacked.UpdateUICard();
    }

    public void RangeAttack(UnitCard unitAttacked)
    {
        unitAttacked.TakeDamage(this, attack);

        if (unitAttacked.health <= 0) Destroy(unitAttacked.gameObject, 0.5f);
        
        unitAttacked.UpdateUICard();
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

    public void UpdateUICard()
    {
        attackText.text = attack.ToString();
        healthText.text = health.ToString();
    }

    public void RemoveBackCard()
    {
        Destroy(backCard);
    }
}
