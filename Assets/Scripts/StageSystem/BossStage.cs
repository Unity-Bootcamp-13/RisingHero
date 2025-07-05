using UnityEngine;
using System;

public class BossStage : MonoBehaviour
{
    [Header("설정")]
    [SerializeField] private float timeLimit = 180f;

    [Header("컴포넌트")]
    [SerializeField] private Timer timer;

    private bool isBossDefeated = false;

    public event Action OnClear;
    public event Action OnFail;

    private void Start()
    {
        if (timer == null)
        {
            Debug.LogError("[BossStage] Timer가 연결되지 않았습니다.");
            return;
        }

        timer.OnTimeout += HandleTimeout;
        timer.StartTimer(timeLimit);
    }

    private void OnDestroy()
    {
        timer.OnTimeout -= HandleTimeout;
    }

    public void BossDefeated()
    {
        if (isBossDefeated) return;
        isBossDefeated = true;

        timer.StopTimer();
        OnClear.Invoke();
    }

    private void HandleTimeout()
    {
        if (!isBossDefeated)
        {
            timer.StopTimer();
            OnFail.Invoke();
        }
    }
}