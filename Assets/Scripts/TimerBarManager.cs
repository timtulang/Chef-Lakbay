using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerBarManager : MonoBehaviour
{
    public GameObject timerBarPrefab; // Assign the TimerBar prefab in the inspector.

    // Function to create a timer bar
    public GameObject CreateTimerBar(Vector3 position, Transform parent, float duration)
    {
        // Instantiate the timer bar
        GameObject timerBarInstance = Instantiate(timerBarPrefab, position, Quaternion.identity, parent);

        // Initialize the timer bar with the duration
        TimerBar timerBar = timerBarInstance.GetComponent<TimerBar>();
        if (timerBar != null)
        {
            timerBar.InitializeTimer(duration);
        }

        return timerBarInstance;
    }
}