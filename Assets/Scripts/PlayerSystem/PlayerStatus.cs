using UnityEngine;
using System.Collections.Generic;

public class PlayerStatus : MonoBehaviour
{
    [Header("기본 능력치")]
    public float moveSpeed = 3f;
    public float attackRange = 3f;
    public float attackCooldown = 0.5f;
    public int arrowDamage = 1;
    public float critChance = 0.1f;
    public float critDamage = 1.5f;

    public void ResetToBaseStats()
    {
        moveSpeed = 3f;
        attackCooldown = 0.5f;
        arrowDamage = 1;
        critChance = 0.1f;
        critDamage = 1.5f;
    }

    public void ApplyWeaponStats(List<WeaponStatEntry> stats, int level)
    {
        foreach (var stat in stats)
        {
            float value = stat.baseValue + stat.growthPerLevel * (level - 1);
            ApplyStat(stat.statType, value);
        }
    }

    private void ApplyStat(WeaponStatType type, float value)
    {
        switch (type)
        {
            case WeaponStatType.ArrowDamage:
                arrowDamage += Mathf.RoundToInt(value);
                break;
            case WeaponStatType.AttackCooldown:
                attackCooldown += value;
                break;
            case WeaponStatType.CritChance:
                critChance += value;
                break;
            case WeaponStatType.CritDamage:
                critDamage += value;
                break;
            case WeaponStatType.MoveSpeed:
                moveSpeed += value;
                break;
        }
    }
}
