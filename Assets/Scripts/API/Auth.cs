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

public class Auth : MonoBehaviour
{
    public InputField outputArea;
    public InputField usernameInput;
    public InputField passwordInput;

    public void Login() => StartCoroutine(LoginCoroutine());

    IEnumerator LoginCoroutine()
    {
        outputArea.text = "Loading...";
        string path = "auth/login";

        AuthRequest body = new AuthRequest()
        {
            username = usernameInput.text,
            password = passwordInput.text,
        };

        var request = Api.CreateAuthRequest(path, "POST", body);

        yield return request.SendWebRequest();
        if (request.result != UnityWebRequest.Result.ConnectionError && request.result != UnityWebRequest.Result.ProtocolError)
        {
            AuthResponse data = JsonUtility.FromJson<AuthResponse>(request.downloadHandler.text);
            outputArea.text = data.accessToken;
            Api.accessToken = data.accessToken;
        }
        else
        {
            outputArea.text = request.error;
        }
    }

    public void Register() => StartCoroutine(RegisterCoroutine());

    IEnumerator RegisterCoroutine()
    {
        outputArea.text = "Loading...";
        string path = "auth/register";

        AuthRequest body = new AuthRequest()
        {
            username = usernameInput.text,
            password = passwordInput.text,
        };

        var request = Api.CreateAuthRequest(path, "POST", body);

        yield return request.SendWebRequest();
        if (request.result != UnityWebRequest.Result.ConnectionError && request.result != UnityWebRequest.Result.ProtocolError)
        {
            AuthResponse data = JsonUtility.FromJson<AuthResponse>(request.downloadHandler.text);
            outputArea.text = data.accessToken;
            Api.accessToken = data.accessToken;
        }
        else
        {
            outputArea.text = request.error;
        }
    }

    public void GetUser() => StartCoroutine(GetUserCoroutine());

    IEnumerator GetUserCoroutine()
    {
        outputArea.text = "Loading...";
        string path = "user";

        var request = Api.CreateRequest(path, "GET");

        yield return request.SendWebRequest();
        if (request.result != UnityWebRequest.Result.ConnectionError && request.result != UnityWebRequest.Result.ProtocolError)
        {
            string json = request.downloadHandler.text;
            UserModel user = JsonUtility.FromJson<UserModel>(json);
            outputArea.text = "Hi, " + user.username;
        }
        else
        {
            outputArea.text = request.error;
        }
    }
}
