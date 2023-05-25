using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;

public class SpawnCard : MonoBehaviour
{
    public int playerNo;
    [SerializeField] List<GameObject> unitCardPrefabs;
    public bool isLoading = false;

    private List<GameObject> cardTiles = new List<GameObject>();
    private List<Card> cards = new();
    private List<int> cardIds = new();

    public GameController gameController;
    public MultiPlayerController multiPlayerController;

    private void Start()
    {
        string saveFile = Application.persistentDataPath + "/deckId.json";
        if (File.Exists(saveFile))
        {
            // Read the entire file and save its contents.
            string fileContents = File.ReadAllText(saveFile);

            // Work with JSON
            //Debug.Log("content " + fileContents);
            
            // ---------- Get cards in deck by deckId ---------- //
            //StartCoroutine(deckController.GetDeck(int.Parse(fileContents), (deck) =>
            //{
            //    cardIds = cards.Select(s => s.id).ToList();
            //    isLoading = true;
            //}));
        }

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
        //foreach (GameObject tile in cardTiles)
        //{
        //    bool isFoundUnit = false;
        //    foreach(Transform tr in tile.transform)
        //    {
        //        if (tr.CompareTag("Unit"))
        //        {
        //            isFoundUnit = true;
        //        }
        //    }
        //    if (!isFoundUnit)
        //    {
        //        int index = Random.Range(0, unitCardPrefabs.Count);
        //        GameObject unitCard = Instantiate(unitCardPrefabs[index], tile.transform.position, Quaternion.identity);
        //        unitCard.transform.parent = tile.transform;
        //        unitCard.GetComponent<UnitCard>().SetPlayerNo(playerNo);
        //        unitCard.GetComponent<UnitCard>().RefreshCredit();
        //        return;
        //    }
        //}
        int arenaId = PlayerPrefs.GetInt("ArenaId");
        int playerId = gameController.playerId;

        StartCoroutine(DrawCardWithDelay(2f, arenaId, playerId));
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
                //int index = Random.Range(0, unitCardPrefabs.Count);
                //GameObject unitCard = Instantiate(unitCardPrefabs[unitCardPrefabs.Count-1], tile.transform.position, Quaternion.identity);
                //unitCard.transform.parent = tile.transform;
                //unitCard.GetComponent<UnitCard>().SetPlayerNo(playerNo);
                //unitCard.GetComponent<UnitCard>().RefreshCredit();
                //unitCard.GetComponent<UnitCard>().SetBackCard(isShowCard);
                //return;

                int index = Random.Range(0, cards.Count);
                int targetIndex = cardIds.ElementAt(index) - 1;
                GameObject unitCard = Instantiate(unitCardPrefabs[targetIndex], tile.transform.position, Quaternion.identity);
                unitCard.transform.parent = tile.transform;
                unitCard.GetComponent<UnitCard>().SetPlayerNo(playerNo);
                unitCard.GetComponent<UnitCard>().RefreshCredit();
                return;
            }
        }
    }

    private IEnumerator DrawCardWithDelay(float delay, int arenaId, int playerId)
    {
        yield return new WaitForSeconds(delay);
        StartCoroutine(
                multiPlayerController.DrawCard(arenaId, playerId, (response) => {
                    Debug.Log("Api: Draw Card Player " + playerId);
                })
        );
    }
}
