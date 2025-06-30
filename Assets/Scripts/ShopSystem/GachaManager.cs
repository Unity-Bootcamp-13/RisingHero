using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GachaManager : MonoBehaviour
{
    [SerializeField] private Transform rewardParentPanel;
    [SerializeField] private GameObject rewardPrefab;
    [SerializeField] private GameObject[] gachaButtons;
    [SerializeField] private GameObject closeButton;

    private GachaDataService dataService;

    private void Awake()
    {
        dataService = new GachaDataService();
    }

    private void OnEnable()
    {
        GachaEventBus.OnGachaRoll += RollGacha;
    }

    private void OnDisable()
    {
        GachaEventBus.OnGachaRoll -= RollGacha;
    }

    public void RollGacha(int listId)
    {
        ListTableData table = dataService.GetListTable(listId);
        if (table == null)
        {
            Debug.LogError($"[GachaManager] ListId {listId} 에 해당하는 테이블이 없습니다.");
            return;
        }

        List<ListItemData> itemList = dataService.GetItemsForGroup(table.GroupId);

        // 가중치 리스트 생성
        List<int> weights = new();
        List<int> itemIds = new();
        foreach (var item in itemList)
        {
            itemIds.Add(item.ItemId);
            weights.Add(item.Weight);
        }

        List<int> resultIds = GetRandomRewards(itemIds, weights, table.RewardAmount);
        StartCoroutine(SpawnRewards(resultIds));
    }

    private List<int> GetRandomRewards(List<int> itemIds, List<int> weights, int count)
    {
        List<int> result = new();
        int total = 0;
        foreach (var w in weights) total += w;

        for (int i = 0; i < count; i++)
        {
            int r = UnityEngine.Random.Range(1, total + 1);
            int sum = 0;
            for (int j = 0; j < weights.Count; j++)
            {
                sum += weights[j];
                if (r <= sum)
                {
                    result.Add(itemIds[j]);
                    break;
                }
            }
        }

        return result;
    }

    private IEnumerator SpawnRewards(List<int> itemIds)
    {
        SetButtons(false);

        foreach (Transform child in rewardParentPanel)
            Destroy(child.gameObject);

        yield return new WaitForSeconds(0.1f);

        foreach (int id in itemIds)
        {
            var obj = Instantiate(rewardPrefab, rewardParentPanel);
            obj.GetComponent<RewardIcon>().Setup(id);
            yield return new WaitForSeconds(0.1f);
        }

        SetButtons(true);
    }

    private void SetButtons(bool active)
    {
        foreach (var btn in gachaButtons)
            btn.SetActive(active);
        closeButton.SetActive(active);
    }
}

public static class GachaEventBus
{
    public static event System.Action<int> OnGachaRoll;

    public static void RaiseGachaRoll(int listId)
    {
        OnGachaRoll.Invoke(listId);
    }
}
