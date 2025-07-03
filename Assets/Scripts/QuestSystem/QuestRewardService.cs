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

        Debug.Log($"[���� ���� �Ϸ�] Gold: {reward.Gold}, Jewel: {reward.Jewel}");

        // 
    }
}
