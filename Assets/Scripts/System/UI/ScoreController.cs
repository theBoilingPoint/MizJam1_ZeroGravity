using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class ScoreController : MonoBehaviour
{
    private TextMeshProUGUI scoreText;
    private int score;
    private int survivalTime;
    private int bonus;
    
    void Start()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        updateSurvivalTime();
        checkScore();
        scoreText.text = "Score: " + score.ToString();
    }

    public void increaseScoreBy(int num)
    {
        bonus += num;
    }

    public int getCurrentScore()
    {
        return score;
    }

    private void checkScore()
    {
        score = survivalTime + bonus;
    }

    private void updateSurvivalTime()
    {
        survivalTime = (int)Math.Round(Time.timeSinceLevelLoad, 0);
    }
}
