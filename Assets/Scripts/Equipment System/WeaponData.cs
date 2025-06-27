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

    public static Color GetColorByRarity(WeaponRarity rarity) // 따로 빼도 됩니다.
    {
        return rarity switch
        {
            WeaponRarity.Common => Color.gray,
            WeaponRarity.Rare => Color.green,
            WeaponRarity.Epic => new Color(0.6f, 0.2f, 0.8f),
            WeaponRarity.Legendary => new Color(0.9f, 0.7f, 0.1f),
            _ => Color.white
        };
    }

    public float GetStatValue(WeaponStatType type, int level)
    {
        var entry = ownedStats.Find(s => s.statType == type);
        if (entry == null) return 0f;
        return entry.baseValue + entry.growthPerLevel * (level - 1);
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
