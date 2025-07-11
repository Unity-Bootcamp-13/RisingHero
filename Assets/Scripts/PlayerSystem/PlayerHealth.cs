using System;
using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private int maxHp = 100;
    private int currentHp;

    [SerializeField] private float invincibleTime = 1f;
    private bool isInvincible = false;

    [Header("자동 회복 설정")]
    [SerializeField] private bool enableRegen = true;
    [SerializeField] private int regenAmount = 1;
    [SerializeField] private float regenInterval = 1.0f;

    public event Action OnDie;
    public event Action<int> OnDamaged;
    public event Action<int> OnHealed; 

    private Coroutine regenCoroutine;

    private void Awake()
    {
        currentHp = maxHp;

        if (enableRegen)
        {
            regenCoroutine = StartCoroutine(AutoRegenCoroutine());
        }
    }

    public void TakeDamage(int amount)
    {
        if (isInvincible) return;

        currentHp -= amount;
        OnDamaged?.Invoke(amount);

        if (currentHp <= 0)
        {
            currentHp = 0;
            OnDie?.Invoke();

            if (regenCoroutine != null)
                StopCoroutine(regenCoroutine);
        }
        else
        {
            StartCoroutine(InvincibilityCoroutine());
        }
    }

    private IEnumerator InvincibilityCoroutine()
    {
        isInvincible = true;
        yield return new WaitForSeconds(invincibleTime);
        isInvincible = false;
    }

    private IEnumerator AutoRegenCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(regenInterval);

            if (currentHp > 0 && currentHp < maxHp)
            {
                int before = currentHp;
                currentHp += regenAmount;
                if (currentHp > maxHp) currentHp = maxHp;

                int healedAmount = currentHp - before;
                if (healedAmount > 0)
                {
                    OnHealed?.Invoke(healedAmount);
                }
            }
        }
    }

    public void SetInvincible(bool value)
    {
        isInvincible = value;
    }

    public int CurrentHp => currentHp;

    public int GetMaxHp()
    {
        return maxHp;
    }
}
