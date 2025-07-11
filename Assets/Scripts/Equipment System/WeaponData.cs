using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NewWeapon", menuName = "Weapon/Create New Weapon")]
public class WeaponData : ScriptableObject
{
    [Header("기본 정보")]
    public int weaponId;
    public string weaponName;
    public WeaponRarity rarity;
    public Sprite icon;
    public int maxLevel = 10;

    [Header("UI 정보")]
    public string displayName;

    [Header("효과 스택")]
    public List<WeaponStatEntry> ownedStats = new();
    public List<WeaponStatEntry> equippedStats = new();

    [Header("투사체 설정")]
    public GameObject projectilePrefab;

    /// <summary>
    /// 코드가 중복되긴 하지만 무기는 보유, 장착의 상태만 있어서 유지
    /// </summary>
    private float GetStatValue(List<WeaponStatEntry> stats, WeaponStatType type, int level) //범용 스탯 계산 메서드
    {
        var entry = stats.Find(s => s.statType == type);
        if (entry == null) return 0f;
        return entry.baseValue + entry.growthPerLevel * (level - 1);
    }

    public float GetOwnedStatValue(WeaponStatType type, int level) // 보유 효과 스탯 계산
    {
        return GetStatValue(ownedStats, type, level);
    }

    public float GetEquippedStatValue(WeaponStatType type, int level) // 장착 효과 스탯 계산
    {
        return GetStatValue(equippedStats, type, level);
    }
}

[System.Serializable]
public class WeaponStatEntry
{
    public WeaponStatType statType;
    public float baseValue;
    public float growthPerLevel;
}

public enum WeaponStatType
{
    ArrowDamage,
    AttackCooldown,
    CritChance,
    CritDamage,
    MaxHp,
    MoveSpeed
}

public enum WeaponRarity
{
    Common,
    Rare,
    Epic,
    Legendary
}
