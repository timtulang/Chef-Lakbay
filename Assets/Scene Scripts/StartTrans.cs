using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartTrans : MonoBehaviour
{
    public void OnStartButtonClicked()
    {
        SceneManager.LoadScene("LoadingScreen");
    }

    public void OnQuitButtonClicked()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
