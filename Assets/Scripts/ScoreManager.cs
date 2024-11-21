using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text gameOverScore, gameOverHighScore;
    public int highScore;
    public PlayerGrab pg;
    public OrderManager om;
    public GameObject checkoutArea, gameOverPanel;
    public Text scoreTxt;
    public Text highScoreTxt;
    public int score = 0, index = -1;
    private bool orderCorrect = false;
    public void Start()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        highScoreTxt.text = "High: " + score.ToString();
    }
    private void Update()
    {
        scoreTxt.text = score.ToString();
        orderCorrect = false;
        if (pg.CurrentItem != null)
        {
            if (checkoutArea.GetComponent<Collider2D>().IsTouching(pg.transform.GetChild(0).GetComponent<Collider2D>()) && om.FindOrderIndex(pg.CurrentItem) != -1)
            {
                orderCorrect = true;
                Debug.Log(orderCorrect);
            }
        }
    }
    public void SaveHighScore(int newScore)
    {
        int currentHighScore = PlayerPrefs.GetInt("HighScore", 0); // Retrieve current high score, default to 0
        if (newScore > currentHighScore)
        {
            PlayerPrefs.SetInt("HighScore", newScore); // Save the new high score
            PlayerPrefs.Save(); // Ensure it's written to storage
        }
    }
    public void OnSpawnButtonPress()
    {
        if (orderCorrect)
        {
            om.RemoveOrder(pg.CurrentItem);
            Destroy(pg.CurrentItem);
            score += 250;
        }
    }
    public void GameOver()
    {
        gameOverScore.text = "Score: " + score.ToString();
        gameOverHighScore.text = "High Score: " + highScore.ToString();
        SaveHighScore(score);
        Time.timeScale = 0; // Pause the game
        gameOverPanel.SetActive(true);
    }
}
