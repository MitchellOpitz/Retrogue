using UnityEngine;

[System.Serializable]
public class FireSpeedUpgrade : Upgrade
{
    public override void ApplyUpgradeEffect()
    {
        // Apply fire speed-related upgrade effect
        currentTier++;
        PlayerShoot playerShoot = GameObject.FindObjectOfType<PlayerShoot>();
        playerShoot.UpdateMultiplier(currentTier * 0.1f);

    }

    public override bool CheckMaxTier()
    {
        UpdateDescription();
        return currentTier >= maxTier; // Assuming maxTier is a variable defined in your UpgradeManager
    }

    private void UpdateDescription()
    {
        PlayerShoot playerShoot = GameObject.FindObjectOfType<PlayerShoot>();

        string percentUpgrade = ((currentTier + 1) * 10).ToString();
        float shotSpeed = playerShoot.baseShootSpeed * (1 - (playerShoot.shootMultiplier + 0.1f)); // 10% faster per upgrade level
        shotSpeed = Mathf.Round(shotSpeed * 100) / 100; // Round to two decimal places
        description = "Increase base damage by " + percentUpgrade + "%.  \nNew shot speed: " + shotSpeed;
    }

}