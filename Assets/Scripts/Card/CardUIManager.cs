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
    public CardController cardController;
    public DeckController deckController;

    private Deck deck;
    private List<UserCard> userCards;

    public GameObject userCardBody;
    public GameObject deckCardBody;
    public RectTransform cardPanel;
    public Button deckCardsButton;
    public Button myCardsButton;
    public TMP_Text deckName;
    private string? classType;
    private int? costType;

    private string activeTab = "deckCards";

    private IEnumerator Start() {
        classType = null;
        costType = null;

        int deckId = PlayerPrefs.GetInt("DeckId");

        yield return StartCoroutine(deckController.GetDeck(deckId, (deck) =>
        {
            this.deck = deck;

            // Set deck name in navbar
            deckName.text = $"Deck {deck.Id}: {deck.Name}";

            deckCardsButton.Select();
        }));

        yield return StartCoroutine(cardController.GetCards((userCards) =>
        {
            Debug.Log(deck.ClassName.ToString());
            this.userCards = userCards
                .Where(w =>
                    !deck.UserCards.Select(s => s.id).Contains(w.id) &&
                    w.card.className == deck.ClassName.ToString() &&
                    w.activeFlag == ActiveFlag.R
                )
                .Select(s => s)
                .ToList();

        }));
        
        UpdatePage();
    }
    public void UpdatePage()
    {
        if (activeTab == "deckCards")
        {
            RenderUserCards(deck.UserCards.ToList(), false);
        }
        else if (activeTab == "myCards")
        {
            RenderUserCards(userCards);
        }
        else
        {
            return;
        }
    }

    private void RenderUserCards(List<UserCard> userCards, bool resetPage = true)
    {
        ResetCardPanelChildren();

        if (resetPage)
        {
            //ResetPage(cards.Count);
        }

        //Debug.Log(userCards
            //.Where(w => classType == null || w.card.className == classType)
            //.Where(w => costType == null || w.card.cost == costType)
            //.Select(s => s).Count());
        //Debug.Log(costType);

        foreach (UserCard userCard in userCards
            //.Where(w => classType == null || w.card.className == classType)
            .Where(w => costType == null || w.card.cost.ToString() == costType.ToString())
            .Select(s => s)
        )
        {
            Debug.Log(userCard.card.cost);
            RenderUserCard(userCard);
        }
    }
    private void ResetCardPanelChildren()
    {
        while (cardPanel.transform.childCount > 0)
        {
            DestroyImmediate(cardPanel.transform.GetChild(0).gameObject);
        }
    }

    private void RenderUserCard(UserCard userCard)
    {
        GameObject newUseruserCardBody = Instantiate(activeTab == "deckCards" ? deckCardBody : userCardBody);
        //CardUI cardUI = newUseruserCardBody.transform.GetChild(0).GetComponent<CardUI>();
        CardUI cardUI = newUseruserCardBody.GetComponentInChildren<CardUI>();

        cardUI.Id = userCard.id;
        cardUI.Name.text = userCard.card.name.ToString();
        cardUI.ClassName.text = userCard.card.className.ToString();
        cardUI.Cost.text = userCard.card.cost.ToString();
        cardUI.Hp.text = userCard.card.hp.ToString();
        cardUI.Atk.text = userCard.card.atk.ToString();

        StartCoroutine(DownloadImage(userCard.card.imageUri, cardUI));

        newUseruserCardBody.transform.SetParent(cardPanel.transform, false);
    }
    IEnumerator DownloadImage(string MediaUrl, CardUI cardUI)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(MediaUrl);
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

    Sprite SpriteFromTexture2D(Texture2D texture)
    {
        return Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100.0f);
    }

    public void ToggleTab(string targetTab)
    {
        // If currently on this active tab, do nothing
        if (targetTab == activeTab) return;

        activeTab = targetTab;

        UpdatePage();
    }

    public void FilterClassType(string targetClassType)
    {
        // Reset underline
        GameObject humanClassTypeText = GameObject.Find($"Class Human");
        GameObject elfClassTypeText = GameObject.Find($"Class Elf");
        GameObject ogreClassTypeText = GameObject.Find($"Class Ogre");
        humanClassTypeText.GetComponent<TMP_Text>().text = $"Human";
        elfClassTypeText.GetComponent<TMP_Text>().text = $"Elf";
        ogreClassTypeText.GetComponent<TMP_Text>().text = $"Ogre";

        if (classType == targetClassType)
        {
            classType = null;
        }
        else
        {
            classType = targetClassType;
            GameObject classTypeText = GameObject.Find($"Class {targetClassType}");
            classTypeText.GetComponent<TMP_Text>().text = $"<u>{targetClassType}</u>";
        }

        UpdatePage();
    }
    public void FilterCostType(int targetCostType)
    {
        // Reset underline
        GameObject Cost1TypeText = GameObject.Find($"Cost 1");
        GameObject Cost2TypeText = GameObject.Find($"Cost 2");
        GameObject Cost3TypeText = GameObject.Find($"Cost 3");
        GameObject Cost4TypeText = GameObject.Find($"Cost 4");
        GameObject Cost5TypeText = GameObject.Find($"Cost 5");
        GameObject Cost6TypeText = GameObject.Find($"Cost 6");
        GameObject Cost7TypeText = GameObject.Find($"Cost 7");
        GameObject Cost8TypeText = GameObject.Find($"Cost 8");
        Cost1TypeText.GetComponent<TMP_Text>().text = $"1";
        Cost2TypeText.GetComponent<TMP_Text>().text = $"2";
        Cost3TypeText.GetComponent<TMP_Text>().text = $"3";
        Cost4TypeText.GetComponent<TMP_Text>().text = $"4";
        Cost5TypeText.GetComponent<TMP_Text>().text = $"5";
        Cost6TypeText.GetComponent<TMP_Text>().text = $"6";
        Cost7TypeText.GetComponent<TMP_Text>().text = $"7";
        Cost8TypeText.GetComponent<TMP_Text>().text = $"8";

        if (costType == targetCostType)
        {
            costType = null;
        }
        else
        {
            costType = targetCostType;
            GameObject costTypeText = GameObject.Find($"Cost {targetCostType}");
            costTypeText.GetComponent<TMP_Text>().text = $"<u>{targetCostType}</u>";
        }

        UpdatePage();
    }

    public void AddCard(int deckId, int cardId)
    {
        StartCoroutine(deckController.AddCard(deckId, cardId, (deck) =>
        {
            userCards.Remove(userCards.Single(x => x.id == cardId));
            this.deck = deck;

            UpdatePage();
        }));
    }

    public void RemoveCard(int deckId, int cardId)
    {
        StartCoroutine(deckController.RemoveCard(deckId, cardId, (deck) =>
        {
            userCards.Add(this.deck.UserCards.Single(x => x.id == cardId));
            this.deck = deck;

            Debug.Log(userCards.Count());
            Debug.Log(deck.UserCards.Count());

            UpdatePage();
        }));
    }
}
