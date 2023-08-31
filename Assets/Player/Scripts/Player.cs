using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    public TextMeshProUGUI xpText;
    public int baseXPToLevel = 20;

    private int currentXP;
    private int currentLevel;
    private int xpToLevel;
    private float levelUpExponent = 1.5f;
    private bool canGainXP;

    private UpgradeManager upgradeManager;
    private EnemyManager enemyManager;

    private void Start()
    {
        currentXP = 0;
        currentLevel = 1;
        canGainXP = true;
        CalculateXPToLevel();

        enemyManager = FindObjectOfType<EnemyManager>();
        upgradeManager = FindObjectOfType<UpgradeManager>();
    }

    public void GainXP(EnemyType enemyType)
    {
        if (canGainXP)
        {
            currentXP += enemyManager.GetXP(enemyType);
            UpdateXPUI();
            CheckLevelUp();
        }
    }

    private void UpdateXPUI()
    {
        xpText.text = "Experience: " + currentXP + " / " + xpToLevel;
    }

    private void CheckLevelUp()
    {
        if (currentXP >= xpToLevel)
        {
            currentLevel++;
            currentXP = 0;
            CalculateXPToLevel();
            enemyManager.DestroyAllEnemies();
            upgradeManager.StartUpgrades();
        }
    }

    private void CalculateXPToLevel()
    {
        xpToLevel = Mathf.RoundToInt(baseXPToLevel * Mathf.Pow(currentLevel, levelUpExponent));
        UpdateXPUI();
    }

    public void ToggleXPGain(bool value)
    {
        canGainXP = value;
    }
}
