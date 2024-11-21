using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class StageSceneScript : MonoBehaviour
{
    public void OnLuzonButtonClicked()
    {
        SceneManager.LoadScene("ManilaScene");
    }

    public void OnVisayasButtonClicked()
    {
        SceneManager.LoadScene("VisayasScene");
    }

    public void OnMindanaoButtonClicked()
    {
        SceneManager.LoadScene("MindanaoScene");
    }

    public void OnBackButtonClicked()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
