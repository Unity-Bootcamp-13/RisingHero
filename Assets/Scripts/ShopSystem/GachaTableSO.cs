using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewGachaTable", menuName = "Gacha/Gacha Table")]
public class GachaTableSO : ScriptableObject
{
    public string gachaName;

    [Tooltip("���⳪ ��ų ScriptableObject�� ����")]
    public List<ScriptableObject> rewardDataList;

    [Tooltip("rewardDataList�� �ε����� 1:1 �����ϴ� ����ġ")]
    public List<int> rewardWeights;

    [Tooltip("�� ���� �����ϴ� ��¿� ������ (UI)")]
    public GameObject rewardPrefabs;
}
