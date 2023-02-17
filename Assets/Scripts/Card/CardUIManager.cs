using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using Newtonsoft.Json;
using TMPro;

public class CardUIManager : MonoBehaviour
{
    public GameObject cardBody; 

    public RectTransform cardPanel;
    public TextMeshProUGUI pageText;
    public TMP_InputField searchInput;


    private List<Card> cards = new List<Card>();
    private int PAGE_COUNT;
    private int page = 0;
    private int MAX_CARD_PER_PAGE = 8;
    private string searchName;

    private void Start() {
        StartCoroutine(GetCards());
    }

    private IEnumerator GetCards()
    {
        string path = "card";

        var request = Api.CreateRequest(path, "GET");

        yield return request.SendWebRequest();
        if (request.result != UnityWebRequest.Result.ConnectionError && request.result != UnityWebRequest.Result.ProtocolError)
        {
            var json = request.downloadHandler.text;
            cards = new List<Card>(JsonConvert.DeserializeObject<Card[]>(json));
            cards.AddRange(JsonConvert.DeserializeObject<Card[]>(json));
            cards.AddRange(JsonConvert.DeserializeObject<Card[]>(json));
        }
        else 
        {
            Debug.Log(request.result);
        }
        
        UpdatePage();
    }
    
    private void AssignCard(Card card)
    {
        GameObject newCardBody = Instantiate(cardBody);
        CardUI cardUI = newCardBody.GetComponent<CardUI>();

        cardUI.unitName.text = card.unitName;
        cardUI.unitDescription.text = card.unitDescription;
        
        cardUI.cost.text = card.cost.ToString();
        cardUI.atk.text = card.atk.ToString();
        cardUI.hp.text = card.hp.ToString();
        
        newCardBody.transform.SetParent(cardPanel.transform, false);
    }

    public void NextPage()
    {
        page = (page + 1) % PAGE_COUNT;
        UpdatePage();
    }

    public void PreviousPage()
    {
        page = (int)(((page - 1) + Mathf.CeilToInt(cards.Count / (float)MAX_CARD_PER_PAGE))) % (Mathf.CeilToInt(cards.Count / (float)MAX_CARD_PER_PAGE));
        UpdatePage();
    }

    private void ResetCardPanelChildren()
    {
        while (cardPanel.transform.childCount > 0) 
        {
            DestroyImmediate(cardPanel.transform.GetChild(0).gameObject);
        }
    }

    private void RenderCards(List<Card> cards, bool resetPage = true)
    {
        ResetCardPanelChildren();

        if (resetPage)
        {
            ResetPage(cards.Count);
        }

        foreach (Card card in cards
            .Skip(MAX_CARD_PER_PAGE * page)
            .Take(MAX_CARD_PER_PAGE)
        )
        {
            AssignCard(card);
        }
    }

    private void ResetPage(int countCards)
    {
        page = 0;
        PAGE_COUNT = Mathf.Max(Mathf.CeilToInt(countCards / (float)MAX_CARD_PER_PAGE), 1);
        pageText.text = (page + 1).ToString() + "/" + PAGE_COUNT.ToString();
    }

    public void SearchByInput()
    {
        searchName = searchInput.text;

        if (searchName == "")
        {
            UpdatePage();
        }
        else
        {
            List<Card> searchCards = cards
                .Where(w => w.unitName
                    .ToUpper()
                    .Contains(searchName
                        .ToUpper()
                    )
                )
                .ToList();
            
            RenderCards(searchCards);
        }
    }

    public void SearchByMana(int _mana)
    {
        List<Card> manaCards = cards
            .Where(w => w.cost == _mana)
            .ToList();

        RenderCards(manaCards);
    }

    public void SearchByClass(string _class)
    {
        List<Card> classCards = cards
            .Where(w => w.className == _class)
            .ToList();

        RenderCards(classCards);
    }

    private void UpdatePage()
    {
        RenderCards(cards, false);

        PAGE_COUNT = Mathf.Max(Mathf.CeilToInt(cards.Count / (float)MAX_CARD_PER_PAGE), 1);
        pageText.text = (page + 1).ToString() + "/" + (PAGE_COUNT).ToString();
    }
}
