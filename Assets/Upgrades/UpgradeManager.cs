using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public List<DamageUpgrade> damageUpgrades = new List<DamageUpgrade>();

    public GameObject upgradePanel1;
    public GameObject upgradePanel2;
    public GameObject upgradePanel3;

    private void Start()
    {
        TogglePanels(false);
    }

    public void SelectUpgrades()
    {
        List<Upgrade> selectedUpgrades = new List<Upgrade>();
        for (int i = 0; i < 3; i++) // Choose 3 upgrades
        {
            Upgrade randomUpgrade = GetRandomUpgrade();
            selectedUpgrades.Add(randomUpgrade);
        }

        DisplayUpgrades(selectedUpgrades);
    }

    private Upgrade GetRandomUpgrade()
    {
        return damageUpgrades[Random.Range(0, damageUpgrades.Count)];
    }

    private void DisplayUpgrades(List<Upgrade> upgrades)
    {
        for (int i = 0; i < upgrades.Count; i++)
        {
            // Assuming upgrades[i] is the UI panel and its children are as you described
            GameObject uiPanel = transform.GetChild(i).gameObject;

            // Assuming uiPanel.transform.GetChild(0) is the name text, uiPanel.transform.GetChild(1) is the description text,
            // and uiPanel.transform.GetChild(2) is the button to activate the upgrade
            TMPro.TextMeshProUGUI nameText = uiPanel.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>();
            TMPro.TextMeshProUGUI descriptionText = uiPanel.transform.GetChild(1).GetComponent<TMPro.TextMeshProUGUI>();
            UnityEngine.UI.Button activateButton = uiPanel.transform.GetChild(2).GetComponent<UnityEngine.UI.Button>();

            nameText.text = upgrades[i].name;
            descriptionText.text = upgrades[i].description;

            // Attach an event listener to the button to call the ApplyUpgradeEffect method
            int index = i; // Capture the value to avoid closures issues
            activateButton.onClick.AddListener(() => ApplyUpgradeEffect(upgrades[index]));
        }
    }


    public void ApplyUpgradeEffect(Upgrade upgrade)
    {
        upgrade.ApplyUpgradeEffect();
        TogglePanels(false);
    }

    public void StartUpgrades()
    {
        TogglePanels(true);
        SelectUpgrades();
    }

    private void TogglePanels(bool value)
    {
        upgradePanel1.SetActive(value);
        upgradePanel2.SetActive(value);
        upgradePanel3.SetActive(value);
    }
}
