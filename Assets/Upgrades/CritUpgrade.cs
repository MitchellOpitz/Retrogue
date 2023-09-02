using UnityEngine;

[System.Serializable]
public class CritUpgrade : Upgrade
{
    public override void ApplyUpgradeEffect()
    {
        // Apply fire speed-related upgrade effect
        currentTier++;
        PlayerManager playerManager = GameObject.FindObjectOfType<PlayerManager>();
        playerManager.critChance += 0.05f;
    }

    public override bool CheckMaxTier()
    {
        UpdateDescription();
        return currentTier >= maxTier; // Assuming maxTier is a variable defined in your UpgradeManager
    }

    private void UpdateDescription()
    {
        PlayerManager playerManager = GameObject.FindObjectOfType<PlayerManager>();

        string percentUpgrade = ((currentTier + 1) * 5).ToString();
        description = "Increase base crit rate by " + percentUpgrade + "%.";
    }
}