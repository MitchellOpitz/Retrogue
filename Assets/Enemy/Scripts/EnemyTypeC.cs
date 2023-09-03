using UnityEngine;

public class EnemyTypeC : Enemy
{
    private Transform playerTransform; // Reference to the player's transform

    private void Start()
    {
        float healthMultiplier = enemyManager.GetHealthMultiplier(enemyType);
        currentMaxHealth = (int)(baseMaxHealth * (1 + healthMultiplier));
        Debug.Log(enemyType + " currrent max health: " + currentMaxHealth);
        currentHealth = currentMaxHealth; // Initialize health

        // Find the player's transform using a tag or other method
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    protected override void Move()
    {
        base.Move();

        if (playerTransform != null)
        {
            Vector3 directionToPlayer = (playerTransform.position - transform.position).normalized;
            transform.Translate(directionToPlayer * moveSpeed * Time.deltaTime);
        }
    }
}
