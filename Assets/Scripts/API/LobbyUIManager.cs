using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using EasyUI.Toast;
using Newtonsoft.Json;
using SocketIOClient;
using SocketIOClient.Newtonsoft.Json;
using TMPro;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyUIManager : MonoBehaviour
{
    public TMP_Text greetingUser;
    public UserController userController;
    public DeckController deckController;
    public TMP_Dropdown deckDropdown;

    private List<DeckItem> deckItems;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(userController.GetUser((responseData) =>
        {
            UserModel user = JsonConvert.DeserializeObject<UserModel>(responseData);
            greetingUser.text = "Hi, " + user.username;
            //Toast.Show("Welcome Back!", 3f, ToastColor.Black, ToastPosition.TopRight);
        }));

        StartCoroutine(deckController.GetDecks((responseData) =>
        {
            List<Deck> decks = new(JsonConvert.DeserializeObject<Deck[]>(responseData));

            deckItems = new()
            {
                // ---------- Add placeholder at the start ---------- //        
                new DeckItem() { id = 0, name = "Choose" }
            };

            deckItems.AddRange(decks.Select(s => new DeckItem() { id = s.id, name = s.name }).ToList());

            // -------- Add deckItems to dropdown ---------- //
            deckDropdown.GetComponent<TMP_Dropdown>().AddOptions(deckItems.Select(s => s.name.ToString()).ToList());
        }));
    }

    public void OnDropdownChange()
    {
        // ---------- Get deck id from dropdown options from value index from deckItems ---------- //
        int deckId = deckItems.First(f => f.name == deckDropdown.options[deckDropdown.value].text).id;

        // ---------- If value equals "Select deck", do nothing ---------- //
        if (deckId == 0)
        {
            // Do nothing //


            //StartCoroutine(cardController.GetCards((responseData) =>
            //{
            //    userCards = new List<UserCard>(JsonConvert.DeserializeObject<UserCard[]>(responseData));

            //    cards.AddRange(userCards.Select(s => s.card));

            //    UpdatePage();
            //}));
        }

        // ---------- If not, get deck by id from dropdown value ---------- //
        else
        {
            SaveIntoJson(deckId.ToString());
        }
    }
    public void SaveIntoJson(string data)
    {
        System.IO.File.WriteAllText(Application.persistentDataPath + "/deckId.json", data);
    }
}
