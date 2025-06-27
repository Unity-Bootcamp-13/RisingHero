using UnityEngine;
using System;

public class Targetable123 : MonoBehaviour
{
    public string TargetName = "Target";
    public float MaxHealth = 100f;          // 임시 최대 체력 100 설정
    public float CurrentHealth {  get; private set; }

    public event Action<float, Targetable123> OnHealthChanged; // 체력 변화
    public event Action<Targetable123> OnDied;                 // 사망

    private void Awake()
    {
        CurrentHealth = MaxHealth;
    }

    public void TakeDamage(float damage)
    {
        CurrentHealth -= damage;
        CurrentHealth = Mathf.Max(0, CurrentHealth);        // 0 미만으로 내려가지 않게 설정

        Debug.Log($"{TargetName}이 {damage}의 피해를 입었습니다. 현재 체력 : {CurrentHealth}");
        OnHealthChanged?.Invoke(CurrentHealth, this);

        if (CurrentHealth <= 0)
        {
            OnDied?.Invoke(this);
            // 이후 풀링 또는 비활성화 로직 작성
        }
    }
}
