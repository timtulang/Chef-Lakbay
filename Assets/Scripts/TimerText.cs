using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerText : MonoBehaviour
{
    [SerializeField] Text timer;
    [SerializeField] float elapsedTime;
    [SerializeField] float endTime;
    void Start()
    {
        elapsedTime = 0f;
        Time.timeScale = 2f;
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        if (Mathf.FloorToInt(elapsedTime) == 779)
        {
            elapsedTime = 60f;
        }
        if (Mathf.FloorToInt(elapsedTime) == 360)
        {
            Time.timeScale = 0f;
        }
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);
        timer.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
