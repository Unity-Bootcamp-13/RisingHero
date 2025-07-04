using UnityEngine;

[System.Serializable]
public class RewardRange
{
    public int Min;
    public int Max;

    public RewardRange(int min, int max)
    {
        Min = min;
        Max = max;
    }

    public int GetRandom()
    {
        return Random.Range(Min, Max + 1);
    }
}
