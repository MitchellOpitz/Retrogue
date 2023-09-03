using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField]
    private List<EnemyTypeProperties> enemyTypePropertiesList = new List<EnemyTypeProperties>();

    private Dictionary<EnemyType, EnemyTypeProperties> enemyTypePropertiesDict;
    private ScoreManager scoreManager;
    private PlayerExp playerExp;
    private EnemySpawner enemySpawner;

    private void Awake()
    {
        enemyTypePropertiesDict = new Dictionary<EnemyType, EnemyTypeProperties>();
        foreach (var properties in enemyTypePropertiesList)
        {
            enemyTypePropertiesDict.Add(properties.enemyType, properties);
        }
        scoreManager = FindAnyObjectByType<ScoreManager>();
        playerExp = FindAnyObjectByType<PlayerExp>();
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
        playerExp.ToggleXPGain(false);
        enemySpawner.StopSpawner();

        Enemy[] enemies = FindObjectsOfType<Enemy>();
        foreach (Enemy enemy in enemies)
        {
            enemy.DestroyEnemy();
        }

        scoreManager.ToggleCanScore(true); // Turn on scoring
        playerExp.ToggleXPGain(true);
    }
    public bool IsEnemyTypeUnlocked(EnemyType enemyType)
    {
        if (enemyTypePropertiesDict.TryGetValue(enemyType, out EnemyTypeProperties properties))
        {
            return properties.isUnlocked;
        }
        else
        {
            Debug.LogWarning("EnemyType not found in dictionary!");
            return false; // Return a default value or handle this case as needed
        }
    }

    public void UnlockEnemyType(EnemyType enemyType)
    {
        if (enemyTypePropertiesDict.TryGetValue(enemyType, out EnemyTypeProperties properties))
        {
            properties.isUnlocked = true;
            enemySpawner.UnlockEnemyType(enemyType);
        }
        else
        {
            Debug.LogWarning("EnemyType not found in dictionary!");
            // Handle this case as needed (e.g., show an error message)
        }
    }

    public void UpdateSpawnRate(EnemyType enemyType)
    {
        if (enemyTypePropertiesDict.TryGetValue(enemyType, out EnemyTypeProperties properties))
        {
            properties.spawnMultiplier += 0.1f;
        }
        else
        {
            Debug.LogWarning("EnemyType not found in dictionary!");
            // Handle this case as needed (e.g., show an error message)
        }
    }

    public float GetSpawnRate(EnemyType enemyType)
    {
        if (enemyTypePropertiesDict.TryGetValue(enemyType, out EnemyTypeProperties properties))
        {
            return properties.spawnMultiplier;
        }
        else
        {
            Debug.LogWarning("EnemyType not found in dictionary!");
            return 0;
            // Handle this case as needed (e.g., show an error message)
        }
    }

    public void UpdateMoveSpeedMultiplier(EnemyType enemyType)
    {
        if (enemyTypePropertiesDict.TryGetValue(enemyType, out EnemyTypeProperties properties))
        {
            properties.moveSpeedMultiplier += 0.1f;
        }
        else
        {
            Debug.LogWarning("EnemyType not found in dictionary!");
            // Handle this case as needed (e.g., show an error message)
        }
    }
    public float GetMoveSpeedMultiplier(EnemyType enemyType)
    {
        if (enemyTypePropertiesDict.TryGetValue(enemyType, out EnemyTypeProperties properties))
        {
            return properties.moveSpeedMultiplier;
        }
        else
        {
            Debug.LogWarning("EnemyType not found in dictionary!");
            return 0;
            // Handle this case as needed (e.g., show an error message)
        }
    }

    public void UpdateDamageMultiplier(EnemyType enemyType)
    {
        if (enemyTypePropertiesDict.TryGetValue(enemyType, out EnemyTypeProperties properties))
        {
            properties.damageMultiplier += 0.1f;
        }
        else
        {
            Debug.LogWarning("EnemyType not found in dictionary!");
            // Handle this case as needed (e.g., show an error message)
        }
    }
    public float GetDamageMultiplier(EnemyType enemyType)
    {
        if (enemyTypePropertiesDict.TryGetValue(enemyType, out EnemyTypeProperties properties))
        {
            return properties.damageMultiplier;
        }
        else
        {
            Debug.LogWarning("EnemyType not found in dictionary!");
            return 0;
            // Handle this case as needed (e.g., show an error message)
        }
    }

    public void UpdateHealthMultiplier(EnemyType enemyType)
    {
        if (enemyTypePropertiesDict.TryGetValue(enemyType, out EnemyTypeProperties properties))
        {
            properties.healthMultiplier += 0.1f;
        }
        else
        {
            Debug.LogWarning("EnemyType not found in dictionary!");
            // Handle this case as needed (e.g., show an error message)
        }
    }
    public float GetHealthMultiplier(EnemyType enemyType)
    {
        if (enemyTypePropertiesDict.TryGetValue(enemyType, out EnemyTypeProperties properties))
        {
            return properties.healthMultiplier;
        }
        else
        {
            Debug.LogWarning("EnemyType not found in dictionary!");
            return 0;
            // Handle this case as needed (e.g., show an error message)
        }
    }

}
