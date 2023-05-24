using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class AuthController : MonoBehaviour
{
    public InputField usernameInput;
    public InputField passwordInput;

    private readonly string controller = "/auth";

    private void Start()
    {
        passwordInput.contentType = InputField.ContentType.Password;
    }

    public void Login() => StartCoroutine(LoginCoroutine());

    IEnumerator LoginCoroutine()
    {
        string path = "/login";

        AuthRequest body = new()
        {
            username = usernameInput.text,
            password = passwordInput.text,
        };

        var request = Api.CreateAuthRequest(controller + path, "POST", body);

        yield return request.SendWebRequest();
        if (request.result != UnityWebRequest.Result.ConnectionError && request.result != UnityWebRequest.Result.ProtocolError)
        {
            AuthResponse data = JsonUtility.FromJson<AuthResponse>(request.downloadHandler.text);

            Api.accessToken = data.accessToken;
            PlayerPrefs.SetString("AccessToken", data.accessToken.ToString());
            PlayerPrefs.Save();

            SceneManager.LoadScene("LobbyPage");
        }
        else
        {
            Debug.Log(request.error);
        }

        request.Dispose();
    }

    public void Register() => StartCoroutine(RegisterCoroutine());

    IEnumerator RegisterCoroutine()
    {
        string path = "/register";

        AuthRequest body = new()
        {
            username = usernameInput.text,
            password = passwordInput.text,
        };

        var request = Api.CreateAuthRequest(controller + path, "POST", body);

        yield return request.SendWebRequest();
        if (request.result != UnityWebRequest.Result.ConnectionError && request.result != UnityWebRequest.Result.ProtocolError)
        {
            AuthResponse data = JsonUtility.FromJson<AuthResponse>(request.downloadHandler.text);

            Api.accessToken = data.accessToken;
        }
        else
        {
            Debug.Log(request.error);
        }

        request.Dispose();
    }

    //IEnumerator GetUserCoroutine()
    //{
    //    //outputArea.text = "Loading...";
    //    string path = "user";

    //    var request = Api.CreateRequest(path, "GET");

    //    yield return request.SendWebRequest();
    //    if (request.result != UnityWebRequest.Result.ConnectionError && request.result != UnityWebRequest.Result.ProtocolError)
    //    {
    //        string json = request.downloadHandler.text;
    //        UserModel user = JsonConvert.DeserializeObject<UserModel>(json);
    //        //outputArea.text = "Hi, " + user.username;
    //    }
    //    else
    //    {
    //        //outputArea.text = request.error;
    //    }

    //    request.Dispose();
    //}
}
