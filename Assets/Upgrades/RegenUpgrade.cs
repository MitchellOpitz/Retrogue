using UnityEngine;

[System.Serializable]
public class RegenUpgrade : Upgrade
{
    public override void ApplyUpgradeEffect()
    {
        // Apply fire speed-related upgrade effect
        currentTier++;
        PlayerManager playerManager = GameObject.FindObjectOfType<PlayerManager>();
        playerManager.regenMultiplier += 0.01f;
    }

    public override bool CheckMaxTier()
    {
        UpdateDescription();
        return currentTier >= maxTier; // Assuming maxTier is a variable defined in your UpgradeManager
    }

    private void UpdateDescription()
    {
        PlayerManager playerManager = GameObject.FindObjectOfType<PlayerManager>();

        string percentUpgrade = ((currentTier + 1)).ToString();
        int regenAmount = Mathf.RoundToInt(playerManager.baseMaxHealth * (playerManager.regenMultiplier + 0.01f));
        description = "Restore " + percentUpgrade + "% of base health at the start of every wave.\nNew regen amount: " + regenAmount;
    }
}