using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class CardUI : MonoBehaviour
{
    public TextMeshProUGUI unitName;
    public TextMeshProUGUI unitDescription;

    public Image cardImage;
    public Image cardBgImage;

    public int id;
    public int deckId = -1;
    public TextMeshProUGUI className;

    public TextMeshProUGUI cost;
    public TextMeshProUGUI atk;
    public TextMeshProUGUI hp;

    public CardUIManager cardUIManager;
    public TMP_Dropdown deckDropdown;
    public Button removeCardButton;

    public void OnClickRemoveCardFromDeck()
    {
        StartCoroutine(cardUIManager.RemoveCard(this.deckId, this.id));
    }

    public void OnClickAssignToDeck()
    {
        // ---------- Get deck id from dropdown options from value index from deckItems ---------- //
        int deckId = cardUIManager
            .GetDeckItems()
            .First(f =>
                f.name == deckDropdown.options[deckDropdown.value].text
            )
            .id;

        StartCoroutine(cardUIManager.AddCard(deckId, this.id));
    }
}
