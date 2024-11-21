using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LagunaSceneScript : MonoBehaviour
{
    public void OnLagunaButtonClicked()
    {
        
        SceneManager.LoadScene("LagunaLockedLevel");
    }

    public void OnPrevButtonClicked()
    {
        SceneManager.LoadScene("CaviteScene");
    }

    public void OnNextButtonClicked()
    {
        SceneManager.LoadScene("ManilaScene");
    }

    public void OnLevelBackButtonClicked()
    {
        SceneManager.LoadScene("StageSelectScene");
    }
}
