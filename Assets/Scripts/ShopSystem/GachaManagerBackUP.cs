using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GachaGroupManager
{
    private List<ListItemData> allItems;

public GachaGroupManager(List<ListItemData> items)
{
    allItems = items;
}

public List<ListItemData> GetItemsByGroup(int groupId)
{
    return allItems.FindAll(item => item.GroupId == groupId);
}
}

public class GachaManagerBackUp : MonoBehaviour
{
    [Header("보상 출력 UI")]
    [SerializeField] private Transform rewardParentPanel;
    [SerializeField] private float spawnInterval = 0.1f;

    [Header("가챠 버튼")]
    [SerializeField] private GameObject[] gachaButtons;

    [Header("가챠 닫기 버튼")]
    [SerializeField] private GameObject closeButton;

    [Header("가중치 리스트")]
    [SerializeField] private List<int> rewardWeights;

    [Header("아이콘 박스 프리팹")]
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
                return i; // 해당 인덱스 반환
            }
        }
        throw new System.Exception("가중치가 보상 아이템의 가중치합보다 크다!");
    }

    private IEnumerator SpawnRewardsOneByOne(List<int> rewardIndexes)
    {
        SetGachaButtonsActive(false);

        // 기존 보상 False
        foreach (GameObject pool in rewardPool)
            pool.SetActive(false);

        yield return new WaitForSeconds(spawnInterval);

        foreach (int id in rewardIndexes)
        {
            if (id > 0 && id < rewardWeights.Count)
            {
                //Instantiate(currentTable.rewardPrefabs[index], rewardParentPanel);
                var icon = Instantiate(rewardPrefab, rewardParentPanel); //어차피 여기서 받아서 뒤에 아이콘에 넣을 필요가 없다.
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
}