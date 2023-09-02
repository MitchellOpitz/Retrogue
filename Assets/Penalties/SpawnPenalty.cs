using UnityEngine;

[System.Serializable]
public class SpawnPenalty : Penalty
{
    private bool needsUnlock = false;
    private string defaultName;
    private bool isFirstTime = true;

    public override void ApplyPenaltyEffect()
    {
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
        description = "Increases " + enemyType + " base spawn rate by " + ((currentTier + 1) * 10) + "%.";
    }

    private EnemyType GetRandomEnemyType()
    {
        int randomIndex = Random.Range(0, System.Enum.GetValues(typeof(EnemyType)).Length);
        return (EnemyType)randomIndex;
    }

    private bool CheckUnlock()
    {
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