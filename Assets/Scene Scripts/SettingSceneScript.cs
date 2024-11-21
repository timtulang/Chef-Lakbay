using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingSceneScript : MonoBehaviour
{
    public void OnSettingCloseButtonClicked()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
