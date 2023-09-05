using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyType enemyType; // The type of the enemy
    public int baseMaxHealth = 100; // Maximum health of the enemy
    public float bufferDistance = 5f; // Set the buffer distance
    public float moveSpeed = 5f; // Movement speed of the enemy

    protected int currentHealth;   // Current health of the enemy
    protected int currentMaxHealth;
    private ScoreManager scoreManager;
    protected EnemyManager enemyManager;
    private PlayerExp playerExp;
    private AudioManager audioManager;
    protected PlayAreaClamp playAreaClamp; // Reference to the PlayAreaClamp script
    protected SpawnDirection spawnDirection;

    private void Awake()
    {
        // Find the PlayAreaClamp script in the scene
        playAreaClamp = FindObjectOfType<PlayAreaClamp>();
        scoreManager = FindObjectOfType<ScoreManager>();
        playerExp = FindAnyObjectByType<PlayerExp>();
        enemyManager = FindAnyObjectByType<EnemyManager>();
        audioManager = FindObjectOfType<AudioManager>();
    }

    private void Start()
    {
        float healthMultiplier = enemyManager.GetHealthMultiplier(enemyType);
        currentMaxHealth = (int)(baseMaxHealth * (1 + healthMultiplier));
        currentHealth = currentMaxHealth; // Initialize health
    }

    private void Update()
    {
        Move();
    }

    protected virtual void Move()
    {

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
        scoreManager.UpdateScore(enemyType);
        playerExp.GainXP(enemyType);
        audioManager.PlaySound("Explosion");
        Destroy(gameObject); // Destroy the enemy GameObject
    }

    public void SetSpawnDirection(SpawnDirection direction)
    {
        spawnDirection = direction;
    }

    protected void CheckOutOfBounds()
    {
        Vector3 position = transform.position;
        Vector2 minClamp = playAreaClamp.minClamp;
        Vector2 maxClamp = playAreaClamp.maxClamp;

        if (position.x < minClamp.x - bufferDistance || position.x > maxClamp.x + bufferDistance ||
            position.y < minClamp.y - bufferDistance || position.y > maxClamp.y + bufferDistance)
        {
            Destroy(gameObject);
        }
    }

    public void DestroyEnemy()
    {
        scoreManager.ToggleCanScore(false); // Turn off scoring
        playerExp.ToggleXPGain(false);

        Die();

        scoreManager.ToggleCanScore(true); // Turn on scoring
        playerExp.ToggleXPGain(true);
    }

}
