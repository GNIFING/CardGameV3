using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using Newtonsoft.Json;
using TMPro;
using Unity.VisualScripting;

public class CardUIManager : MonoBehaviour
{
    public GameObject cardBody; 

    public RectTransform cardPanel;
    public TextMeshProUGUI pageText;
    public TMP_InputField searchInput;
    public TMP_Dropdown deckDropdown;
    public TMP_InputField newDeckNameInput;
    public DeckController deckController;
    public CardController cardController;

    protected List<Card> cards = new();
    protected List<Deck> decks = new();
    protected List<DeckItem> deckItems = new();

    private List<UserCard> userCards = new();
    private int PAGE_COUNT;
    private int page = 0;
    private readonly int MAX_CARD_PER_PAGE = 8;
    private string searchName;

    private IEnumerator Start() {
        yield return StartCoroutine(deckController.GetDecks((responseData) =>
        {
            List<Deck> decks = new(JsonConvert.DeserializeObject<Deck[]>(responseData));

            deckItems = new()
            {
                // ---------- Add placeholder at the start ---------- //        
                new DeckItem() { id = 0, name = "Choose" }
            };

            deckItems.AddRange(decks.Select(s => new DeckItem() { id = s.id, name = s.name }).ToList());

            // -------- Add deckItems to dropdown ---------- //
            deckDropdown.GetComponent<TMP_Dropdown>().AddOptions(deckItems.Select(s => s.name.ToString()).ToList());
        }));

        yield return StartCoroutine(cardController.GetCards((responseData) =>
        {
            userCards = new List<UserCard>(JsonConvert.DeserializeObject<UserCard[]>(responseData));

            // ---------- Clear card panel data ---------- //
            cards.Clear();

            cards.AddRange(userCards.Select(s => s.card));

            UpdatePage();
        }));
    }

    public IEnumerable<int> GetCardIdsFromDeckId(int deckId)
    {
        StartCoroutine(deckController.GetDeck(deckId, (responseData) =>
        {
            cards.AddRange(new List<Card>(JsonConvert.DeserializeObject<Card[]>(responseData)));

            UpdatePage();
            AssignDeckId(deckId);
        }));

        return cards.Select(s => s.id);
    }

    public void OnDropdownChange()
    {
        // ---------- Clear card panel data ---------- //
        cards.Clear();

        // ---------- Get deck id from dropdown options from value index from deckItems ---------- //
        int deckId = deckItems.First(f => f.name == deckDropdown.options[deckDropdown.value].text).id;

        // ---------- If value equals "Select deck", do nothing ---------- //
        if (deckId == 0)
        {
            StartCoroutine(cardController.GetCards((responseData) =>
            {
                userCards = new List<UserCard>(JsonConvert.DeserializeObject<UserCard[]>(responseData));

                cards.AddRange(userCards.Select(s => s.card));

                UpdatePage();
            }));
        }

        // ---------- If not, get deck by id from dropdown value ---------- //
        else
        {
            StartCoroutine(deckController.GetDeck(deckId, (responseData) =>
            {
                cards.AddRange(new List<Card>(JsonConvert.DeserializeObject<Card[]>(responseData)));

                UpdatePage();
                AssignDeckId(deckId);
            }));
        }
    }

    public void OnClickCreateDeck()
    {
        string necDeckName = newDeckNameInput.text;

        StartCoroutine(deckController.AddDeck(necDeckName, (responseData) =>
        {
            CreateDeckResponse response = JsonConvert.DeserializeObject<CreateDeckResponse>(responseData);

            // ---------- Clear and add new cards from the new deck to cardPanel ---------- //
            cards.Clear();
            cards.AddRange(response.cards.Select(s => s));

            // ---------- Add new deck to decks ---------- //
            deckItems.Add(new DeckItem() { id = response.id, name = response.name });

            // -------- Clear and add deckItems to dropdown ---------- //
            deckDropdown.GetComponent<TMP_Dropdown>().ClearOptions();
            deckDropdown.GetComponent<TMP_Dropdown>().AddOptions(deckItems.Select(s => s.name.ToString()).ToList());
            deckDropdown.value = deckDropdown.options.Count - 1;

            UpdatePage();
        }));
    }

    private void AssignCard(Card card)
    {
        GameObject newCardBody = Instantiate(cardBody);
        CardUI cardUI = newCardBody.GetComponent<CardUI>();

        cardUI.id = card.id;
        cardUI.unitName.text = card.name;
        cardUI.unitDescription.text = card.description;

        StartCoroutine(DownloadImage(card.imageUri, cardUI));
        
        cardUI.cost.text = card.cost.ToString();
        cardUI.atk.text = card.atk.ToString();
        cardUI.hp.text = card.hp.ToString();

        cardUI.cardUIManager = this;

        if (deckDropdown.value == 0)
        {
            cardUI.removeCardButton.gameObject.SetActive(false);
            cardUI.deckDropdown.gameObject.SetActive(true);
            cardUI.deckDropdown.GetComponent<TMP_Dropdown>().AddOptions(deckItems.Select(s => s.name.ToString()).ToList());
        }
        else
        {
            cardUI.removeCardButton.gameObject.SetActive(true);
            cardUI.deckDropdown.gameObject.SetActive(false);
        }

        newCardBody.transform.SetParent(cardPanel.transform, false);
    }

    public void AssignDeckId(int deckId)
    {
        foreach (Transform child in cardPanel.transform)
        {
            CardUI childCardUI = child.gameObject.GetComponent<CardUI>();
            childCardUI.deckId = deckId;
        }
    }
    
    IEnumerator DownloadImage(string MediaUrl, CardUI cardUI)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(Api.s3Prefix + MediaUrl + ".png");
        yield return request.SendWebRequest();
        if (request.result != UnityWebRequest.Result.ConnectionError && request.result != UnityWebRequest.Result.ProtocolError)
        {
            if (cardUI != null && !cardUI.gameObject.IsDestroyed())
            {
                Texture2D webTexture = ((DownloadHandlerTexture)request.downloadHandler).texture as Texture2D;
                Sprite webSprite = SpriteFromTexture2D(webTexture);
                cardUI.cardImage.GetComponent<Image>().sprite = webSprite;
            }
        }
        else
        {
            Debug.Log(request.result);
        }
    }
	
	Sprite SpriteFromTexture2D(Texture2D texture) {
		return Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100.0f);
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
                .Where(w => w.name
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

    public void UpdatePage()
    {
        RenderCards(cards, false);

        PAGE_COUNT = Mathf.Max(Mathf.CeilToInt(cards.Count / (float)MAX_CARD_PER_PAGE), 1);
        pageText.text = (page + 1).ToString() + "/" + (PAGE_COUNT).ToString();
    }

    public List<DeckItem> GetDeckItems()
    {
        return deckItems;
    }

    public List<Card> GetCards()
    {
        return cards;
    }
}
