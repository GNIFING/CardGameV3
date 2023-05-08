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

    private string roomId;
    private int playerId;

    //public int foundMatchTime = 10;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        time = 0;
        this.playerId = PlayerPrefs.GetInt("PlayerId");

        // Create initial MatchMaking
        yield return StartCoroutine(roomController.MatchMaking(this.playerId, (response) =>
        {
            Debug.Log(response);

            // Store roomId
            this.roomId = response.roomId;

            // if match other player
            if (response.playerTwoId != null && response.arenaId != null)
            {
                PlayerPrefs.SetInt("ArenaId", (int)response.arenaId);

                // Load game scene
                SceneManager.LoadScene("SampleScene");
            }
            else
            {
                // Return to lobby if something wrong
                SceneManager.LoadScene("LobbyPage");
            }
        }));

        Debug.Log(this.roomId);

        // If not match other play, create a cycle for checking room status
        yield return StartCoroutine(GetRoom(this.roomId, 5));
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        showTime = (int)time;
        timeText.text = showTime.ToString();
    }

    IEnumerator GetRoom(string roomId, int cycleSecond)
    {
        while (true)
        {
            // Wait for sometime before call matching again
            yield return new WaitForSeconds(cycleSecond);

            StartCoroutine(roomController.GetRoom(roomId, (response) =>
            {
                Debug.Log(response);

                // match other player
                if (response.playerTwoId != null && response.arenaId != null)
                {
                    PlayerPrefs.SetInt("ArenaId", (int)response.arenaId);
                    // Load game scene
                    SceneManager.LoadScene("SampleScene");
                }
                else
                {
                    // Return to lobby if something wrong
                    SceneManager.LoadScene("LobbyPage");
                }
            }));
        }
    }

    public void CancelMatchMaking()
    {
        StartCoroutine(roomController.CancelMatchMaking(this.roomId, this.playerId, (response) =>
        {
            if (response.isActive == false)
            {
                SceneManager.LoadScene("LobbyPage");
            }
            else
            {
                Debug.LogError(response);
            }
        }));
    }
}
