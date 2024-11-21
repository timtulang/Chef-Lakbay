using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // Add this for scene management

public class LoadingBarScript : MonoBehaviour
{
    public Slider LoadingSlider; 
    private float loadingTime = 3f; 
    private float currentTime = 0f; 

    void Start()
    {
        if (LoadingSlider != null)
        {
            LoadingSlider.value = 0;
        }
        else
        {
            Debug.LogError("LoadingSlider not assigned!");
        }
    }

    void Update()
    {
        if (currentTime < loadingTime)
        {
            currentTime += Time.deltaTime;
            float progress = Mathf.Clamp01(currentTime / loadingTime);
            LoadingSlider.value = progress;

            if (currentTime >= loadingTime)
            {
                OnLoadingComplete();
            }
        }
    }

    void OnLoadingComplete()
    {
        Debug.Log("Loading complete!");
        SceneManager.LoadScene("MainMenu"); 
    }
}
