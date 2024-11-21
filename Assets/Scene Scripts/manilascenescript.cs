using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class manilascenescript : MonoBehaviour
{
    public void OnManilaButtonClicked()
    {
        SceneManager.LoadScene("ShoppingLoading");
    }

    public void OnPrevButtonClicked()
    {
        SceneManager.LoadScene("LagunaScene");
    }

    public void OnNextButtonClicked()
    {
        SceneManager.LoadScene("CaviteScene");
    }

    public void OnLevelBackButtonClicked()
    {
        SceneManager.LoadScene("StageSelectScene");
    }
}
