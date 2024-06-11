using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GAmeController : MonoBehaviour
{
    public ScoreManager scoreManagerScript;
    public TMP_Text highScoreText, scoreText;
    public GameObject gamePausePanel, gamePauseButton;
    int score, highScore;


    // Start is called before the first frame update
    void Start()
    {
        gamePausePanel.SetActive(false);
        gamePauseButton.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        highScore = PlayerPrefs.GetInt("high_score");
        score = scoreManagerScript.score;

        highScoreText.text = "High Score : " + highScore.ToString();
        scoreText.text = "Your Score : " + score.ToString();
    }

    public void Restart(){
        SceneManager.LoadScene("Game");
    }

    public void PauseGame(){
        Time.timeScale = 0;
        gamePausePanel.SetActive(true);
        gamePauseButton.SetActive(false);
    }

    public void ResumeGame(){
        Time.timeScale = 1;
        gamePausePanel.SetActive(false);
        gamePauseButton.SetActive(true);
    }

    public void MainMenu(){
        SceneManager.LoadScene("Main");
    }
}
