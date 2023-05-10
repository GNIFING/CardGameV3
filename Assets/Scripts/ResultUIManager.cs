using TMPro;
using UnityEngine;

public class ResultUIManager : MonoBehaviour
{
    public TMP_Text resultText;

    // Start is called before the first frame update
    void Start()
    {
        string result = PlayerPrefs.GetString("Result", "Draw");
        
        // Set result text from PlayerPrefs
        resultText.text = result;
    }
}
