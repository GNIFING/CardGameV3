using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCard : MonoBehaviour
{
    public int playerNo;
    [SerializeField] List<GameObject> unitCardPrefabs;
    private List<GameObject> cardTiles = new List<GameObject>();

    private void Start()
    {
        AddTilesToList();
    }

    private void AddTilesToList()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            cardTiles.Add(transform.GetChild(i).gameObject);
        }
    }

    public void SpawnUnit()
    {
        foreach (GameObject tile in cardTiles)
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
                unitCard.GetComponent<UnitCard>().SetPlayerNo(playerNo);
                return;
            }
        }
    }
}
