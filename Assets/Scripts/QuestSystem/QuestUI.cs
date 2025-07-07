using UnityEngine;
using TMPro;

public class QuestUI : MonoBehaviour
{
    [SerializeField] private QuestManager questManager;
    [SerializeField] private GameObject questPanel;
    [SerializeField] private TextMeshProUGUI questTitleText;
    [SerializeField] private TextMeshProUGUI questProgressText;

    public Quest quest;

    public QuestType questType;

    private void Start()
    {
        SetQuest(questManager.CurrentQuest);
    }

    private void Update()
    {
        UpdateQuestUI();
    }

    public void UpdateQuestUI()
    {
        if (questManager.CurrentQuest == null)
        {
            questPanel.SetActive(false);
            return;
        }
        questPanel.SetActive(true);
        questTitleText.text = questType.ToString();

        questProgressText.text = $"{quest.CurrentValue} / {quest.GoalValue}";
    }
    public void SetQuest(Quest quest)
    {
        this.quest = quest;
        UpdateQuestUI();
    }

}