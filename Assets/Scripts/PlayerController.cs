using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] GameObject unitCardPrefab;
    private GameObject[] units;
    private List<GameObject> Tiles = new List<GameObject>();

    private void Start()
    {
        AddTilesToList();
    }

    private void AddTilesToList()
    {
        GameObject playerTile = transform.GetChild(0).gameObject;
        for (int i = 0; i < playerTile.transform.childCount; i++)
        {
            Tiles.Add(playerTile.transform.GetChild(i).gameObject);
        }
    }

    public void SpawnUnit()
    {
        foreach (GameObject tile in Tiles)
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
                GameObject unitCard = Instantiate(unitCardPrefab, tile.transform.position, Quaternion.identity);
                unitCard.transform.parent = tile.transform;
                UnitCard unCard = unitCard.GetComponent<UnitCard>();
                unCard.health = Random.Range(2, 6);
                unCard.attack = Random.Range(2, 6);
                break;
            }
        }
    }
}
