using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_4 : UnitCard
{


    // Start is called before the first frame update
    void Start()
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

    // Update is called once per frame
    void Update()
    {

    }

    public override void UnitSkill()
    {
        Debug.Log("Unit 4 Skill !");
        isSkillDone = true;
    }
}