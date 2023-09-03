using UnityEngine;

[System.Serializable]
public class EnemyTypeProperties
{
    public EnemyType enemyType;
    public GameObject enemyPrefab;

    public float baseSpawnRate;
    public float spawnMultiplier;

    public int baseScore;
    public int scoreMultiplier;

    public int baseXP;
    public int xpMultiplier;

    public float damageMultiplier;
    public float moveSpeedMultiplier;
    public float healthMultiplier;

    public bool isUnlocked;

    public int damageMultiplierRank;
    public int moveSpeedMultiplierRank;
    public int spawnRateMultiplierRank;
    public int healthMultiplierRank;

    public string movementPattern;
}
