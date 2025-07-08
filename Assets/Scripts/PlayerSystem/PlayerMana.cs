using UnityEngine;
using System.Collections;

public class PlayerMana : MonoBehaviour
{
    [SerializeField] private int maxMana = 100;
    [SerializeField] private float recoverInterval = 1f; // ȸ�� �ֱ� (��)
    [SerializeField] private int recoverAmount = 2;      // ȸ����

    private int currentMana;

    private void Awake()
    {
        currentMana = maxMana;
    }

    private void Start()
    {
        StartCoroutine(AutoRecoverRoutine());
    }

    public bool HasEnoughMana(int cost)
    {
        return currentMana >= cost;
    }

    public void ConsumeMana(int cost)
    {
        currentMana = Mathf.Max(currentMana - cost, 0);
        Debug.Log($"[PlayerMana] ���� {cost} �Ҹ� �� ���� ����: {currentMana}");
    }

    public int GetCurrentMana() => currentMana;

    public void RecoverMana(int amount)
    {
        int before = currentMana;
        currentMana = Mathf.Min(currentMana + amount, maxMana);

        if (currentMana > before)
        {
            Debug.Log($"[PlayerMana] ���� {amount} ȸ�� �� ���� ����: {currentMana}");
        }
    }

    private IEnumerator AutoRecoverRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(recoverInterval);
            RecoverMana(recoverAmount);
        }
    }
}
