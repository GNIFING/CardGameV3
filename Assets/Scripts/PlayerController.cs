using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] GameObject unitCardPrefab;
    private GameObject[] units;
    private List<GameObject> tiles = new List<GameObject>();

    private void Start()
    {
        AddTilesToList();
        foreach (var tile in tiles)
        {
            //Debug.Log(tile.name);
        }
    }

    private void AddTilesToList()
    {
        GameObject playerTile = transform.GetChild(0).gameObject;
        for (int i = 0; i < playerTile.transform.childCount; i++)
        {
            tiles.Add(playerTile.transform.GetChild(i).gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnUnit()
    {
        foreach (GameObject tile in tiles)
        {
            bool isFoundUnit = false;
            Debug.Log(tile.name);
            foreach(Transform tr in tile.transform)
            {
                Debug.Log("tr = " + tr);
                if (tr.CompareTag("Unit"))
                {
                    Debug.Log("Already Have Unit Here");
                    isFoundUnit = true;
                }
            }
            if (!isFoundUnit)
            {
                GameObject unitCard = Instantiate(unitCardPrefab, tile.transform.position, Quaternion.identity);
                unitCard.transform.parent = tile.transform;
                break;
            }
        }
    }
}
