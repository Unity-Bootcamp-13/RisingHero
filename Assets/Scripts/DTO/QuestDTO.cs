[System.Serializable]
public class QuestDTO
{
    public int questId;
    public QuestType questType;
    public int goalValue;
    public int currentValue;
    public bool isCompleted;

    public RewardRange rewardGold;
    public RewardRange rewardJewel;

    public static QuestDTO FromQuest(Quest quest)
    {
        return new QuestDTO
        {
            questId = quest.Id,
            questType = quest.Type,
            goalValue = quest.GoalValue,
            currentValue = quest.CurrentValue,
            isCompleted = quest.IsCompleted,
            rewardGold = quest.RewardGold,
            rewardJewel = quest.RewardDiamond
        };
    }
}
