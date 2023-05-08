using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;

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
        StartCoroutine(userController.GetUser((user) =>
        {
            greetingUser.text = "Hi, " + user.Username;
        }));

        StartCoroutine(deckController.GetDecks((decks) =>
        {
            List<DeckItem> deckItems = new();

            deckItems.AddRange(decks.Select(s => new DeckItem() { id = s.Id, name = s.Name }).ToList());

            this.deckItems = deckItems;
            // -------- Add deckItems to dropdown ---------- //
            deckDropdown.GetComponent<TMP_Dropdown>().AddOptions(deckItems.Select(s => s.name.ToString()).ToList());
        }));
    }

    public void OnDropdownChange()
    {
        // ---------- Get deck id from dropdown options from value index from deckItems ---------- //
        int deckId = deckItems.First(f => f.name == deckDropdown.options[deckDropdown.value].text).id;

        PlayerPrefs.SetInt("DeckId", deckId);
    }
    //public void SaveIntoJson(string data)
    //{
    //    System.IO.File.WriteAllText(Application.persistentDataPath + "/deckId.json", data);
    //}
}
