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

        Debug.Log($"[보상 지급 완료] Gold: {reward.Gold}, Jewel: {reward.Diamond}");

        _saveService.Save(playerData);
    }
}