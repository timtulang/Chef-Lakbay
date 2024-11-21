using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public AudioManager am;
    public static bool isPaused;
    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Pause()
    {
        //FindObjectOfType<AudioManager>().StopBackgroundMusic(FindObjectOfType<AudioManager>().stageMusic);
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }
    public void Resume()
    {
        //FindObjectOfType<AudioManager>().PlayBackgroundMusic(FindObjectOfType<AudioManager>().stageMusic);
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }
    public void QuitStage()
    {
        FindObjectOfType<AudioManager>().StopBackgroundMusic(FindObjectOfType<AudioManager>().stageMusic);
        SceneManager.LoadScene("MainMenu");
    }
}
