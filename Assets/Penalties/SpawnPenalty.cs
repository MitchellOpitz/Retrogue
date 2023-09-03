using UnityEngine;

[System.Serializable]
public class SpawnPenalty : Penalty
{
    private bool needsUnlock = false;
    private string defaultName;
    private bool isFirstTime = true;

    public override void ApplyPenaltyEffect()
    {
        Debug.Log("Spawn - ApplyPenaltyEffect()");
        EnemyManager enemyManager = GameObject.FindObjectOfType<EnemyManager>();
        // Apply fire speed-related upgrade effect
        if (needsUnlock)
        {
            enemyManager.UnlockEnemyType(enemyType);
            name = defaultName;
        }
        else
        {
            currentTier++;
            enemyManager.UpdateSpawnRate(enemyType);
        }
    }

    public override bool CheckMaxTier()
    {
        Debug.Log("Spawn - CheckMaxTier()");
        if (isFirstTime)
        {
            defaultName = name;
            isFirstTime = false;
        } else
        {
            name = defaultName;
            needsUnlock = false;
        }

        enemyType = GetRandomEnemyType();
        if (!CheckUnlock())
        {
            return false;
        }

        UpdateDescription();

        return currentTier >= maxTier; // Assuming maxTier is a variable defined in your UpgradeManager
    }

    private void UpdateDescription()
    {
        Debug.Log("Spawn - UpdateDescription()");
        EnemyManager enemyManager = GameObject.FindObjectOfType<EnemyManager>();
        float currentRank = enemyManager.GetSpawnRate(enemyType);
        Debug.Log(currentRank);
        description = "Increases " + enemyType + " base spawn rate by " + ((currentRank + 0.1f) * 100) + "%.";
    }

    private EnemyType GetRandomEnemyType()
    {
        Debug.Log("Spawn - GetRandomEnemyType()");
        int randomIndex = Random.Range(0, System.Enum.GetValues(typeof(EnemyType)).Length);
        return (EnemyType)randomIndex;
    }

    private bool CheckUnlock()
    {
        Debug.Log("Spawn - CheckUnlock()");
        EnemyManager enemyManager = GameObject.FindObjectOfType<EnemyManager>();

        if (!enemyManager.IsEnemyTypeUnlocked(enemyType))
        {
            needsUnlock = true;
            name = "Unlock " + enemyType;
            description = "Causes a new enemy type to spawn.";
            return false;
        }

        return true;
    }
}