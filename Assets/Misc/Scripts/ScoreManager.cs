using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField]
    private EnemyManager enemyManager; // Reference to the EnemyManager

    [SerializeField]
    private TextMeshProUGUI scoreText; // Reference to the TMPro text field

    private int score;
    private bool canScore;

    private void Start()
    {
        score = 0;
        canScore = true;

        if (enemyManager == null)
        {
            Debug.LogWarning("EnemyManager not assigned to ScoreManager!");
        }

        if (scoreText == null)
        {
            Debug.LogWarning("Score Text component not assigned to ScoreManager!");
        }
    }

    public void UpdateScore(EnemyType enemyType)
    {
        if (canScore)
        {
            score += enemyManager.GetScore(enemyType);
            scoreText.text = $"Score: {score}";
        }
    }

    public void ToggleCanScore(bool value)
    {
        canScore = value;
    }

    public int GetFinalScore()
    {
        return score;
    }
}
