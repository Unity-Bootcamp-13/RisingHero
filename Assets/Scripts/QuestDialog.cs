using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class QuestDialog : MonoBehaviour
{
    [Header("UI")]
    public Text questDescriptionText;
    public Text progressText;
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

        questDescriptionText.text = GetQuestDescription(questData);
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
                  ", EXP + " + questManager.currentQuest.rewardExp + ", Diamond + " + questManager.currentQuest.rewardJewel);
        // ���⿡ �÷��̾ ���/����ġ/���� �߰��ϴ� �ڵ� ����
        DiamondManager.Instance.GetDiamond(500);


        // ���� ����Ʈ�� ����
        questManager.currentQuest = questManager.GenerateNextQuest();
        questManager.SaveQuestData();

        RefreshQ();
    }
}
