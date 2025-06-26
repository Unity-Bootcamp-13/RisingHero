using UnityEngine;
using System;

public class Targetable : MonoBehaviour
{
    public string TargetName = "Target";
    public float MaxHealth = 100f;          // �ӽ� �ִ� ü�� 100 ����
    public float CurrentHealth {  get; private set; }

    public event Action<float, Targetable> OnHealthChanged; // ü�� ��ȭ
    public event Action<Targetable> OnDied;                 // ���

    private void Awake()
    {
        CurrentHealth = MaxHealth;
    }

    public void TakeDamage(float damage)
    {
        CurrentHealth -= damage;
        CurrentHealth = Mathf.Max(0, CurrentHealth);        // 0 �̸����� �������� �ʰ� ����

        Debug.Log($"{TargetName}�� {damage}�� ���ظ� �Ծ����ϴ�. ���� ü�� : {CurrentHealth}");
        OnHealthChanged?.Invoke(CurrentHealth, this);

        if (CurrentHealth <= 0)
        {
            OnDied?.Invoke(this);
            // ���� Ǯ�� �Ǵ� ��Ȱ��ȭ ���� �ۼ�
        }
    }
}
