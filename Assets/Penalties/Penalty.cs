[System.Serializable]
public abstract class Penalty
{
    public string name;
    public string description;
    public int currentTier;
    public int maxTier;
    public EnemyType enemyType;

    public abstract void ApplyPenaltyEffect();
    public abstract bool CheckMaxTier(); // Implement this method in each derived upgrade class
}
