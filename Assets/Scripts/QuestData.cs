[System.Serializable]
public class QuestData
{
    public int questId;
    public QuestType questType; 
    public int goalValue;
    public int currentValue;
    public int rewardGold;
    public int rewardJewel;
    public bool isCompleted;
}

public enum QuestType
{
    Kill,
    Upgrade,
    Gacha
}
