using UnityEngine;
using TMPro;

public class QuestUI : MonoBehaviour
{
    [SerializeField] private QuestManager questManager;
    [SerializeField] private GameObject questPanel;
    [SerializeField] private TextMeshProUGUI questTitleText;
    [SerializeField] private TextMeshProUGUI questProgressText;

    private void OnEnable()
    {
        questManager.OnQuestChanged += Handle;
        questManager.OnProgressChanged += Handle;
        Handle(questManager.CurrentQuest);
    }

    private void OnDisable()
    {
        questManager.OnQuestChanged -= Handle;
        questManager.OnProgressChanged -= Handle;
    }

    private void Handle(Quest current)
    {
        if (questPanel == null) return;
        if (current == null) { questPanel.SetActive(false); return; }
        questPanel.SetActive(true);
        questTitleText.text = current.Type.ToString();
        questProgressText.text = $"{current.CurrentValue} / {current.GoalValue}";
    }
}