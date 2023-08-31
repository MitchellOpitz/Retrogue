using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Types to Spawn")]
    public EnemyType[] enemyTypes;

    [Header("Enemy Type A")]
    public GameObject TypeAPrefab;
    public float TypeABaseSpawnRate;

    [Header("Enemy Type B")]
    public GameObject TypeBPrefab;
    public float TypeBBaseSpawnRate;

    [Header("Enemy Type C")]
    public GameObject TypeCPrefab;
    public float TypeCBaseSpawnRate;

    [Header("Enemy Type D")]
    public GameObject TypeDPrefab;
    public float TypeDBaseSpawnRate;

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
                float spawnInterval = 0f;

                switch (enemyType)
                {
                    case EnemyType.TypeA:
                        spawnInterval = TypeABaseSpawnRate;
                        break;
                    case EnemyType.TypeB:
                        spawnInterval = TypeBBaseSpawnRate;
                        break;
                    case EnemyType.TypeC:
                        spawnInterval = TypeCBaseSpawnRate;
                        break;
                    case EnemyType.TypeD:
                        spawnInterval = TypeDBaseSpawnRate;
                        break;
                }
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

        GameObject enemyObject = null;

        switch (enemyType)
        {
            case EnemyType.TypeA:
                enemyObject = Instantiate(TypeAPrefab, spawnLocation, Quaternion.identity);
                EnemyTypeA enemyA = enemyObject.GetComponent<EnemyTypeA>();
                enemyA.SetSpawnDirection(randomDirection);
                break;

            case EnemyType.TypeB:
                enemyObject = Instantiate(TypeBPrefab, spawnLocation, Quaternion.identity);
                EnemyTypeB enemyB = enemyObject.GetComponent<EnemyTypeB>();
                enemyB.SetSpawnDirection(randomDirection);
                break;

            case EnemyType.TypeC:
                enemyObject = Instantiate(TypeCPrefab, spawnLocation, Quaternion.identity);
                EnemyTypeC enemyC = enemyObject.GetComponent<EnemyTypeC>();
                enemyC.SetSpawnDirection(randomDirection);
                break;

            case EnemyType.TypeD:
                enemyObject = Instantiate(TypeDPrefab, spawnLocation, Quaternion.identity);
                EnemyTypeD enemyD = enemyObject.GetComponent<EnemyTypeD>();
                enemyD.SetSpawnDirection(randomDirection);
                enemyD.AdjustSpawnPosition();
                break;

            // Add cases for other enemy types as needed

            default:
                Debug.LogWarning($"Unknown enemy type: {enemyType}");
                break;
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
