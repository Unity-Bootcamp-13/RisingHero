using UnityEngine;
using UnityEngine.UI;

public class KillCounter : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Image fillImage;

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

    public void AddKill()
    {
        if (!isActive) return;

        currentKills++;
        UpdateUI();

        if (currentKills >= killGoal)
        {
            isActive = false;
            Debug.Log("[KillCounter] 목표 달성");
            OnClear?.Invoke();
        }
    }

    public void EndCountByFail()
    {
        if (!isActive) return;

        isActive = false;
        Debug.Log("[KillCounter] 실패");
        OnFail?.Invoke();
    }

    private void UpdateUI()
    {
        if (fillImage != null)
        {
            fillImage.fillAmount = Mathf.Clamp01((float)currentKills / killGoal);
        }
    }
}
