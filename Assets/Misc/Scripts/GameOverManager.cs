using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverManager : MonoBehaviour
{
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI scoreText;

    private ScoreManager scoreManager;
    private Leaderboard leaderboard;
    private int score;

    private void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
        leaderboard = FindObjectOfType<Leaderboard>();
    }

    public void StartGameOver()
    {
        leaderboard.GetHighScores();
        score = scoreManager.GetFinalScore();
        StartCoroutine(GameOverText(1.5f));
    }

    IEnumerator GameOverText(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        DisplayGameOverText();
    }

    private void DisplayGameOverText()
    {
        // Display the game over text.
        gameOverText.gameObject.SetActive(true);
        scoreText.gameObject.SetActive(true);

        gameOverText.text = "Game Over";
        scoreText.text = "Score: " + score;

        StartCoroutine(AfterGameOverText(1.5f));
    }

    IEnumerator AfterGameOverText(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        if (leaderboard.CheckHighScore(score))
        {
            gameOverText.text = "New high score!";

        } else
        {
            SceneManagement.instance.LoadSceneByName("Title");
        }
    }
}
