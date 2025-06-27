using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class GachaManager : MonoBehaviour
{
    [Header("���� ��� UI")]
    [SerializeField] private Transform rewardParentPanel;
    [SerializeField] private float spawnInterval = 0.1f;

    [Header("��í ��ư")]
    [SerializeField] private GameObject[] gachaButtons;

    [Header("��í �ݱ� ��ư")]
    [SerializeField] private GameObject closeButton;

    [Header("����ġ ����Ʈ")]
    [SerializeField] private List<int> rewardWeights;

    [Header("������ �ڽ� ������")]
    [SerializeField] private GameObject rewardPrefab;

    private int totalWeight = 0;
    private List<GameObject> rewardPool = new List<GameObject>();


    private void Start()
    {
        TotalWeight();
    }


    public void RollOnce() => StartCoroutine(SpawnRewardsOneByOne(GetRandomRewards(1)));
    public void RollTen() => StartCoroutine(SpawnRewardsOneByOne(GetRandomRewards(10)));

    private List<int> GetRandomRewards(int count)
    {
        List<int> results = new();

        for (int i = 0; i < count; i++)
        {
            results.Add(GachaRandomReward());
        }

        return results;
    }

    private int GachaRandomReward()
    {
        int rand = Random.Range(1, totalWeight + 1);
        int cumulative = 0;
        for (int i = 0; i < rewardWeights.Count; i++)
        {
            cumulative += rewardWeights[i];
            if (rand <= cumulative)
            {
                return i; // �ش� �ε��� ��ȯ
            }
        }
        throw new System.Exception("����ġ�� ���� �������� ����ġ�պ��� ũ��!");
    }

    private IEnumerator SpawnRewardsOneByOne(List<int> rewardIndexes)
    {
        SetGachaButtonsActive(false);

        // ���� ���� False
        foreach (GameObject pool in rewardPool)
            pool.SetActive(false);

        yield return new WaitForSeconds(spawnInterval);

        foreach (int id in rewardIndexes)
        {
            if (id > 0 && id < rewardWeights.Count)
            {
                //Instantiate(currentTable.rewardPrefabs[index], rewardParentPanel);
                var icon = Instantiate(rewardPrefab, rewardParentPanel); //������ ���⼭ �޾Ƽ� �ڿ� �����ܿ� ���� �ʿ䰡 ����.
                rewardPool.Add(icon);

                icon.GetComponent<RewardIcon>().Setup(id);
            }
            yield return new WaitForSeconds(spawnInterval);
        }

        SetGachaButtonsActive(true);
    }

    private void TotalWeight()
    {
        foreach (int weight in rewardWeights)
        {
            totalWeight += weight;
        }
    }

    private void SetGachaButtonsActive(bool active)
    {
        foreach (GameObject btn in gachaButtons)
        {
            btn.SetActive(active);
        }
        closeButton.SetActive(active);
    }

    /*private void ExceptionGacha()
    {
        if (currentTable == null)
        {
            Debug.LogError("Gacha Table is not set!");
            return;
        }
        if (currentTable.rewardDataList == null || currentTable.rewardDataList.Count == 1)
        {
            Debug.LogError("Reward data list is empty!");
            return;
        }
        if (currentTable.rewardWeights == null || currentTable.rewardWeights.Count != currentTable.rewardDataList.Count)
        {
            Debug.LogError("Reward weights are not set correctly!");
            return;
        }
        if (currentTable.rewardPrefabs == null)
        {
            Debug.LogError("Reward prefab is not set!");
            return;
        }
    }*/
}
