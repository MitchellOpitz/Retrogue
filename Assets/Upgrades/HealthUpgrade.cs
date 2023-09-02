using UnityEngine;

[System.Serializable]
public class HealthUpgrade : Upgrade
{
    public override void ApplyUpgradeEffect()
    {
        // Apply fire speed-related upgrade effect
        currentTier++;
        PlayerManager playerManager = GameObject.FindObjectOfType<PlayerManager>();
        playerManager.UpgradeMaxHealth();
    }

    public override bool CheckMaxTier()
    {
        UpdateDescription();
        return currentTier >= maxTier; // Assuming maxTier is a variable defined in your UpgradeManager
    }

    private void UpdateDescription()
    {
        PlayerManager playerManager = GameObject.FindObjectOfType<PlayerManager>();

        string percentUpgrade = ((currentTier + 1) * 10).ToString();
        int healthAmount = Mathf.RoundToInt(playerManager.baseMaxHealth * (playerManager.healthMultiplier + 0.1f));
        description = "Increase base health by " + percentUpgrade + "%.\nNew health amount: " + healthAmount;
    }
}