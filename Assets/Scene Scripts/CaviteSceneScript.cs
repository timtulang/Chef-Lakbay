using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CaviteSceneScript : MonoBehaviour
{
    public void OnCaviteButtonClicked()
    {
        
        SceneManager.LoadScene("LockedLevel");
    }

    public void OnPrevButtonClicked()
    {
        SceneManager.LoadScene("ManilaScene");
    }

    public void OnNextButtonClicked()
    {
        SceneManager.LoadScene("LagunaScene");
    }

    public void OnLevelBackButtonClicked()
    {
        SceneManager.LoadScene("StageSelectScene");
    }
}
