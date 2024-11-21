using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AudioManagerScript : MonoBehaviour
{
    public AudioSource backgroundMusic;  // Reference to the AudioSource for background music
    private static AudioManagerScript instance;  // Static instance to prevent duplicates

    // This is called when the script is first initialized
    void Awake()
    {
        // Check if there's already an instance of AudioManagerScript
        if (instance == null)
        {
            instance = this;  // Set this as the instance
            DontDestroyOnLoad(gameObject);  // Prevent this GameObject from being destroyed on scene load

            // Check if an EventSystem exists in the scene. If not, create one.
            EventSystem eventSystem = FindObjectOfType<EventSystem>();
            if (eventSystem == null)
            {
                new GameObject("EventSystem", typeof(EventSystem), typeof(StandaloneInputModule));  // Create a new EventSystem if none exists
            }
        }
        else
        {
            Destroy(gameObject);  // Destroy this instance if there's already an AudioManagerScript
        }
    }

    // This is called when the game starts or when the scene starts
    void Start()
    {
        // Ensure that the background music is playing when the game starts
        if (backgroundMusic != null && !backgroundMusic.isPlaying)
        {
            backgroundMusic.Play();  // Play the music if it's not already playing
        }
    }
}
