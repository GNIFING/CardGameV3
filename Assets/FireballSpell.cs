using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballSpell : MagicCard
{
    // Override the Play method to implement the Fireball spell
    private void Start()
    {
        tileManager = GameObject.Find("Tiles").GetComponent<TileManager>();
        highlightUnitType = HighlightUnitType.FriendlyOnly;
    }

    void OnMouseDown()
    {
        Play();
        Debug.Log("MouseDown Spell");
    }

    public override void Play()
    {
        // Select two game objects to target
        HighlightUnits(HighlightUnitType.FriendlyOnly, 1);
    }
}