using UnityEngine;

public enum QuestType
{
    Kill,
    Upgrade,
    Gacha
}


public class Quest
{
    public int Id { get; private set; }
    public QuestType Type { get; private set; }
    public int GoalValue { get; private set; }
    public int CurrentValue { get; private set; }
    public bool IsCompleted { get; private set; }
    public RewardRange RewardGold { get; private set; }
    public RewardRange RewardDiamond { get; private set; }

    public Quest(int id, QuestType type, int goalValue, RewardRange rewardGold, RewardRange rewardDiamond)
    {
        Id = id; Type = type; GoalValue = goalValue;
        RewardGold = rewardGold; RewardDiamond = rewardDiamond;
        CurrentValue = 0; IsCompleted = false;
    }

    public void ApplyProgress(int currentValue, bool isCompleted)
    {
        CurrentValue = Mathf.Clamp(currentValue, 0, GoalValue);
        IsCompleted = isCompleted && CurrentValue >= GoalValue;
    }

    public void AddProgress(int amount)
    {
        if (IsCompleted) return;
        CurrentValue = Mathf.Clamp(CurrentValue + amount, 0, GoalValue);
    }

    public bool CanComplete()
    {
        return !IsCompleted && CurrentValue >= GoalValue;
    }

    public QuestReward Complete()
    {
        if (!CanComplete())
            throw new System.InvalidOperationException($"퀘스트 완료 조건 불충족(QuestId={Id}, cur={CurrentValue}, goal={GoalValue})");
        IsCompleted = true;
        return new QuestReward(RewardGold.GetRandom(), RewardDiamond.GetRandom());
    }
}
