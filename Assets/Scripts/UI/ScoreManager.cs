using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
public class ScoreManager : MonoBehaviour
{

    public static TextMeshProUGUI scoreText;
    public static int score;

    private static Animator anim;

    

    // Start is called before the first frame update
    void Start()
    {
        scoreText = GameObject.Find("Score").GetComponent<TextMeshProUGUI>();
        anim = GameObject.Find("Score").GetComponent<Animator>();
        
    }

    public static void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = score.ToString();
        anim.SetTrigger("ScorePop");
    }

}
