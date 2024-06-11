using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int score = 0, highSCore;
    public static int lastScore = 0;
    public TMP_Text scoreText, highScoreText, lastScoreText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = score.ToString();

        if(score > highSCore){
            highSCore = score;
            PlayerPrefs.SetInt("highScore", highSCore);
        }
    }

    IEnumerator Score(){
        while(true){
            yield return new WaitForSeconds(0.8f);
            score++;
            lastScore = score;
        }
    }
}
