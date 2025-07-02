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
        questManager = FindObjectOfType<QuestManager>(); // ������ ������ �ʴ� ���� ����. (���� UI ������Ʈ ���� �� ��.)
        
        if (questManager == null)
        {
            Debug.LogError("QuestManager �νĵ��� ����. Ȯ�� �ٶ�.");
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
            Debug.LogError("����Ʈ ������ ����. Ȯ�� �ٶ�.");
            return;
        }

        questTitle.text = GetQuestDescription(questData);
        progressText.text = $"{questData.currentValue} / {questData.goalValue}";
        completeButton.interactable = IsQuestCompletable(); // ����Ʈ �Ϸ� ��ư ���� �� �ִ��� Ȯ��
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
                return $"���͸� {quest.goalValue}���� óġ";
            case QuestType.Upgrade:
                return $"��� {quest.goalValue}ȸ ������.";
            case QuestType.Gacha:
                return $"�̱� 1ȸ ����.";
            default:
                return "�� �� ���� ����Ʈ�Դϴ�.";
        }
    }


    private void OnCompleteButtonClicked()
    {
        if (!IsQuestCompletable()) return;

        questData.isCompleted = true;
        questManager.SaveQuestData();

        // ���� ����
        Debug.Log("���� ����: Gold + " + questManager.currentQuest.rewardGold +
                   ", Diamond + " + questManager.currentQuest.rewardJewel);

        // ���� ���� ���� ó��
        int gold = Random.Range(questData.rewardGold, questData.rewardGold + 1);
        int jewel = Random.Range(questData.rewardJewel, questData.rewardJewel + 1);

        DiamondManager.Instance.GetDiamond(jewel);

        // ���� ����Ʈ�� ����
        questManager.currentQuest = questManager.GenerateNextQuest();
        questManager.SaveQuestData();

        RefreshQ();
    }
}
