using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public Quest CurrentQuest => _currentQuest;

    private Quest _currentQuest;
    private int _questIndex = 0;

    private List<Quest> _questList;
    private QuestRepository _repository;
    private QuestRewardService _rewardService;

    private PlayerSaveData _playerData;

    private void Awake()
    {
        _repository = new QuestRepository();
        _rewardService = new QuestRewardService(_playerData);

        _questList = _repository.LoadQuestList();

        LoadCurrentQuest();
    }

    private void LoadCurrentQuest()
    {
        _currentQuest = _repository.LoadQuestProgress();

        if (_currentQuest == null && _questList.Count > 0)
        {
            _currentQuest = _questList[_questIndex];
        }
    }

    public void AddProgress(QuestType type, int amount)
    {
        if (_currentQuest.Type != type || _currentQuest.IsCompleted)
            return;

        _currentQuest.AddProgress(amount);
    }

    public bool TryCompleteQuest()
    {
        if (!_currentQuest.CanComplete()) return false;

        QuestReward reward = _currentQuest.Complete();
        _rewardService.Apply(reward);

        _repository.SaveQuestProgress(_currentQuest);

        GoToNextQuest();

        return true;
    }

    private void GoToNextQuest()
    {
        _questIndex++;
        if (_questIndex >= _questList.Count)
            _questIndex = 0;

        _currentQuest = _questList[_questIndex];
        _repository.SaveQuestProgress(_currentQuest);
    }
}
