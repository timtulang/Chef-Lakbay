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
    public ScoreManager sm;
    void Start()
    {
        endTime = 0f;
        elapsedTime = 600f;
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        endTime += Time.deltaTime;
        if (Mathf.FloorToInt(elapsedTime) == 779f)
        {
            elapsedTime = 60f;
        }
        if (Mathf.FloorToInt(endTime) == 300f)
        {
            sm.GameOver();
        }
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);
        timer.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
