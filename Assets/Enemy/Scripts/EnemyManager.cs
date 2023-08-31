using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField]
    private List<EnemyTypeProperties> enemyTypePropertiesList = new List<EnemyTypeProperties>();

    private Dictionary<EnemyType, EnemyTypeProperties> enemyTypePropertiesDict;
    private ScoreManager scoreManager;
    private Player player;
    private EnemySpawner enemySpawner;

    private void Awake()
    {
        enemyTypePropertiesDict = new Dictionary<EnemyType, EnemyTypeProperties>();
        foreach (var properties in enemyTypePropertiesList)
        {
            enemyTypePropertiesDict.Add(properties.enemyType, properties);
        }
        scoreManager = FindAnyObjectByType<ScoreManager>();
        player = FindAnyObjectByType<Player>();
        enemySpawner = FindAnyObjectByType<EnemySpawner>();
    }

    public float GetSpawnInterval(EnemyType enemyType)
    {
        if (enemyTypePropertiesDict.TryGetValue(enemyType, out EnemyTypeProperties properties))
        {
            float baseSpawnRate = properties.baseSpawnRate;
            float spawnMultiplier = properties.spawnMultiplier;
            float adjustedSpawnRate = baseSpawnRate - (baseSpawnRate * spawnMultiplier);
            return adjustedSpawnRate;
        }
        else
        {
            Debug.LogWarning("EnemyType not found in dictionary!");
            return 0f; // Return a default value or handle this case as needed
        }
    }

    public GameObject GetEnemyPrefab(EnemyType enemyType)
    {
        if (enemyTypePropertiesDict.TryGetValue(enemyType, out EnemyTypeProperties properties))
        {
            switch (enemyType)
            {
                case EnemyType.TypeA:
                    return properties.enemyPrefab; // Return the TypeA prefab
                case EnemyType.TypeB:
                    return properties.enemyPrefab; // Return the TypeB prefab
                case EnemyType.TypeC:
                    return properties.enemyPrefab; // Return the TypeC prefab
                case EnemyType.TypeD:
                    return properties.enemyPrefab; // Return the TypeD prefab
                // Add cases for other enemy types as needed
                default:
                    Debug.LogWarning($"Unknown enemy type: {enemyType}");
                    return null;
            }
        }
        else
        {
            Debug.LogWarning("EnemyType not found in dictionary!");
            return null; // Return null or handle this case as needed
        }
    }

    public int GetScore(EnemyType enemyType)
    {
        if (enemyTypePropertiesDict.TryGetValue(enemyType, out EnemyTypeProperties properties))
        {
            int baseScore = properties.baseScore;
            float scoreMultiplier = properties.scoreMultiplier; // Get the score multiplier from EnemyTypeProperties
            int calculatedScore = Mathf.RoundToInt(baseScore * scoreMultiplier);
            return calculatedScore;
        }
        else
        {
            Debug.LogWarning("EnemyType not found in dictionary!");
            return 0;
        }
    }
    public int GetXP(EnemyType enemyType)
    {
        if (enemyTypePropertiesDict.TryGetValue(enemyType, out EnemyTypeProperties properties))
        {
            int baseXP = properties.baseXP;
            int xpMultiplier = properties.xpMultiplier;
            int calculatedXP = Mathf.RoundToInt(baseXP * xpMultiplier);
            return calculatedXP;
        }
        else
        {
            Debug.LogWarning("EnemyType not found in dictionary!");
            return 0;
        }
    }

    public void DestroyAllEnemies()
    {
        scoreManager.ToggleCanScore(false); // Turn off scoring
        player.ToggleXPGain(false);
        enemySpawner.StopSpawner();

        Enemy[] enemies = FindObjectsOfType<Enemy>();
        foreach (Enemy enemy in enemies)
        {
            enemy.DestroyEnemy();
        }

        scoreManager.ToggleCanScore(true); // Turn on scoring
        player.ToggleXPGain(true);
    }

}