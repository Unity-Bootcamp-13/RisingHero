using UnityEngine;
using UnityEngine.UI;

public class KillCounter : MonoBehaviour
{
    [Header("UI")][SerializeField] private Slider killSlider;
    [Header("¼³Á¤")][SerializeField] private int killGoal = 80;

    private int currentKills = 0;
    private bool isActive = false;

    public System.Action OnClear;
    public System.Action OnFail;

    private void OnEnable()
    {
        GameEvents.EnemyKilled += OnEnemyKilled;
    }

    private void OnDisable()
    {
        GameEvents.EnemyKilled -= OnEnemyKilled;
    }

    public void StartCount(int goal)
    {
        currentKills = 0;
        killGoal = goal;
        isActive = true;
        UpdateUI();
    }

    public void EndCountByFail()
    {
        if (!isActive) return;
        isActive = false;
        OnFail?.Invoke();
    }

    private void OnEnemyKilled()
    {
        if (!isActive) return;

        currentKills++;
        UpdateUI();

        if (currentKills >= killGoal)
        {
            isActive = false;
            OnClear?.Invoke();
        }
    }

    private void UpdateUI()
    {
        if (killSlider != null)
            killSlider.value = Mathf.Clamp01((float)currentKills / killGoal);
    }
}