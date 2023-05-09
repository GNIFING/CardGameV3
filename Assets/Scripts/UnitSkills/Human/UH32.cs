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
        TakeDamage(unitAttacked, unitAttacked.GetAttackDamage());
        unitAttacked.TakeDamage(this, attack);
        MeleeAttackAnimation(unitAttacked);

        if (unitAttacked.GetHealth() <= 0) 
        {
            summonTile = unitAttacked.GetComponentInParent<Tile>();
            Destroy(unitAttacked.gameObject, 0.5f);
            StartCoroutine(SummonWarrior(1f));

        }
        if (health <= 0) Destroy(gameObject, 0.5f);

        UpdateCardUI();
        unitAttacked.UpdateCardUI();
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
    }

    public override void UnitHighlight()
    {
        isSkillDone = true;
        tileManager.NoHighlightUnit();
        Debug.Log("Highlight from unit 32");
    }
}
