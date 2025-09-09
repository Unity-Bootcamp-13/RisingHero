using System;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public Quest CurrentQuest => _currentQuest;
    public ISaveService SaveService => _saveService; // 외부에서 공유 사용

    [SerializeField] private bool loopQuests = true;
    [SerializeField] private Quest _currentQuest;
    [SerializeField] private List<Quest> _questList;
    [SerializeField] private QuestRepository _repository;
    [SerializeField] private QuestRewardService _rewardService;

    public event Action<Quest> OnQuestChanged;
    public event Action<Quest> OnProgressChanged;

    private ISaveService _saveService;
    private bool _completing;

    private void Awake()
    {
        _saveService = new JsonSaveService();
        var playerData = _saveService.Load();

        _repository = _repository ?? new QuestRepository();
        _questList = _repository.LoadQuestList();
        LoadCurrentQuest(playerData);
    }
    private void OnEnable()
    {
        GameEvents.EnemyKilled += OnEnemyKilled;
    }

    private void OnDisable()
    {
        GameEvents.EnemyKilled -= OnEnemyKilled;
    }

    private void OnEnemyKilled()
    {
        // 현재 퀘스트가 Kill 타입일 때만 내부에서 처리됨
        AddProgress(QuestType.Kill, 1);
    }
    private void LoadCurrentQuest(PlayerSaveData playerData)
    {
        if (_questList == null || _questList.Count == 0) { _currentQuest = null; OnQuestChanged?.Invoke(null); return; }

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

        // 저장된 진행치 반영
        var prog = playerData.questProgress.Find(p => p.questId == _currentQuest.Id);
        if (prog != null) _currentQuest.ApplyProgress(prog.currentValue, prog.isCompleted);
        OnQuestChanged?.Invoke(_currentQuest);
        OnProgressChanged?.Invoke(_currentQuest);
    }

    private void SaveProgress()
    {
        var data = _saveService.Load();
        var prog = data.questProgress.Find(p => p.questId == _currentQuest.Id);
        if (prog == null)
        {
            prog = new QuestProgressData { questId = _currentQuest.Id };
            data.questProgress.Add(prog);
        }
        prog.currentValue = _currentQuest.CurrentValue;
        prog.isCompleted = _currentQuest.IsCompleted;
        _saveService.Save(data);
    }

    public void AddProgress(QuestType type, int amount)
    {
        if (_currentQuest == null) return;
        if (_currentQuest.Type != type || _currentQuest.IsCompleted) return;

        _currentQuest.AddProgress(amount);
        SaveProgress();
        OnProgressChanged?.Invoke(_currentQuest);
    }

    public void TryCompleteQuest()
    {
        if (_currentQuest == null) return;
        if (_completing) return; // 중복 방지
        if (!_currentQuest.CanComplete()) return;

        _completing = true;
        try
        {
            QuestReward reward = _currentQuest.Complete();
            _rewardService = _rewardService ?? new QuestRewardService(_saveService);
            _rewardService.Apply(reward);
            SaveProgress();
            GoToNextQuest();
        }
        finally
        {
            _completing = false;
        }
    }

    private void GoToNextQuest()
    {
        if (_questList == null || _questList.Count == 0) { _currentQuest = null; OnQuestChanged?.Invoke(null); return; }

        int currentIndex = _questList.FindIndex(q => q.Id == _currentQuest.Id);
        int nextIndex = currentIndex + 1;

        if (nextIndex >= _questList.Count)
        {
            if (!loopQuests)
            {
                // 더 이상 퀘스트 없음
                _currentQuest = null;
                var data = _saveService.Load();
                data.currentQuestId = -1;
                _saveService.Save(data);
                OnQuestChanged?.Invoke(null);
                return;
            }
            nextIndex = 0;
        }

        _currentQuest = _questList[nextIndex];
        var playerData = _saveService.Load();
        playerData.currentQuestId = _currentQuest.Id;
        _saveService.Save(playerData);

        // 새 퀘스트의 저장된 진행치 적용
        var prog = playerData.questProgress.Find(p => p.questId == _currentQuest.Id);
        if (prog != null) _currentQuest.ApplyProgress(prog.currentValue, prog.isCompleted);
        else _currentQuest.ApplyProgress(0, false);

        OnQuestChanged?.Invoke(_currentQuest);
        OnProgressChanged?.Invoke(_currentQuest);
    }
}