using UnityEngine;

public class Boss : Enemy
{
    [Header("Boss 전용")]
    [SerializeField] private BossHealthUI bossHealthUI;

    protected override void Awake()
    {
        base.Awake();

        if (bossHealthUI != null)
        {
            health.OnHealthChanged += bossHealthUI.UpdateHealthBar;
        }
    }

    protected override void Die()
    {
        base.Die();

        // 보스 전용 사망 처리 (예: 게임 클리어 UI)
        Debug.Log("Boss defeated!");
        // GameClearManager.Instance.ShowClearUI(); // 필요 시 여기에 연결
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        bossHealthUI?.Show();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        bossHealthUI?.Hide();
    }
}
