using UnityEngine;
using System;

public class EliteStage : MonoBehaviour
{
    [Header("설정")]
    [SerializeField] private int killGoal = 80;
    [SerializeField] private float timeLimit = 120f;

    [Header("컴포넌트")]
    [SerializeField] private KillCounter killCounter;
    [SerializeField] private Timer timer;

    public event Action OnClear;
    public event Action OnFail;

    private void Start()
    {
        StartStage();
    }

    public void StartStage()
    {
        if (killCounter == null || timer == null)
        {
            Debug.LogError("[EliteStage] KillCounter 또는 Timer가 연결되지 않았습니다.");
            return;
        }

        killCounter.OnClear += HandleClear;
        killCounter.OnFail += HandleFail;
        timer.OnTimeout += HandleTimeout;

        killCounter.StartCount(killGoal);
        timer.StartTimer(timeLimit);
    }

    private void OnDestroy()
    {
        killCounter.OnClear -= HandleClear;
        killCounter.OnFail -= HandleFail;
        timer.OnTimeout -= HandleTimeout;
    }

    public void StopStage()
    {
        killCounter.OnClear -= HandleClear;
        killCounter.OnFail -= HandleFail;
        timer.OnTimeout -= HandleTimeout;

        timer.StopTimer();
    }

    private void HandleClear()
    {
        StopStage();
        OnClear.Invoke();
    }

    private void HandleFail()
    {
        StopStage();
        OnFail.Invoke();
    }

    private void HandleTimeout()
    {
        killCounter.EndCountByFail();
    }
}
