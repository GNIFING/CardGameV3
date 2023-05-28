using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;
using Unity.VisualScripting;

public class ResultUIManager : MonoBehaviour
{
    public TMP_Text resultText;
    public TMP_Text pointText;
    public TMP_Text loadingText;
    public CardController cardController;

    public RectTransform cardPanel;
    public GameObject cardBody;

    // Start is called before the first frame update
    void Start()
    {
        string result = PlayerPrefs.GetString("Result", "Draw");

        // Set result text from PlayerPrefs
        resultText.text = result;

        // Set point due to result
        pointText.text = $"{(result == "Victory" ? "+ 1" : "- 1")} Score";

        if (result == "Victory")
        {
            StartCoroutine(cardController.GetReward((userCard) =>
            {
                GameObject newCardBody = Instantiate(cardBody);
                
                CardUI cardUI = newCardBody.GetComponentInChildren<CardUI>();

                StartCoroutine(DownloadImage(userCard.card.imageUri, cardUI));

                cardUI.Id = userCard.card.id;
                cardUI.Name.text = userCard.card.name.ToString();
                cardUI.ClassName.text = userCard.card.className.ToString();
                cardUI.Cost.text = userCard.card.cost.ToString();
                cardUI.Hp.text = userCard.card.hp.ToString();
                cardUI.Atk.text = userCard.card.atk.ToString();

                newCardBody.transform.SetParent(cardPanel.transform, false);
                loadingText.gameObject.SetActive(false);
            }));
        } else if (result == "Defeat")
        {
            loadingText.text = "No reward";
        }
    }


    private IEnumerator DownloadImage(string MediaUrl, CardUI cardUI)
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

    private Sprite SpriteFromTexture2D(Texture2D texture)
    {
        return Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100.0f);
    }
}
