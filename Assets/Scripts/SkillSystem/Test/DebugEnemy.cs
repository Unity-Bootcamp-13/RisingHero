using UnityEngine;

public class DebugEnemy : MonoBehaviour, IDamageable
{
    int currentHp = 100;

    public void TakeDamage(int damage)
    {
        if (currentHp <= 0)
            return;

        currentHp -= damage;
        Debug.Log($"{gameObject.name}이 {damage}의 피해를 입었습니다. " +
            $"{gameObject.name}의 현재 체력 : {currentHp}");

        if (currentHp <= 0)
        {
            Debug.Log($"{gameObject.name} 사망");
            gameObject.SetActive(false);
        }
    }
}
