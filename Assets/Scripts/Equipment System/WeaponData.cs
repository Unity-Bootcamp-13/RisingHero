using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NewWeapon", menuName = "Weapon/Create New Weapon")]
public class WeaponData : ScriptableObject
{
    [Header("�⺻ ����")]
    public int weaponId;
    public string weaponName;
    public WeaponRarity rarity;
    public Sprite icon;
    public int maxLevel = 10;

    [Header("UI ����")]
    public string displayName;

    [Header("ȿ�� ����")]
    public List<WeaponStatEntry> ownedStats = new();
    public List<WeaponStatEntry> equippedStats = new();

    [Header("����ü ����")]
    public GameObject projectilePrefab;

    /// <summary>
    /// �ڵ尡 �ߺ��Ǳ� ������ ����� ����, ������ ���¸� �־ ����
    /// </summary>
    private float GetStatValue(List<WeaponStatEntry> stats, WeaponStatType type, int level) //���� ���� ��� �޼���
    {
        var entry = stats.Find(s => s.statType == type);
        if (entry == null) return 0f;
        return entry.baseValue + entry.growthPerLevel * (level - 1);
    }

    public float GetOwnedStatValue(WeaponStatType type, int level) // ���� ȿ�� ���� ���
    {
        return GetStatValue(ownedStats, type, level);
    }

    public float GetEquippedStatValue(WeaponStatType type, int level) // ���� ȿ�� ���� ���
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
