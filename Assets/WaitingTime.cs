using System.Collections;
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
    public RoomController roomController;

    //public int foundMatchTime = 10;

    // Start is called before the first frame update
    void Start()
    {
        time = 0;
        int deckId = PlayerPrefs.GetInt("DeckId");
        Debug.Log(deckId);
        //StartCoroutine(FindMatch(3));
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        showTime = (int)time;
        timeText.text = showTime.ToString();
        //if (time >= foundMatchTime)
        //{
        //    findingMatchText.text = "Match Found!";
        //    timeText.text = "";
        //    cancelButton.SetActive(false);
        //    SceneManager.LoadScene(5);
        //}
    }

    IEnumerator FindMatch(int cycleSecond)
    {
        while (true)
        {
            StartCoroutine(roomController.MatchMaking(2, (responseData) =>
            {
                Debug.Log(responseData);
            }));
            
            // Wait for sometime before call matching again
            yield return new WaitForSeconds(cycleSecond);
        }
    }

    public void CancelFindingMatch()
    {
        Debug.Log("Cancel");
        SceneManager.LoadScene(2);
    }
}
