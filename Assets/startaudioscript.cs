using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class startaudioscript : MonoBehaviour
{
    public AudioSource startLoadingMusic; // Reference to the AudioSource for Start/Loading screens

    private static startaudioscript instance;

    void Awake()
    {
        // Ensure the music persists across scenes
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        // Play music at the start
        if (startLoadingMusic != null)
        {
            startLoadingMusic.Play();
        }

        // Listen for scene changes
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy()
    {
        // Cleanup listener to prevent memory leaks
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Stop music when the Main Menu is loaded
        if (scene.name == "MainMenu") // Replace with your Main Menu scene's name
        {
            if (startLoadingMusic != null && startLoadingMusic.isPlaying)
            {
                startLoadingMusic.Stop();
            }

            Destroy(gameObject); // Remove the object as it's no longer needed
        }
    }
}
