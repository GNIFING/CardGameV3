using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEditor.PackageManager.Requests;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.Sqlite;
using System.Text;

public class CardData : MonoBehaviour
{
    public InputField outputArea;
    public RawImage cardImage;

    public void GetCard() => StartCoroutine(GetCardCoroutine());

    IEnumerator GetCardCoroutine()
    {
        outputArea.text = "Loading...";
        RectTransform startPos = outputArea.GetComponent<RectTransform>();
        RectTransform imagePos = cardImage.GetComponent<RectTransform>();

        string path = "card";

        var request = Api.CreateRequest(path, "GET");

        yield return request.SendWebRequest();
        if (request.result != UnityWebRequest.Result.ConnectionError && request.result != UnityWebRequest.Result.ProtocolError)
        {
            var json = request.downloadHandler.text;
            outputArea.text = json;
            Card[] cards = JsonConvert.DeserializeObject<Card[]>(json);
            Debug.Log(outputArea.transform.position);
            foreach (Card card in cards)
            {
                InputField newOutputArea = Instantiate(outputArea, outputArea.transform);
                RectTransform newOutputAreaTransform = newOutputArea.GetComponent<RectTransform>();
                newOutputAreaTransform.anchoredPosition = new Vector2(200, 0);

                RawImage newRawImage = Instantiate(cardImage, cardImage.transform);
                RectTransform newRawImageTransform = newRawImage.GetComponent<RectTransform>();
                newRawImageTransform.anchoredPosition = new Vector2(200, 0);

                // newOutputArea.text = card.className + ": " + card.unitName + "\nattack type: " + card.atkType + "\nHp: " + card.hp + "\nAttack: " + card.atk;
            }
        }
        else 
        {
            outputArea.text = request.error;
        }
    }
}
