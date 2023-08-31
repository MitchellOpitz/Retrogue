using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    public EnemyType[] enemyTypes;

    private bool isSpawning = false;
    private EnemyManager enemyManager; // Reference to the EnemyManager script
    private PlayAreaClamp playAreaClamp;

    private void Start()
    {
        enemyManager = FindObjectOfType<EnemyManager>();
        playAreaClamp = FindObjectOfType<PlayAreaClamp>();
    }

    public void StartSpawner()
    {
        if (!isSpawning)
        {
            isSpawning = true;
            foreach (EnemyType enemyType in enemyTypes)
            {
                float spawnInterval = enemyManager.GetSpawnInterval(enemyType);
                StartCoroutine(SpawnEnemies(enemyType, spawnInterval));
            }
        }
    }

    public void StopSpawner()
    {
        isSpawning = false;
        StopAllCoroutines();
    }

    private IEnumerator SpawnEnemies(EnemyType enemyType, float spawnInterval)
    {
        while (isSpawning)
        {
            SpawnEnemy(enemyType); // Spawn the appropriate enemy type
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnEnemy(EnemyType enemyType)
    {
        SpawnDirection randomDirection = (SpawnDirection)Random.Range(0, System.Enum.GetValues(typeof(SpawnDirection)).Length);
        Vector2 spawnLocation = GetRandomSpawnLocation(randomDirection);

        GameObject enemyPrefab = enemyManager.GetEnemyPrefab(enemyType);
        if (enemyPrefab != null)
        {
            GameObject enemyObject = Instantiate(enemyPrefab, spawnLocation, Quaternion.identity);
            Component enemyComponent = null;

            switch (enemyType)
            {
                case EnemyType.TypeA:
                    enemyComponent = enemyObject.GetComponent<EnemyTypeA>();
                    break;
                case EnemyType.TypeB:
                    enemyComponent = enemyObject.GetComponent<EnemyTypeB>();
                    break;
                case EnemyType.TypeC:
                    enemyComponent = enemyObject.GetComponent<EnemyTypeC>();
                    break;
                case EnemyType.TypeD:
                    enemyComponent = enemyObject.GetComponent<EnemyTypeD>();
                    break;
                // Add cases for other enemy types as needed
                default:
                    Debug.LogWarning($"Unknown enemy type: {enemyType}");
                    break;
            }

            if (enemyComponent != null)
            {
                if (enemyComponent is EnemyTypeD)
                {
                    ((EnemyTypeD)enemyComponent).AdjustSpawnPosition();
                }
                enemyComponent.SendMessage("SetSpawnDirection", randomDirection, SendMessageOptions.DontRequireReceiver);
            }
            else
            {
                Debug.LogWarning($"Enemy component not found for type: {enemyType}");
            }
        }
        else
        {
            Debug.LogWarning($"Prefab not found for enemy type: {enemyType}");
        }
    }


    private Vector2 GetRandomSpawnLocation(SpawnDirection direction)
    {
        Vector2 clampMin = playAreaClamp.minClamp;
        Vector2 clampMax = playAreaClamp.maxClamp;

        float spawnX = Random.Range(clampMin.x, clampMax.x);
        float spawnY = Random.Range(clampMin.y, clampMax.y);

        switch (direction)
        {
            case SpawnDirection.Left:
                spawnX = Random.Range(clampMin.x, clampMin.x - 5f);
                break;
            case SpawnDirection.Right:
                spawnX = Random.Range(clampMax.x + 5f, clampMax.x + 10f);
                break;
            case SpawnDirection.Top:
                spawnY = Random.Range(clampMax.y + 5f, clampMax.y + 10f);
                break;
            case SpawnDirection.Bottom:
                spawnY = Random.Range(clampMin.y - 10f, clampMin.y - 5f);
                break;
        }

        return new Vector2(spawnX, spawnY);
    }
}
