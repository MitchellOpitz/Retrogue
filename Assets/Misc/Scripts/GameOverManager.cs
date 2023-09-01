using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverManager : MonoBehaviour
{
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI scoreText;

    private ScoreManager scoreManager;

    private void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
    }

    public void StartGameOver()
    {
        // Wait for a few seconds before starting the game over sequence.
        StartCoroutine(BeforeText(3.0f));
    }

    IEnumerator BeforeText(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        UpdateGameOverText();
    }

    private void UpdateGameOverText()
    {
        // Display the game over text.
        gameOverText.gameObject.SetActive(true);
        scoreText.gameObject.SetActive(true);

        // Show the player's score.
        scoreText.text = "Score: " + scoreManager.GetFinalScore();
        StartCoroutine(AfterText(3.0f));
    }

    IEnumerator AfterText(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        SceneManagement.instance.LoadSceneByName("Title");
    }
}
