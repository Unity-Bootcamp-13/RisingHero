using UnityEngine;
using System;
using System.Collections.Generic;

public class BossStage : MonoBehaviour
{
    [Header("설정")]
    [SerializeField] private float timeLimit = 180f;

    [Header("컴포넌트")]
    [SerializeField] private Timer timer;
    [SerializeField] private List<EnemyHealth> bossList = new(); // 다수의 보스 리스트

    private EnemyHealth activeBoss;
    private bool isBossDefeated = false;

    public event Action OnClear;
    public event Action OnFail;

    private void Start()
    {
        if (timer == null)
        {
            return;
        }

        if (bossList == null || bossList.Count == 0)
        {
            return;
        }

        // 세이브 데이터 로드
        ISaveService saveService = new JsonSaveService(); // 실제 게임에서는 DI 방식으로 전달하는 것이 좋습니다
        int currentStage = saveService.Load().currentStage;

        // 보스 선택
        int bossIndex = GetBossIndexFromStage(currentStage);
        if (bossIndex < 0 || bossIndex >= bossList.Count)
        {
            return;
        }

        // 보스 설정
        for (int i = 0; i < bossList.Count; i++)
        {
            if (i == bossIndex)
            {
                bossList[i].gameObject.SetActive(true);
                activeBoss = bossList[i];
                activeBoss.OnDie += HandleBossDeath;
            }
            else
            {
                bossList[i].gameObject.SetActive(false);
            }
        }

        timer.OnTimeout += HandleTimeout;
        timer.StartTimer(timeLimit);
    }

    private void OnDestroy()
    {
        if (timer != null)
            timer.OnTimeout -= HandleTimeout;

        if (activeBoss != null)
            activeBoss.OnDie -= HandleBossDeath;
    }

    private int GetBossIndexFromStage(int stage)
    {
        if (stage >= 10 && stage <= 19)
            return 0;
        else if (stage >= 20 && stage <= 29)
            return 1;

        return -1; // 비정상 입력
    }

    private void HandleBossDeath()
    {
        BossDefeated();
    }

    public void BossDefeated()
    {
        if (isBossDefeated) return;
        isBossDefeated = true;

        timer.StopTimer();
        OnClear?.Invoke();
    }

    private void HandleTimeout()
    {
        if (!isBossDefeated)
        {
            timer.StopTimer();
            OnFail?.Invoke();
        }
    }
}
