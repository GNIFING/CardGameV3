using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class DeckUI : MonoBehaviour
{
    public TextMeshProUGUI Name;
    public TextMeshProUGUI Id;
    public TextMeshProUGUI Label;

    public void EditDeck()
    {
        PlayerPrefs.SetInt("DeckId", int.Parse(Id.text));
        SceneManager.LoadScene("CardPage");
    }
}

