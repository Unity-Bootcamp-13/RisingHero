using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewGachaTable", menuName = "Gacha/Gacha Table")]
public class GachaTableSO : ScriptableObject
{
    public string gachaName;

    [Tooltip("무기나 스킬 ScriptableObject를 넣음")]
    public List<ScriptableObject> rewardDataList;

    [Tooltip("rewardDataList의 인덱스와 1:1 대응하는 가중치")]
    public List<int> rewardWeights;

    [Tooltip("각 보상에 대응하는 출력용 프리팹 (UI)")]
    public GameObject rewardPrefabs;
}
