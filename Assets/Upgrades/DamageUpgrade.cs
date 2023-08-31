using UnityEngine;

[System.Serializable]
public class DamageUpgrade : Upgrade
{
    public override void ApplyUpgradeEffect()
    {
        // Apply fire speed-related upgrade effect
        currentTier++;
        UpgradeManager upgradeManager = GameObject.FindObjectOfType<UpgradeManager>();
        upgradeManager.damageMultiplier = currentTier * 0.1f;

    }

    public override bool CheckMaxTier()
    {
        UpdateDescription();
        return currentTier >= maxTier; // Assuming maxTier is a variable defined in your UpgradeManager
    }

    private void UpdateDescription()
    {
        description = "Increase base damage by " + ((currentTier + 1) * 10) + "%.";
    }
}