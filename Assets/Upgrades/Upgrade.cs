[System.Serializable]
public abstract class Upgrade
{
    public string name;
    public string description;
    public int currentTier;
    public int maxTier;

    public abstract void ApplyUpgradeEffect();
    public abstract bool CheckMaxTier(); // Implement this method in each derived upgrade class
}
