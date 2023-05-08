using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;

public class CardUIManager2 : MonoBehaviour
{
    public CardController cardController;
    public DeckController deckController;
    public GameObject cardBody;
    public RectTransform cardPanel;

    protected List<UserCard> decks = new();

    private IEnumerator Start()
    {
        yield return StartCoroutine(deckController.GetDeck(13, (responseData) =>
        {
            Debug.Log(responseData.ToString());
            //decks = new(JsonConvert.DeserializeObject<Deck[]>(responseData));
            
            UpdatePage();
        }));
    }

    public void UpdatePage()
    {
        //RenderDecks(decks, false);
    }

    private void RenderDecks(List<Deck> decks, bool resetPage = true)
    {
        ResetCardPanelChildren();

        if (resetPage)
        {
            //ResetPage(cards.Count);
        }

        foreach (Deck deck in decks
            //.Skip(MAX_CARD_PER_PAGE * page)
            //.Take(MAX_CARD_PER_PAGE)
        )
        {
            RenderDeck(deck);
        }
    }
    private void ResetCardPanelChildren()
    {
        while (cardPanel.transform.childCount > 0)
        {
            DestroyImmediate(cardPanel.transform.GetChild(0).gameObject);
        }
    }

    private void RenderDeck(Deck deck)
    {
        GameObject newCardBody = Instantiate(cardBody);
        DeckUI deckUI = newCardBody.GetComponent<DeckUI>();

        deckUI.Name.text = deck.Name;
        deckUI.Id.text = $"Deck {deck.Id}: ({deck.UserCards.Count()} cards)";

        newCardBody.transform.SetParent(cardPanel.transform, false);
    }
}
