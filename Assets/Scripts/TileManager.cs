using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    private List<GameObject> tiles = new List<GameObject>();

    private void Start()
    {
        AddTilesToList();
        foreach (var tile in tiles)
        {
            Debug.Log(tile.name);
        }
    }

    private void AddTilesToList()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            tiles.Add(transform.GetChild(i).gameObject);
        }
    }

    public List<GameObject> GetAllTiles()
    {
        return tiles;
    }

}
