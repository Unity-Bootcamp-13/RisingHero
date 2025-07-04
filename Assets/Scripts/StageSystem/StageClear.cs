using UnityEngine;

public class StageClear : MonoBehaviour
{
    [SerializeField] private KillCounter killCounter;
    [SerializeField] private Timer timer;

    private ISaveService saveService;

    public void Initialize(ISaveService saveService)
    {
        this.saveService = saveService;

        int stage = saveService.Load().currentStage;
        if (IsEliteStage(stage))
        {
            killCounter?.StartCount(80); // ��ǥ ų ��
            killCounter.OnClear += OnStageClear;
            killCounter.OnFail += OnStageFail;

            timer?.StartTimer(120f); // ���� �ð�
            timer.OnTimeout += () => killCounter?.EndCountByFail();
        }
    }

    private bool IsEliteStage(int stage)
    {
        return stage == 19 || stage == 29; // stage % 10 == 9
    }

    private void OnStageClear()
    {
        timer?.StopTimer();
        Debug.Log("[StageClear] �������� Ŭ����!");
    }

    private void OnStageFail()
    {
        timer?.StopTimer();
        Debug.Log("[StageClear] �������� ����!");
    }
}
