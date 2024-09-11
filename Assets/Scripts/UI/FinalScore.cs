using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FinalScore : MonoBehaviour
{
    private TextMeshProUGUI finalScoreText;

    public TextMeshProUGUI highScoreText;
    // Start is called before the first frame update
    void Start()
    {
        print("Final Score: " + ScoreManager.score);
        int totalMemory = PlayerPrefs.GetInt("TotalMemory", 0);
        print("Total Memory: " + totalMemory);
        totalMemory += ScoreManager.score;
        print("Total Memory: " + totalMemory);
        PlayerPrefs.SetInt("TotalMemory", totalMemory);
        print("Total Memory: " + PlayerPrefs.GetInt("TotalMemory", 0));
        finalScoreText = GetComponent<TextMeshProUGUI>();
        finalScoreText.text = "SCORE: " + ScoreManager.score;
        if (ScoreManager.score > PlayerPrefs.GetInt("HighScore", 0)) {
            PlayerPrefs.SetInt("HighScore", ScoreManager.score);
        }
        highScoreText = GameObject.Find("HighScore").GetComponent<TextMeshProUGUI>();
        highScoreText.text = "HIGH SCORE: " + PlayerPrefs.GetInt("HighScore", 0);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
