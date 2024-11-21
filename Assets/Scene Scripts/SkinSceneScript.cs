using UnityEngine;
using UnityEngine.SceneManagement;

public class SkinSceneScript : MonoBehaviour
{
    public void OnSkinCloseButtonClicked()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
