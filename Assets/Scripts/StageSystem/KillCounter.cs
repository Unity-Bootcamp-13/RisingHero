using UnityEngine;
using UnityEngine.UI;


public class KillCounter : MonoBehaviour
{
    [SerializeField] public Quest quest;
    [SerializeField] private QuestManager questManager;
    public QuestType Type;
    public PlayerSaveData psd;

    private ISaveService saveService;

    [Header("UI")]
    [SerializeField] private Slider killSlider;

    [Header("설정")]
    [SerializeField] private int killGoal = 80;

    private int currentKills = 0;

    public System.Action OnClear;
    public System.Action OnFail;

    private bool isActive = false;

    public void SetQuest(Quest quest)
    {
        this.quest = quest;
    }

    private void Awake()
    {
        saveService = new JsonSaveService();
        psd = saveService.Load();
    }

    public void StartCount(int goal)
    {
        currentKills = 0;
        killGoal = goal;
        isActive = true;
        UpdateUI();
    }

    public void AddKill()
    {
        saveService = new JsonSaveService();
        if (psd.currentQuestId == 1)
        {
            SetQuest(questManager.CurrentQuest);
            quest.AddProgress(1);  // 이 시점에만 퀘스트 진행도 증가
        }

        if (!isActive)
            return;

        currentKills++;
        UpdateUI();

        if (currentKills >= killGoal)
        {
            isActive = false;
            OnClear?.Invoke();
        }
    }

    public void EndCountByFail()
    {
        if (!isActive) return;

        isActive = false;
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
