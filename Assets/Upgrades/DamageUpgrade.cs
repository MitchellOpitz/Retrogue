using UnityEngine;

[System.Serializable]
public class DamageUpgrade : Upgrade
{

    public override void ApplyUpgradeEffect()
    {
        // Apply fire speed-related upgrade effect
        currentTier++;
        PlayerManager playerManager = GameObject.FindObjectOfType<PlayerManager>();
        playerManager.damageMultiplier = currentTier * 0.1f;
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
        int damageAmount = Mathf.RoundToInt(playerManager.baseDamage * (1 + (playerManager.damageMultiplier + .1f)));
        description = "Increase base damage by " + percentUpgrade + "%.  \nNew damage amount: " + damageAmount;
    }
}