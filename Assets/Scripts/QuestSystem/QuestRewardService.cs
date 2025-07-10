using UnityEngine;

public class QuestRewardService
{
    private readonly ISaveService _saveService;

    public QuestRewardService(ISaveService saveService)
    {
        _saveService = saveService;
    }

    public void Apply(QuestReward reward)
    {
        var playerData = _saveService.Load();
        playerData.coin += reward.Gold;
        playerData.diamond += reward.Diamond;
        _saveService.Save(playerData);
    }
}