using UnityEngine;

public class QuestRewardService
{
    private PlayerSaveData _playerData;

    public QuestRewardService(PlayerSaveData playerData)
    {
        _playerData = playerData;
    }

    public void Apply(QuestReward reward)
    {
        _playerData.coin += reward.Gold;
        _playerData.diamond += reward.Jewel;

        Debug.Log($"[보상 지급 완료] Gold: {reward.Gold}, Jewel: {reward.Jewel}");

        // 
    }
}
