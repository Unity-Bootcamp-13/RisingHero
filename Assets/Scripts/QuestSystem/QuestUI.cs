using UnityEngine;
using TMPro;

public class QuestUI : MonoBehaviour
{
    [SerializeField] private QuestManager questManager;
    [SerializeField] private GameObject questPanel;
    [SerializeField] private TextMeshProUGUI questTitleText;
    [SerializeField] private TextMeshProUGUI questProgressText;

    private Quest quest;


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
        Quest current = questManager.CurrentQuest;

        if (current == null)
        {
            questPanel.SetActive(false);
            return;
        }


        Debug.Log($"[UpdateQuestUI] TitleText before: {questTitleText.text}");
        Debug.Log($"[UpdateQuestUI] CurrentQuest Type: {current.Type}");

        questTitleText.text = current.Type.ToString();
        questProgressText.text = $"{current.CurrentValue} / {current.GoalValue}";

        Debug.Log($"[UpdateQuestUI] TitleText after: {questTitleText.text}");
    }

    public void SetQuest(Quest quest)
    {
        this.quest = quest;
        UpdateQuestUI();
    }

}