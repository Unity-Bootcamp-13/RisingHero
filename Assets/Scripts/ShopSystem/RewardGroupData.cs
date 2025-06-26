using UnityEngine;
using static UnityEditor.Progress;


public class RewardGroupContainer : MonoBehaviour
{
    [SerializeField]
    private RewardWeaponGroupData[] weaponGroups = new RewardWeaponGroupData[11];

    [SerializeField]
    private RewardSkillGroupData[] skillGroups = new RewardSkillGroupData[11];

    private void Reset()
    {
        for (int i = 0; i <= 10; i++)
        {
            weaponGroups[i] = new RewardWeaponGroupData { ItemId = i, Weight = 100 };
            skillGroups[i] = new RewardSkillGroupData { ItemId = i, Weight = 50 };
        }
    }
}

[System.Serializable]
public class RewardWeaponGroupData
{
    private int itemId;
    private int weight;

    public int ItemId
    {
        get => itemId;
        set => itemId = value;
    }

    public int Weight
    {
        get => weight;
        set => weight = value;
    }
}

public class RewardSkillGroupData
{
    private int itemId;
    private int weight;

    public int ItemId
    {
        get => itemId;
        set => itemId = value;
    }

    public int Weight
    {
        get => weight;
        set => weight = value;
    }
}
