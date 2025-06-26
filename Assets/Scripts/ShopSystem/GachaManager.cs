using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GachaManager : MonoBehaviour
{
    [Header("가챠 테이블 (무기, 스킬 등)")]
    [SerializeField] private GachaTableSO currentTable;

    [Header("보상 출력 UI")]
    [SerializeField] private Transform rewardParentPanel;
    [SerializeField] private float spawnInterval = 0.1f;

    [Header("가챠 버튼")]
    [SerializeField] private GameObject[] gachaButtons;

    [Header("가챠 닫기 버튼")]
    [SerializeField] private GameObject closeButton;

    public void SetTable(GachaTableSO table)
    {
        currentTable = table;
    }

    public void RollOnce() => StartCoroutine(SpawnRewardsOneByOne(GetRandomRewards(1)));
    public void RollTen() => StartCoroutine(SpawnRewardsOneByOne(GetRandomRewards(10)));

    private List<int> GetRandomRewards(int count)
    {
        List<int> results = new();
        int totalWeight = 0;

        foreach (int weight in currentTable.rewardWeights)
            totalWeight += weight;

        for (int i = 0; i < count; i++)
        {
            int rand = Random.Range(1, totalWeight + 1);
            int cumulative = 0;

            for (int j = 0; j < currentTable.rewardWeights.Count; j++)
            {
                cumulative += currentTable.rewardWeights[j];
                if (rand <= cumulative)
                {
                    results.Add(j);
                    break;
                }
            }
        }

        return results;
    }

    private IEnumerator SpawnRewardsOneByOne(List<int> rewardIndexes)
    {
        // 버튼 비활성화
        foreach (GameObject btn in gachaButtons)
            btn.SetActive(false);

        // 기존 보상 제거
        foreach (Transform child in rewardParentPanel)
            Destroy(child.gameObject);

        yield return new WaitForSeconds(spawnInterval);

        foreach (int index in rewardIndexes)
        {
            if (index > 0 && index < currentTable.rewardDataList.Count)
            {
                //Instantiate(currentTable.rewardPrefabs[index], rewardParentPanel);
                var icon = Instantiate(currentTable.rewardPrefabs, rewardParentPanel);
                icon.GetComponent<RewardIcon>()?.Setup(currentTable.rewardDataList[index]);
            }
            yield return new WaitForSeconds(spawnInterval);
        }

        // 버튼 재활성화
        foreach (GameObject btn in gachaButtons)
            btn.SetActive(true);
    }
}
