using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FinalScore : MonoBehaviour
{
    private TextMeshProUGUI finalScoreText;
    // Start is called before the first frame update
    void Start()
    {
        finalScoreText = GetComponent<TextMeshProUGUI>();
        finalScoreText.text = "SCORE: " + ScoreManager.score;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
