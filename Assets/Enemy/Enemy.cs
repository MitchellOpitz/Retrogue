using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyType enemyType; // The type of the enemy
    public int maxHealth = 100; // Maximum health of the enemy

    private int currentHealth;   // Current health of the enemy

    private void Start()
    {
        currentHealth = maxHealth; // Initialize health
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Perform death behavior based on enemyType (e.g., play death animation, drop items, etc.)
        Destroy(gameObject); // Destroy the enemy GameObject
    }
}
