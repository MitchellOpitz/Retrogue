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
}
