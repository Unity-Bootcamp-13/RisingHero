using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private int maxHp = 30;
    public int currentHp;

    private bool isInvincible = false;

    public event Action OnDie;
    public event Action<int> OnDamaged;
    public event Action<float> OnHealthChanged;
    public event Func<bool> OnBeforeDie;

    public int MaxHp => maxHp;

    private void Awake()
    {
        currentHp = maxHp;
    }

    public void TakeDamage(int amount)
    {
        if (isInvincible) return; // 무적 상태면 무시

        currentHp = Mathf.Max(currentHp - amount, 0);

        OnHealthChanged?.Invoke((float)currentHp / maxHp);
        OnDamaged?.Invoke(amount);

        if (currentHp <= 0)
        {
            if (OnBeforeDie?.Invoke() != true)
            {
                OnDie?.Invoke();
            }
        }
    }

    public void ResetHealth()
    {
        currentHp = maxHp;
    }

    public void SetInvincible(bool value)
    {
        isInvincible = value;
    }

    public void ForceUpdateUI()
    {
        OnHealthChanged?.Invoke((float)currentHp / maxHp);
    }
}
