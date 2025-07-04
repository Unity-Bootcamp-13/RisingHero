using System;
using System.Collections;
using UnityEngine;

public class CharacterHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private int maxHp = 100;
    private int currentHp;

    [SerializeField] private float invincibleTime = 1f;
    private bool isInvincible = false;

    public event Action OnDie;
    public event Action<int> OnDamaged;

    private void Awake()
    {
        currentHp = maxHp;
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
