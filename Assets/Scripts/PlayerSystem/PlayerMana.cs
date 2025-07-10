using UnityEngine;
using System.Collections;

public class PlayerMana : MonoBehaviour
{
    [SerializeField] private int maxMana = 100;
    [SerializeField] private float recoverInterval = 1f; // 회복 주기 (초)
    [SerializeField] private int recoverAmount = 2;      // 회복량

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
    }

    public int GetCurrentMana() => currentMana;

    public void RecoverMana(int amount)
    {
        int before = currentMana;
        currentMana = Mathf.Min(currentMana + amount, maxMana);
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
