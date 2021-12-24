using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverScreen : MonoBehaviour
{
    public TextMeshProUGUI pointsText;
    public TextMeshProUGUI highScoreText;

    void Start(){
        if (ScoreScript.currentScore > ScoreScript.highScore)
        {
            ScoreScript.highScore = ScoreScript.currentScore;
            PlayerPrefs.SetInt("highScore", ScoreScript.currentScore);
        }

        pointsText.text = Mathf.RoundToInt(ScoreScript.currentScore).ToString() + " POINTS";
        highScoreText.text = "High Score: " + Mathf.RoundToInt(ScoreScript.highScore).ToString();
    }
}
