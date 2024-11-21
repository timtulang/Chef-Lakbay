using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  

public class MainMenu : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void OnLetsCookButtonClicked()
    {
        SceneManager.LoadScene("StageSelectScene");
    }

    public void OnCreditsButtonClicked()
    {
        SceneManager.LoadScene("CreditScene");
    }

    public void OnSkinsButtonClicked()
    {
        SceneManager.LoadScene("SkinScene");
    }

    public void OnSettingsButtonClicked()
    {
        SceneManager.LoadScene("SettingScene");
    }
}
