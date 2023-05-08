using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using Newtonsoft.Json;

public class CardUI : MonoBehaviour
{
    public TextMeshProUGUI Name;
    //public TextMeshProUGUI Description;

    public Image cardImage;
    public Image cardBgImage;

    public int Id;
    //public int deckId = -1;
    public TextMeshProUGUI ClassName;

    public TextMeshProUGUI Cost;
    public TextMeshProUGUI Atk;
    public TextMeshProUGUI Hp;

    public CardUIManager cardUIManager;

    private void Start()
    {
        cardUIManager = FindObjectOfType<CardUIManager>();
    }
    public void AddCard()
    {
        int deckId = PlayerPrefs.GetInt("DeckId");

        cardUIManager.AddCard(deckId, Id);
    }

    public void RemoveCard()
    {
        int deckId = PlayerPrefs.GetInt("DeckId");

        cardUIManager.RemoveCard(deckId, Id);
    }

    //public CardUIManager cardUIManager;
    //public TMP_Dropdown deckDropdown;
    //public Button removeCardButton;

    //public void OnClickRemoveCardFromDeck()
    //{
    //    StartCoroutine(cardUIManager.cardController.RemoveCard(this.deckId, this.id, (responseData) =>
    //    {
    //        Card[] response = JsonConvert.DeserializeObject<Card[]>(responseData);

    //        // ---------- Clear card panel ---------- //
    //        cardUIManager.GetCards().Clear();

    //        // ---------- Add new cards ---------- //
    //        cardUIManager.GetCards().AddRange(response.Select(s => s));

    //        cardUIManager.UpdatePage();
    //        cardUIManager.AssignDeckId(deckId);
    //    }));
    //}

    //public void OnClickAssignToDeck()
    //{
    //    // ---------- Get deck id from dropdown options from value index from deckItems ---------- //
    //    int deckId = cardUIManager
    //        .GetDeckItems()
    //        .First(f =>
    //            f.name == deckDropdown.options[deckDropdown.value].text
    //        )
    //        .id;

    //    StartCoroutine(cardUIManager.cardController.AddCard(deckId, this.id, (responseData) =>
    //    {
    //        cardUIManager.UpdatePage();
    //    }));
    //}
}
