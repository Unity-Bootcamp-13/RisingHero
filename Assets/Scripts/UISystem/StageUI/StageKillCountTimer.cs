using TMPro;
using UnityEngine;

public class StageKillCountTimer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private TextMeshProUGUI killText;

    public void UpdateTime(float time)
    {
        int intTime = Mathf.CeilToInt(time);
        timeText.text = $"남은 시간: {intTime}초";
    }

    public void UpdateKillCount(int current, int goal)
    {
        killText.text = $"처치 수: {current} / {goal}";
    }

    public void Show(bool active)
    {
        timeText.gameObject.SetActive(active);
        killText.gameObject.SetActive(active);
    }
}
