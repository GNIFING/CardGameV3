using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingPage : MonoBehaviour
{
    public GameObject surrenderPage;
    
    public void SetActiveSurrenderPage(bool isActive)
    {
        surrenderPage.SetActive(isActive);
    }
}
