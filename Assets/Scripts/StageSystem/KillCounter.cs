using UnityEngine;
using UnityEngine.UI;


public class KillCounter : MonoBehaviour
{
    [SerializeField] public Quest quest;
    [SerializeField] private QuestManager questManager;
    public QuestType Type;

    [Header("UI")]
    [SerializeField] private Slider killSlider;

    [Header("설정")]
    [SerializeField] private int killGoal = 80;

    private int currentKills = 0;

    public System.Action OnClear;
    public System.Action OnFail;

    private bool isActive = false;

    public void StartCount(int goal)
    {
        currentKills = 0;
        killGoal = goal;
        isActive = true;
        UpdateUI();
    }

    public void SetQuest(Quest quest)
    {
        this.quest = quest;
    }

    public void AddKill()
    {
        if (Type == QuestType.Kill)
        {
            SetQuest(questManager.CurrentQuest);
            quest.AddProgress(1);
        }

        if (!isActive) return;

        currentKills++;
        UpdateUI();
        

        if (currentKills >= killGoal)
        {
            isActive = false;
            Debug.Log("[KillCounter] 목표 달성");
            OnClear.Invoke();
        }
    }

    public void EndCountByFail()
    {
        if (!isActive) return;

        isActive = false;
        Debug.Log("[KillCounter] 실패");
        OnFail.Invoke();
    }

    private void UpdateUI()
    {
        if (killSlider != null)
        {
            killSlider.value = Mathf.Clamp01((float)currentKills / killGoal);
        }
    }
}
