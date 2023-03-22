using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowMana : MonoBehaviour
{
    public List<SpriteRenderer> manaImage;

    public void SetManaColor(int mana, int maxMana)
    {
        for (int i = 0; i < maxMana; i++)
        {
            if(i < mana)
            {
                manaImage[i].color = new Color(255, 200, 0);
            }
            else
            {
                manaImage[i].color = Color.black;

            }
        }
    }
}
