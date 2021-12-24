using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreScript : MonoBehaviour
{
    public static int currentScore;
    public static int highScore;
    
    private TextMeshProUGUI scoreText;
    
    // Start is called before the first frame update
    void Start()
    {
        currentScore = 0;
        scoreText = GetComponent<TextMeshProUGUI>();
        highScore = PlayerPrefs.GetInt("highScore");
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score: " + currentScore;
    }
}
