using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;

public class SpawnCard : MonoBehaviour
{
    public int playerNo;
    [SerializeField] List<GameObject> unitCardPrefabs;
    public DeckController deckController;

    private List<GameObject> cardTiles = new List<GameObject>();
    private List<Card> cards = new();
    private List<int> cardIds = new();

    private void Start()
    {
        StartCoroutine(deckController.GetDeck(11, (responseData) =>
        {
            cards.AddRange(new List<Card>(JsonConvert.DeserializeObject<Card[]>(responseData)));
            cardIds = cards.Select(s => s.id).ToList();
        }));

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
        Debug.Log(cardIds.Count);
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
                unitCard.GetComponent<UnitCard>().RefreshCredit();
                return;
            }
        }
    }

    public void InitialSpawn(bool isShowCard)
    {
        foreach (GameObject tile in cardTiles)
        {
            bool isFoundUnit = false;
            foreach (Transform tr in tile.transform)
            {
                if (tr.CompareTag("Unit"))
                {
                    isFoundUnit = true;
                }
            }
            if (!isFoundUnit)
            {
                int index = Random.Range(0, unitCardPrefabs.Count);
                GameObject unitCard = Instantiate(unitCardPrefabs[unitCardPrefabs.Count-1], tile.transform.position, Quaternion.identity);
                unitCard.transform.parent = tile.transform;
                unitCard.GetComponent<UnitCard>().SetPlayerNo(playerNo);
                unitCard.GetComponent<UnitCard>().RefreshCredit();
                unitCard.GetComponent<UnitCard>().SetBackCard(isShowCard);
                return;
            }
        }
    }
}
