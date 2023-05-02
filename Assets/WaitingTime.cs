using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class WaitingTime : MonoBehaviour
{
    public int showTime;
    public float time;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI findingMatchText;
    public GameObject cancelButton;

    public int foundMatchTime = 10;

    // Start is called before the first frame update
    void Start()
    {
        time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        showTime = (int)time;
        timeText.text = showTime.ToString();
        if (time >= foundMatchTime)
        {
            findingMatchText.text = "Match Found!";
            timeText.text = "";
            cancelButton.SetActive(false);
            SceneManager.LoadScene(5);
        }
    }

    public void CancelFindingMatch()
    {
        Debug.Log("Cancel");
        SceneManager.LoadScene(2);
    }
}
