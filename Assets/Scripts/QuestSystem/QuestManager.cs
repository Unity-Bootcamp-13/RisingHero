using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public Quest CurrentQuest => _currentQuest;

    [SerializeField] private Quest _currentQuest;
    [SerializeField] private List<Quest> _questList;
    [SerializeField] private QuestRepository _repository;
    [SerializeField] private QuestRewardService _rewardService;
    [SerializeField] private QuestUI questUI;

    private ISaveService _saveService;

    private void Awake()
    {
        _saveService = new JsonSaveService();
        var playerData = _saveService.Load();

        _repository = new QuestRepository();

        _questList = _repository.LoadQuestList();
        LoadCurrentQuest(playerData);
    }

    private void LoadCurrentQuest(PlayerSaveData playerData)
    {
        if (_questList.Count == 0)
        {
            return;
        }

        if (playerData.currentQuestId < 0 || !_questList.Exists(q => q.Id == playerData.currentQuestId))
        {
            _currentQuest = _questList[0];
            playerData.currentQuestId = _currentQuest.Id;
            _saveService.Save(playerData);
        }
        else
        {
            _currentQuest = _questList.Find(q => q.Id == playerData.currentQuestId);
        }
    }

    public void AddProgress(QuestType type, int amount)
    {
        if (_currentQuest.Type != type || _currentQuest.IsCompleted)
            return;

        _currentQuest.AddProgress(amount);
    }

    public void TryCompleteQuest()
    {
        if (!_currentQuest.CanComplete()) return;

        QuestReward reward = _currentQuest.Complete();
        _rewardService = new QuestRewardService(_saveService);
        _rewardService.Apply(reward);

        GoToNextQuest();

        return;
    }

    private void GoToNextQuest()
    {
        int currentIndex = _questList.FindIndex(q => q.Id == _currentQuest.Id);
        int nextIndex = (currentIndex + 1) % _questList.Count;

        _currentQuest = _questList[nextIndex];

        var playerData = _saveService.Load();
        playerData.currentQuestId = _currentQuest.Id;
        _saveService.Save(playerData);
        questUI.UpdateQuestUI();
    }
}
