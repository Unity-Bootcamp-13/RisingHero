using UnityEngine;

public class Boss : Enemy
{
    [Header("Boss Àü¿ë")]
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
        Debug.Log("Boss defeated!");
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
