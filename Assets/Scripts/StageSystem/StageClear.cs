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
            killCounter?.StartCount(80); // 목표 킬 수
            killCounter.OnClear += OnStageClear;
            killCounter.OnFail += OnStageFail;

            timer?.StartTimer(120f); // 제한 시간
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
        Debug.Log("[StageClear] 스테이지 클리어!");
    }

    private void OnStageFail()
    {
        timer?.StopTimer();
        Debug.Log("[StageClear] 스테이지 실패!");
    }
}
