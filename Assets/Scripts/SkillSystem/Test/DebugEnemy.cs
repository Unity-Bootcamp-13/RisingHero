using UnityEngine;

public class DebugEnemy : MonoBehaviour, IDamageable
{
    int currentHp = 100;

    public void TakeDamage(int damage)
    {
        if (currentHp <= 0)
            return;

        currentHp -= damage;
        Debug.Log($"{gameObject.name}�� {damage}�� ���ظ� �Ծ����ϴ�. " +
            $"{gameObject.name}�� ���� ü�� : {currentHp}");

        if (currentHp <= 0)
        {
            Debug.Log($"{gameObject.name} ���");
            gameObject.SetActive(false);
        }
    }
}
