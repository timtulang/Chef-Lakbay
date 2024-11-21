using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditSceneScript : MonoBehaviour
{
    public void OnCreditCloseButtonClicked()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
