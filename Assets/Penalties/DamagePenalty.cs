using UnityEngine;

[System.Serializable]
public class DamagePenalty : Penalty
{
    private bool needsUnlock = false;
    private string defaultName;
    private bool isFirstTime = true;

    public override void ApplyPenaltyEffect()
    {
        Debug.Log("Damage - ApplyPenaltyEffect()");
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
            enemyManager.UpdateDamageMultiplier(enemyType);
        }
    }

    public override bool CheckMaxTier()
    {
        Debug.Log("Damage - CheckMaxTier()");
        if (isFirstTime)
        {
            defaultName = name;
            isFirstTime = false;
        }
        else
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
        Debug.Log("Damage - UpdateDescription()");
        EnemyManager enemyManager = GameObject.FindObjectOfType<EnemyManager>();
        float currentRank = enemyManager.GetDamageMultiplier(enemyType);
        Debug.Log(currentRank);
        description = "Increases " + enemyType + " base damage by " + ((currentRank + 0.1f) * 100) + "%.";
    }

    private EnemyType GetRandomEnemyType()
    {
        Debug.Log("Damage - GetRandomEnemyType()");
        int randomIndex = Random.Range(0, System.Enum.GetValues(typeof(EnemyType)).Length);
        return (EnemyType)randomIndex;
    }

    private bool CheckUnlock()
    {
        Debug.Log("Damage - CheckUnlock()");
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