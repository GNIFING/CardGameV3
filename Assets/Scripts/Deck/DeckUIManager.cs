using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class DeckUIManager : MonoBehaviour
{
    public DeckController deckController;
    public GameObject deckBody;
    public RectTransform deckPanel;
    public TMP_InputField newDeckInput;
    public TMP_Dropdown classDropdown;

    protected List<Deck> decks = new();

    private IEnumerator Start()
    {
        yield return StartCoroutine(deckController.GetDecks((decks) =>
        {
            this.decks = decks;
            
            UpdatePage();
        }));
    }

    public void AddDeck()
    {
        string name = newDeckInput.text;
        string className = classDropdown.options[classDropdown.value].text;

        StartCoroutine(deckController.AddDeck(name, className, (deck) =>
        {
            this.decks.Add(deck);

            UpdatePage();
        }));
    }

    public void UpdatePage()
    {
        RenderDecks(decks, false);
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
        while (deckPanel.transform.childCount > 0)
        {
            DestroyImmediate(deckPanel.transform.GetChild(0).gameObject);
        }
    }

    private void RenderDeck(Deck deck)
    {
        GameObject newDeckBody = Instantiate(deckBody);
        DeckUI deckUI = newDeckBody.GetComponent<DeckUI>();

        deckUI.Name.text = deck.Name;
        deckUI.Id.text = deck.Id.ToString();
        deckUI.Label.text = $"Deck {deck.Id}: ({deck.UserCards.Count()} cards)";

        newDeckBody.transform.SetParent(deckPanel.transform, false);
    }
}
