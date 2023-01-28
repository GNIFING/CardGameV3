using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    private List<GameObject> Tiles { get; set; } = new List<GameObject>();
    public GameObject ActiveUnit { get; private set; }

    private void Start()
    {
        AddTilesToList();
    }

    private void AddTilesToList()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Tiles.Add(transform.GetChild(i).gameObject);
        }
    }

    public List<GameObject> GetAllTiles() => Tiles;

    public void SetActiveUnit(GameObject unit)
    {
        ActiveUnit = unit;
    }

    public GameObject GetActiveUnit() => ActiveUnit;
}