using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestDialog : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI questTitle;
    public TextMeshProUGUI progressText;
    public Button completeButton;

    private QuestManager questManager;
    private QuestData questData;

    private IDiamondManager diamondManager;

    private void Start()
    {
        questManager = FindObjectOfType<QuestManager>(); // 지금은 사용되지 않는 것이 정상. (아직 UI 컴포넌트 연결 안 함.)
        
        if (questManager == null)
        {
            Debug.LogError("QuestManager 인식되지 않음. 확인 바람.");
            enabled = false; 
            return;
        }
        
        questData = questManager.currentQuest;

        if (completeButton != null)
        {
            completeButton.onClick.AddListener(OnCompleteButtonClicked);
        }

        UpdateUI();
    }

    public void RefreshQ()
    {
        questData = questManager.currentQuest;
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (questData == null)
        {
            Debug.LogError("퀘스트 데이터 없음. 확인 바람.");
            return;
        }

        questTitle.text = GetQuestDescription(questData);
        progressText.text = $"{questData.currentValue} / {questData.goalValue}";
        completeButton.interactable = IsQuestCompletable(); // 퀘스트 완료 버튼 누를 수 있는지 확인
    }

    private bool IsQuestCompletable()
    {
        return questData.currentValue >= questData.goalValue && !questData.isCompleted;
    }

    private string GetQuestDescription(QuestData quest)
    {
        switch (quest.questType)
        {
            case QuestType.Kill:
                return $"몬스터를 {quest.goalValue}마리 처치";
            case QuestType.Upgrade:
                return $"장비를 {quest.goalValue}회 레벨업.";
            case QuestType.Gacha:
                return $"뽑기 1회 진행.";
            default:
                return "알 수 없는 퀘스트입니다.";
        }
    }


    private void OnCompleteButtonClicked()
    {
        if (!IsQuestCompletable()) return;

        questData.isCompleted = true;
        questManager.SaveQuestData();

        // 보상 지급
        Debug.Log("보상 지급: Gold + " + questManager.currentQuest.rewardGold +
                   ", Diamond + " + questManager.currentQuest.rewardJewel);

        // 골드와 보석 보상 처리
        int gold = Random.Range(questData.rewardGold, questData.rewardGold + 1);
        int jewel = Random.Range(questData.rewardJewel, questData.rewardJewel + 1);

        DiamondManager.Instance.GetDiamond(jewel);

        // 다음 퀘스트로 갱신
        questManager.currentQuest = questManager.GenerateNextQuest();
        questManager.SaveQuestData();

        RefreshQ();
    }
}
