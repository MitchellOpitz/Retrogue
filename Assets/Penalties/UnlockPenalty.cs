using UnityEngine;

public class UnlockPenalty : Penalty
{
    public override void ApplyPenaltyEffect()
    {
        // Apply fire speed-related upgrade effect
        EnemyManager enemyManager = GameObject.FindObjectOfType<EnemyManager>();
        enemyManager.UnlockEnemyType(enemyType);
    }

    private void UpdateDescription()
    {
        description = "Increases " + enemyType + " spawn rate by " + ((currentTier + 1) * 10) + "%.";
    }

    public void ChangeType(EnemyType type)
    {
        enemyType = type;
    }

    public override bool CheckMaxTier()
    {
        return currentTier >= maxTier; // Assuming maxTier is a variable defined in your UpgradeManager
    }
}
