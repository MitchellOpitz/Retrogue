using UnityEngine;

[System.Serializable]
public class MultishotUpgrade : Upgrade
{

    public override void ApplyUpgradeEffect()
    {
        // Apply fire speed-related upgrade effect
        currentTier++;
        PlayerShoot playerShoot = GameObject.FindObjectOfType<PlayerShoot>();
        playerShoot.multishot++;
    }

    public override bool CheckMaxTier()
    {
        UpdateDescription();
        return currentTier >= maxTier; // Assuming maxTier is a variable defined in your UpgradeManager
    }

    private void UpdateDescription()
    {
        PlayerShoot playerShoot = GameObject.FindObjectOfType<PlayerShoot>();

        int multishotAmount = playerShoot.multishot + 1;
        description = "Fires 1 additional bullet per shot.\nTotal bullets per shot: " + multishotAmount;
    }
}