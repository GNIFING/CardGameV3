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
    [SerializeField] private GameObject rangeIcon;
    [SerializeField] private GameObject meleeIcon;
    [SerializeField] private GameObject swordAnimationPrefab1;
    [SerializeField] private GameObject swordAnimationPrefab2;

    [SerializeField] protected GameObject bulletPrefab;

    public UnitCardStat unitCardStat;

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI attackText;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI manaText;
    public TextMeshProUGUI descriptionText;

    public MultiPlayerController multiPlayerController;
    public int cardCredit;

    protected int health;
    protected int attack;
    public int mana { get; set; }
    public int userCardId;
    protected int id;
    protected string cardIndex;

    public string description { get; set; }

    public SpriteRenderer unitImage;

    public bool isPlayCard;
    public bool isSkillDone;

    protected GameObject skillTargetUnit;
    protected TileManager tileManager;
    public GameObject backCard;
    public ChooseMoveType chooseMoveType;
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
        Debug.Log("Reduce");
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
        Vector2 direction;
        if (unitInSelectTile.CompareTag("Unit"))
        {
            direction = unitInSelectTile.transform.parent.transform.position - transform.position;
        }
        else
        {
            direction = unitInSelectTile.transform.transform.position - transform.position;
        }
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        return Quaternion.AngleAxis(angle, Vector3.forward);
    }

    protected void InitializeCardStats()
    {
        gameController = FindObjectOfType<GameController>();
        tileManager = GameObject.Find("Tiles").GetComponent<TileManager>();
        unitImage.sprite = unitCardStat.CardImage;
        nameText.text = unitCardStat.CardName;
        health = unitCardStat.Hp;
        attack = unitCardStat.AttackDamage;
        mana = unitCardStat.ManaCost;
        id = unitCardStat.Id;
        cardIndex = unitCardStat.CardIndex;
        description = unitCardStat.CardDescription;
        cardCredit = maxCardCredit;
        chooseMoveType.SetMoveTypeImage();

        if (unitCardStat.CurrentAttackType == UnitCardStat.AttackType.Melee)
        {
            meleeIcon.SetActive(true);
            rangeIcon.SetActive(false);
        }
        else
        {
            meleeIcon.SetActive(false);
            rangeIcon.SetActive(true);
        }
    }

    //------------------------- Card Index ---------------------------//
    public int GetUserCardId()
    {
        return userCardId;
    }

    public void SetUserCardId(int newUserCardId)
    {
        userCardId = newUserCardId;
    }

    public int GetId()
    {
        return id;
    }

    public string GetCardIndex()
    {
        return cardIndex;
    }
    
    //-------------------- Increase-Decrease Stat --------------------//
    public virtual void TakeDamage(UnitCard attackUnitCard, int damage)
    {
        DecreaseHealth(damage);
        UpdateCardUI();
        //if (health <= 0) Destroy(this.gameObject, 0.5f);
    }

    public void IncreaseHealth(int plusHealth, bool callApi = true)
    {
        //----------- SEND API ------------//
        gameController = FindObjectOfType<GameController>();

        int arenaId = gameController.arenaId;
        multiPlayerController = FindObjectOfType<MultiPlayerController>();
        Tile tileOnThisUnit = GetComponentInParent<Tile>();
        int tileIndex = tileOnThisUnit.ConvertTilePosToIndex(tileOnThisUnit.GetXPos(), tileOnThisUnit.GetYPos());
        Debug.Log("increase health");
        if (callApi == true)
        {
            StartCoroutine(multiPlayerController.UpdateCard(arenaId, tileIndex, plusHealth, 0, (response) => { }));
        }
        //----------- SEND API ------------//
        health += plusHealth;
        UpdateCardUI();
    }

    public void DecreaseHealth(int minusHealth, bool callApi = true)
    {
        health -= minusHealth;
        //----------- SEND API ------------//
        gameController = FindObjectOfType<GameController>();

        int arenaId = gameController.arenaId;
        multiPlayerController = FindObjectOfType<MultiPlayerController>();
        Tile tileOnThisUnit = GetComponentInParent<Tile>();
        int tileIndex = tileOnThisUnit.ConvertTilePosToIndex(tileOnThisUnit.GetXPos(), tileOnThisUnit.GetYPos());

        if (callApi == true)
        {
            StartCoroutine(multiPlayerController.UpdateCard(arenaId, tileIndex, -minusHealth, 0, (response) => { Debug.Log("Update Card Done"); }));
        }
        Debug.Log("decrease health - " + minusHealth + "Passed");
        //----------- SEND API ------------//
        
        if (health <= 0) Destroy(this.gameObject, 0.5f);

    }

    public void SetHealth(int newHealth, bool callApi = true)
    {
        //----------- SEND API ------------//
        gameController = FindObjectOfType<GameController>();

        int arenaId = gameController.arenaId;
        multiPlayerController = FindObjectOfType<MultiPlayerController>();
        Tile tileOnThisUnit = GetComponentInParent<Tile>();
        int tileIndex = tileOnThisUnit.ConvertTilePosToIndex(tileOnThisUnit.GetXPos(), tileOnThisUnit.GetYPos());
        Debug.Log("set health");
        if (callApi == true)
        {
            StartCoroutine(multiPlayerController.UpdateCard(arenaId, tileIndex, newHealth - health, 0, (response) => { }));
        }
        //----------- SEND API ------------//
        health = newHealth;
        UpdateCardUI();
        if (health <= 0) Destroy(this.gameObject, 0.5f);
    }
    public void IncreaseAttackDamage(int plusAttack, bool callApi = true)
    {
        //----------- SEND API ------------//
        gameController = FindObjectOfType<GameController>();

        int arenaId = gameController.arenaId;
        multiPlayerController = FindObjectOfType<MultiPlayerController>();
        Tile tileOnThisUnit = GetComponentInParent<Tile>();
        int tileIndex = tileOnThisUnit.ConvertTilePosToIndex(tileOnThisUnit.GetXPos(), tileOnThisUnit.GetYPos());
        //Debug.Log("tileIndex = " + tileIndex);
        //Debug.Log("plusAttack = " + plusAttack);
        Debug.Log("increase attack");
        if (callApi == true)
        {
            StartCoroutine(multiPlayerController.UpdateCard(arenaId, tileIndex, 0, plusAttack, (response) => { Debug.Log(response.atk); }));
        }
        //----------- SEND API ------------//
        attack += plusAttack;
        UpdateCardUI();
    }

    public void DecreaseAttackDamage(int minusAttack, bool callApi = true)
    {
        
        if(attack <= 0)
        {
            attack = 0;
        }
        //UpdateCardUI();
        //----------- SEND API ------------//
        gameController = FindObjectOfType<GameController>();

        int arenaId = gameController.arenaId;
        multiPlayerController = FindObjectOfType<MultiPlayerController>();
        Tile tileOnThisUnit = GetComponentInParent<Tile>();
        int tileIndex = tileOnThisUnit.ConvertTilePosToIndex(tileOnThisUnit.GetXPos(), tileOnThisUnit.GetYPos());
        Debug.Log("decrease attack");
        if(minusAttack - attack >= 0 && callApi == true)
        {
            StartCoroutine(multiPlayerController.UpdateCard(arenaId, tileIndex, 0, -attack, (response) => { }));
        }

        else if (callApi == true)
        {
            StartCoroutine(multiPlayerController.UpdateCard(arenaId, tileIndex, 0, -minusAttack, (response) => { }));
        }
        //----------- SEND API ------------//
        attack -= minusAttack;
        UpdateCardUI();
    }

    public void SetAttackDamage(int newAttackDamage, bool callApi = true)
    {
        //----------- SEND API ------------//
        gameController = FindObjectOfType<GameController>();

        //Debug.Log("gameController = " + gameController);
        int arenaId = gameController.arenaId;
        multiPlayerController = FindObjectOfType<MultiPlayerController>();
        Tile tileOnThisUnit = GetComponentInParent<Tile>();
        int tileIndex = tileOnThisUnit.ConvertTilePosToIndex(tileOnThisUnit.GetXPos(), tileOnThisUnit.GetYPos());
        Debug.Log("set attack");
        if (callApi == true)
        {
            StartCoroutine(multiPlayerController.UpdateCard(arenaId, tileIndex, 0, newAttackDamage - attack, (response) => { }));
        }
        //----------- SEND API ------------//
        attack = newAttackDamage;
        UpdateCardUI();
    }

    public int GetHealth()
    {
        return health;
    }

    public int GetAttackDamage()
    {
        return attack;
    }

    public void UpdateCardUI()
    {
        //Debug.Log("attack damage " + id +" = " + attack);
        attackText.text = attack.ToString();
        healthText.text = health.ToString();
        manaText.text = mana.ToString();
        descriptionText.text = description;
    }


    //-------------------- Attack Unit --------------------//
    public void AttackUnit(UnitCard unitAttacked)
    {
        //----------- SEND API ------------//
        int arenaId = gameController.arenaId;
        Tile attackerTile = this.GetComponentInParent<Tile>();
        int attackerIndex = attackerTile.ConvertTilePosToIndex(attackerTile.GetXPos(), attackerTile.GetYPos());
        Tile defenderTile = unitAttacked.GetComponentInParent<Tile>();
        int defenderIndex = defenderTile.ConvertTilePosToIndex(defenderTile.GetXPos(), defenderTile.GetYPos());
        multiPlayerController = FindObjectOfType<MultiPlayerController>();
        StartCoroutine(multiPlayerController.AttackCard(arenaId, attackerIndex, defenderIndex, (response) => { Debug.Log("Attack Card Done"); }));
        //----------- SEND API ------------//
        if (unitCardStat.CurrentAttackType == UnitCardStat.AttackType.Melee)
        {
            Debug.Log("Melee Attack");
            StartCoroutine(MeleeAttack(unitAttacked));
        }
        if(unitCardStat.CurrentAttackType == UnitCardStat.AttackType.Range)
        {
            Debug.Log("Range Attack");
            RangeAttack(unitAttacked);
        }
    }

    public virtual IEnumerator MeleeAttack(UnitCard unitAttacked)
    {
        TakeDamage(unitAttacked, unitAttacked.attack);
        yield return new WaitForSeconds(0.3f);
        unitAttacked.TakeDamage(this, attack);

        //MeleeAttackAnimation(unitAttacked);
    }

    public void MeleeAttackAnimation(UnitCard unitAttacked)
    {
        if(unitAttacked == null)
        {
            if(transform.position.x <= 1)
            {
                GameObject swordAnimation2 = Instantiate(swordAnimationPrefab2, transform.position, Quaternion.identity);
            }
            else
            {
                GameObject swordAnimation1 = Instantiate(swordAnimationPrefab2, transform.position, Quaternion.identity);
            }
        }
        else if(transform.position.x <= unitAttacked.transform.position.x)
        {
            GameObject swordAnimation1 = Instantiate(swordAnimationPrefab1, transform.position, Quaternion.identity);
            GameObject swordAnimation2 = Instantiate(swordAnimationPrefab2, unitAttacked.transform.position, Quaternion.identity);
            Debug.Log("case 1");
        }
        else
        {
            Debug.Log("case 2");

            GameObject swordAnimation2 = Instantiate(swordAnimationPrefab2, transform.position, Quaternion.identity);
            GameObject swordAnimation1 = Instantiate(swordAnimationPrefab1, unitAttacked.transform.position, Quaternion.identity);
        }
        
    }

    public virtual void RangeAttack(UnitCard unitAttacked)
    {
        unitAttacked.TakeDamage(this, attack);
        //RangeAttackAnimation(unitAttacked.gameObject);
    }


    public void RangeAttackAnimation(GameObject unitAttacked)
    {
        Quaternion rotation = CalculateRotation(unitAttacked);
        GameObject bullet = Instantiate(bulletPrefab, transform.position, rotation);

        if (unitAttacked.CompareTag("Unit"))
        {
            bullet.GetComponent<BulletScript>().SetTarget(unitAttacked.transform.parent.gameObject);
        }
        else
        {
            bullet.GetComponent<BulletScript>().SetTarget(unitAttacked.transform.gameObject);
        }

    }
    //-------------------- End-Start Turn Skill --------------------//
    public virtual void EndTurnSkill()
    {
        Debug.Log("use end turn skill!");
    }

    public virtual void StartTurnSkill()
    {
        Debug.Log("use start turn skill!");
    }

    //-------------------- Back Card --------------------//

    public void SetBackCard(bool isActive)
    {
        backCard.SetActive(isActive);
    }

}
