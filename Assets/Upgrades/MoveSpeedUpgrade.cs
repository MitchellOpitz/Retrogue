using UnityEngine;

[System.Serializable]
public class MoveSpeedUpgrade : Upgrade
{
    public override void ApplyUpgradeEffect()
    {
        // Apply fire speed-related upgrade effect
        currentTier++;
        PlayerMovement playerMovement = GameObject.FindObjectOfType<PlayerMovement>();
        playerMovement.UpgradeMoveSpeed();
    }

    public override bool CheckMaxTier()
    {
        UpdateDescription();
        return currentTier >= maxTier; // Assuming maxTier is a variable defined in your UpgradeManager
    }

    private void UpdateDescription()
    {
        PlayerMovement playerMovement = GameObject.FindObjectOfType<PlayerMovement>();

        string percentUpgrade = ((currentTier + 1) * 10).ToString();
        float speedAmount = playerMovement.baseMoveSpeed * (1 + playerMovement.moveSpeedMultiplier + 0.1f);
        description = "Increase base move speed by " + percentUpgrade + "%.\nNew speed: " + speedAmount;
    }
}