using UnityEngine;

[System.Serializable]
public class SpawnPenalty : Penalty
{
    public override void ApplyPenaltyEffect()
    {
        // Apply fire speed-related upgrade effect
        currentTier++;
    }

    public override bool CheckMaxTier()
    {
        UpdateDescription();
        enemyType = GetRandomEnemyType();
        return currentTier >= maxTier; // Assuming maxTier is a variable defined in your UpgradeManager
    }

    private void UpdateDescription()
    {
        description = "Increases " + enemyType + " spawn rate by " + ((currentTier + 1) * 10) + "%.";
    }
    private EnemyType GetRandomEnemyType()
    {
        int randomIndex = Random.Range(0, System.Enum.GetValues(typeof(EnemyType)).Length);
        return (EnemyType)randomIndex;
    }
}