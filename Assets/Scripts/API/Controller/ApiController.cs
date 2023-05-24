using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.API.Controller
{
    public class ApiController: MonoBehaviour
    {
        protected void CheckRequestStatus(UnityWebRequest request)
        {
            Debug.Log(request.error);

            // ---------- If unauthorized, load scene login ---------- //
            if (request.responseCode != 200)
            {
                SceneManager.LoadScene("LoginPage");
            }
        }
    }
}
