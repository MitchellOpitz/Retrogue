using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    public int baseDamage;
    public float damageMultiplier = 0;
    public int baseMaxHealth;
    public float healthMultiplier;
    public TextMeshProUGUI healthText;

    private int currentHealth;
    private EnemyManager enemyManager;

    private void Start()
    {
        damageMultiplier = 0;
        healthMultiplier = 1;

        currentHealth = baseMaxHealth;
        UpdateHealthUI();

        enemyManager = FindObjectOfType<EnemyManager>();
    }

    private void UpdateHealthUI()
    {
        healthText.text = "Health: " + currentHealth + " / " + (baseMaxHealth * healthMultiplier);
    }

    public void Damage(int damage)
    {
        currentHealth -= damage;
        UpdateHealthUI();

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            UpdateHealthUI();
            enemyManager.DestroyAllEnemies();
            FindObjectOfType<GameOverManager>().StartGameOver();
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D (Collider2D collider)
    {
        // Debug.Log("Collision detected.");
        if (collider.gameObject.tag == "Enemy")
        {
            // Debug.Log("Enemy detected.");
            Damage(10);
        }
    }
}
