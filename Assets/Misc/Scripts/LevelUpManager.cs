using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpManager : MonoBehaviour
{
    public HorizontalLayoutGroup upgradePanelContainer;
    public HorizontalLayoutGroup penaltyPanelContainer;
    public float transitionSpeed = 2f;
    public float transitionDistance = 1300f; // Initial bottom padding value
    public float transitionTargetDistance = 0f; // Target bottom padding value

    private EnemyManager enemyManager;
    private UpgradeManager upgradeManager;
    private PenaltyManager penaltyManager;

    private void Start()
    {
        enemyManager = FindObjectOfType<EnemyManager>();
        upgradeManager = FindObjectOfType<UpgradeManager>();
        penaltyManager = FindObjectOfType<PenaltyManager>();
    }

    public void LevelUp()
    {
        enemyManager.DestroyAllEnemies();

        Bullet[] bullets = FindObjectsOfType<Bullet>();
        foreach (Bullet bullet in bullets)
        {
            bullet.DestroyBullet();
        }

        upgradeManager.StartUpgrades();
        StartUpgradeTransition();
    }

    public void StartUpgradeTransition()
    {
        StartCoroutine(StartTransition(true));
    }

    public void StartPenaltyTransition()
    {
        penaltyManager.StartPenalties();
        StartCoroutine(StartTransition(false));
    }

    private IEnumerator StartTransition(bool isUpgrading)
    {
        HorizontalLayoutGroup layoutGroup = isUpgrading ? upgradePanelContainer : penaltyPanelContainer;
        float startPadding = layoutGroup.padding.bottom;
        float targetPadding = transitionTargetDistance;

        float elapsedTime = 0;

        while (elapsedTime < transitionSpeed)
        {
            layoutGroup.padding = new RectOffset(0, 0, 0, Mathf.RoundToInt(Mathf.Lerp(startPadding, targetPadding, elapsedTime / transitionSpeed)));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        layoutGroup.padding = new RectOffset(0, 0, 0, Mathf.RoundToInt(targetPadding));
    }

    public void EndUpgradeTransition()
    {
        StartCoroutine(EndTransition(true));
    }

    public void EndPenaltyTransition()
    {
        StartCoroutine(EndTransition(false));
    }

    private IEnumerator EndTransition(bool isUpgrading)
    {
        HorizontalLayoutGroup layoutGroup = isUpgrading ? upgradePanelContainer : penaltyPanelContainer;
        float startPadding = layoutGroup.padding.bottom;
        float targetPadding = transitionDistance;

        float elapsedTime = 0;

        while (elapsedTime < transitionSpeed)
        {
            layoutGroup.padding = new RectOffset(0, 0, 0, Mathf.RoundToInt(Mathf.Lerp(startPadding, targetPadding, elapsedTime / transitionSpeed)));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        layoutGroup.padding = new RectOffset(0, 0, 0, Mathf.RoundToInt(targetPadding));
        if (isUpgrading)
        {
            upgradeManager.TogglePanels(false);
            StartPenaltyTransition();
        } else
        {
            penaltyManager.TogglePanels(false);
            FindObjectOfType<CountdownManager>().StartCountdown();
        }
    }
}
