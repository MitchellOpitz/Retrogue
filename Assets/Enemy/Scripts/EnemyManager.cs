using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField]
    private List<EnemyTypeProperties> enemyTypePropertiesList = new List<EnemyTypeProperties>();

    private Dictionary<EnemyType, EnemyTypeProperties> enemyTypePropertiesDict;

    private void Awake()
    {
        enemyTypePropertiesDict = new Dictionary<EnemyType, EnemyTypeProperties>();
        foreach (var properties in enemyTypePropertiesList)
        {
            enemyTypePropertiesDict.Add(properties.enemyType, properties);
        }
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
}
