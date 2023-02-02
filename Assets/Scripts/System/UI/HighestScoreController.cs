using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighestScoreController : MonoBehaviour
{
    private TextMeshProUGUI highestScoreText;
    private TextMeshProUGUI currentScoreText;

    
    void Start()
    {
        highestScoreText = GameObject.Find("HighestScoreText").GetComponent<TextMeshProUGUI>();
        currentScoreText = GameObject.Find("CurrentScoreText").GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        currentScoreText.text = "Current Score: " + PlayerPrefs.GetInt("Score", 0);
        highestScoreText.text = "Highest Score: " + PlayerPrefs.GetInt("HighestScore", 0);
    }
}
