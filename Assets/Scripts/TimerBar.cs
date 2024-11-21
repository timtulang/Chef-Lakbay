using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerBar : MonoBehaviour
{
    [Header("UI References")]
    public UnityEngine.UI.Image fillImage; // Reference to the image that fills the timer bar.

    private float timerDuration; // Total time for the timer.
    private float timeRemaining; // Remaining time for the timer.

    // Initialize the timer
    public void InitializeTimer(float duration)
    {
        timerDuration = duration;
        timeRemaining = duration;
        UpdateFill(); // Ensure the bar starts full.
    }

    private void Update()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            UpdateFill();

            if (timeRemaining <= 0)
            {
                TimerFinished();
            }
        }
    }

    // Update the fill amount of the image
    private void UpdateFill()
    {
        if (fillImage != null)
        {
            fillImage.fillAmount = timeRemaining / timerDuration;
        }
    }

    // Logic when the timer finishes
    private void TimerFinished()
    {
        Debug.Log("Timer Finished!");
    }
}
