[System.Serializable]
public class Upgrade
{
    public string name;
    public string description;

    public virtual void ApplyUpgradeEffect()
    {
        // Default implementation or leave it empty
    }
}
