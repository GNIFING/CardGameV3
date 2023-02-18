using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCard : MonoBehaviour
{
    [SerializeField] List<GameObject> unitCardPrefabs;
    private List<GameObject> unitTiles = new List<GameObject>();
    private List<GameObject> magicTiles = new List<GameObject>();

    private void Start()
    {
        AddTilesToList();
    }

    private void AddTilesToList()
    {
        GameObject playerUnitTile = transform.GetChild(0).gameObject;
        GameObject playerMagicTile = transform.GetChild(1).gameObject;
        for (int i = 0; i < playerUnitTile.transform.childCount; i++)
        {
            unitTiles.Add(playerUnitTile.transform.GetChild(i).gameObject);
        }
        for (int i = 0; i < playerMagicTile.transform.childCount; i++)
        {
            unitTiles.Add(playerMagicTile.transform.GetChild(i).gameObject);
        }
    }

    public void SpawnUnit()
    {
        foreach (GameObject tile in unitTiles)
        {
            bool isFoundUnit = false;
            foreach(Transform tr in tile.transform)
            {
                if (tr.CompareTag("Unit"))
                {
                    isFoundUnit = true;
                }
            }
            if (!isFoundUnit)
            {
                int index = Random.Range(0, unitCardPrefabs.Count);
                GameObject unitCard = Instantiate(unitCardPrefabs[index], tile.transform.position, Quaternion.identity);
                unitCard.transform.parent = tile.transform;
                return;
            }
        }
    }
}
