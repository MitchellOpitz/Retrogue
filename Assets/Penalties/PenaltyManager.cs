using System.Collections.Generic;
using UnityEngine;

public class PenaltyManager : MonoBehaviour
{
    public List<SpawnPenalty> spawnPenalty = new List<SpawnPenalty>();
    public List<MoveSpeedPenalty> moveSpeedPenalty = new List<MoveSpeedPenalty>();
    public List<DamagePenalty> damagePenalty = new List<DamagePenalty>();

    public GameObject penaltyPanel1;
    public GameObject penaltyPanel2;
    public GameObject penaltyPanel3;

    private void Start()
    {
        TogglePanels(false);
    }

    public void SelectPenalties()
    {
        List<Penalty> selectedPenalties = new List<Penalty>();
        for (int i = 0; i < 3; i++) // Choose 3 penalties
        {
            Penalty randomPenalty = GetRandomPenalty();
            while (randomPenalty == null)
            {
                randomPenalty = GetRandomPenalty();
            }
            selectedPenalties.Add(randomPenalty);
        }

        DisplayPenalties(selectedPenalties);
    }

    private Penalty GetRandomPenalty()
    {
        int randomNumber = Random.Range(1, 4); // Generate a random number between 1 and 3

        Penalty penalty = null;

        switch (randomNumber)
        {
            case 1:
                penalty = spawnPenalty.Find(p => p is SpawnPenalty && !p.CheckMaxTier());
                break;
            case 2:
                penalty = moveSpeedPenalty.Find(p => p is MoveSpeedPenalty && !p.CheckMaxTier());
                break;
            case 3:
                penalty = damagePenalty.Find(p => p is DamagePenalty && !p.CheckMaxTier());
                break;
        }

        return penalty;
    }

    private void DisplayPenalties(List<Penalty> penalties)
    {
        for (int i = 0; i < penalties.Count; i++)
        {
            // Assuming penalties[i] is the UI panel and its children are as you described
            GameObject uiPanel = transform.GetChild(i).gameObject;

            // Assuming uiPanel.transform.GetChild(0) is the name text, uiPanel.transform.GetChild(1) is the description text,
            // and uiPanel.transform.GetChild(2) is the button to apply the penalty
            TMPro.TextMeshProUGUI nameText = uiPanel.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>();
            TMPro.TextMeshProUGUI descriptionText = uiPanel.transform.GetChild(1).GetComponent<TMPro.TextMeshProUGUI>();
            UnityEngine.UI.Button applyButton = uiPanel.transform.GetChild(2).GetComponent<UnityEngine.UI.Button>();

            nameText.text = penalties[i].name;
            descriptionText.text = penalties[i].description;

            // Attach an event listener to the button to call the ApplyPenaltyEffect method
            int index = i; // Capture the value to avoid closures issues
            applyButton.onClick.RemoveAllListeners();
            applyButton.onClick.AddListener(() => ApplyPenaltyEffect(penalties[index]));
        }
    }

    public void ApplyPenaltyEffect(Penalty penalty)
    {
        penalty.ApplyPenaltyEffect();
        FindObjectOfType<LevelUpManager>().EndPenaltyTransition();
    }

    public void StartPenalties()
    {
        TogglePanels(true);
        SelectPenalties();
    }

    public void TogglePanels(bool value)
    {
        penaltyPanel1.SetActive(value);
        penaltyPanel2.SetActive(value);
        penaltyPanel3.SetActive(value);
    }
}
