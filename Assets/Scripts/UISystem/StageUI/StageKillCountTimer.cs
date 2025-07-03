using TMPro;
using UnityEngine;

public class StageKillCountTimer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private TextMeshProUGUI killText;

    public void UpdateTime(float time)
    {
        int intTime = Mathf.CeilToInt(time);
        timeText.text = $"���� �ð�: {intTime}��";
    }

    public void UpdateKillCount(int current, int goal)
    {
        killText.text = $"óġ ��: {current} / {goal}";
    }

    public void Show(bool active)
    {
        timeText.gameObject.SetActive(active);
        killText.gameObject.SetActive(active);
    }
}
