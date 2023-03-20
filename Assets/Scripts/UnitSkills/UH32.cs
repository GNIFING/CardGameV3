using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UH32 : UnitCard
{
    [SerializeField]
    private GameObject UH32_1_Prefab;

    private Tile unitTile;
    private Tile summonTile;

    void Start()
    {
        InitializeCardStats();
        UpdateCardUI();
    }

    public override void MeleeAttack(UnitCard unitAttacked)
    {
        TakeDamage(unitAttacked, unitAttacked.attack);
        unitAttacked.TakeDamage(this, attack);

        if (unitAttacked.health <= 0) 
        {
            summonTile = unitAttacked.GetComponentInParent<Tile>();
            Destroy(unitAttacked.gameObject, 0.5f);
            StartCoroutine(SummonWarrior(1f));

        }
        if (health <= 0) Destroy(gameObject, 0.5f);

        UpdateUICard();
        unitAttacked.UpdateUICard();
    }

    IEnumerator SummonWarrior(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        GameObject UH32_1 = Instantiate(UH32_1_Prefab, summonTile.transform.position, Quaternion.identity);
        UH32_1.transform.parent = summonTile.transform;
        UnitCard UH32_1Card = UH32_1.GetComponent<UnitCard>();
        UH32_1Card.SetPlayerNo(playerNo);
        UH32_1Card.isPlayCard = true;
        UH32_1Card.isSkillDone = true;
        UH32_1Card.RemoveBackCard();
    }

    public override void UnitHighlight()
    {
        isSkillDone = true;
        tileManager.NoHighlightUnit();
        Debug.Log("Highlight from unit 32");
    }
}
