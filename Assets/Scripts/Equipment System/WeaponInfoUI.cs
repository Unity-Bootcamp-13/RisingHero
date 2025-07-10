using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

[System.Serializable]
public class WeaponStatDisplay
{
    public TMP_Text label;
    public TMP_Text before;
    public TMP_Text after;
}

public class WeaponInfoUI : MonoBehaviour
{
    public WeaponData SelectedWeapon { get; private set; }

    [Header("UI 연결")]
    [SerializeField] private TMP_Text weaponName;
    [SerializeField] private TMP_Text weaponLevel;
    [SerializeField] private Image weaponIcon;

    [Header("보유 효과 UI")]
    [SerializeField] private WeaponStatDisplay[] ownedEffectDisplays;

    [Header("장착 효과 UI")]
    [SerializeField] private WeaponStatDisplay[] equippedEffectDisplays;

    private ISaveService saveService;
    private WeaponEquip weaponEquip;
    private WeaponLevelUp weaponLevelUp;
    private CoinUI coinUI;
    private WeaponStatus weaponStatus;

    public void Initialize(ISaveService saveService, WeaponEquip weaponEquip, WeaponLevelUp weaponLevelUp, CoinUI coinUI, WeaponStatus weaponStatus)
    {
        this.saveService = saveService;
        this.weaponEquip = weaponEquip;
        this.weaponLevelUp = weaponLevelUp;
        this.coinUI = coinUI;
        this.weaponStatus = weaponStatus;
    }

    public void Display(WeaponData data)
    {
        if (saveService == null)
        {
            return;
        }

        SelectedWeapon = data;
        weaponName.text = data.weaponName;

        weaponIcon.sprite = data.icon;
        weaponIcon.enabled = true;

        var save = saveService.Load();
        var owned = save.ownedWeapons.Find(w => w.weaponId == data.weaponId);
        int currentLevel = owned?.level ?? 1;
        int nextLevel = currentLevel + 1;

        weaponLevel.text = $"Lv.{currentLevel} / {data.maxLevel}";

        UpdateStatDisplays(data.ownedStats, ownedEffectDisplays, currentLevel, nextLevel, true);
        UpdateStatDisplays(data.equippedStats, equippedEffectDisplays, currentLevel, nextLevel, false);
    }

    private void UpdateStatDisplays(List<WeaponStatEntry> stats, WeaponStatDisplay[] displays, int curLevel, int nextLevel, bool isOwned)
    {
        for (int i = 0; i < displays.Length; i++)
        {
            if (i < stats.Count)
            {
                var stat = stats[i];
                displays[i].label.text = stat.statType.ToString();

                float valueBefore = isOwned
                    ? SelectedWeapon.GetOwnedStatValue(stat.statType, curLevel)
                    : SelectedWeapon.GetEquippedStatValue(stat.statType, curLevel);

                float valueAfter = isOwned
                    ? SelectedWeapon.GetOwnedStatValue(stat.statType, nextLevel)
                    : SelectedWeapon.GetEquippedStatValue(stat.statType, nextLevel);

                displays[i].before.text = valueBefore.ToString("0.##");
                displays[i].after.text = valueAfter.ToString("0.##");

                SetAlpha(displays[i].label, 1);
                SetAlpha(displays[i].before, 1);
                SetAlpha(displays[i].after, 1);
            }
            else
            {
                HideText(displays[i].label, displays[i].before, displays[i].after);
            }
        }
    }

    private void SetAlpha(TMP_Text text, float alpha)
    {
        var c = text.color;
        c.a = alpha;
        text.color = c;
    }

    private void HideText(TMP_Text label, TMP_Text before, TMP_Text after)
    {
        label.text = before.text = after.text = "";
        SetAlpha(label, 0);
        SetAlpha(before, 0);
        SetAlpha(after, 0);
    }

    public void OnClickLevelUp()
    {
        if (SelectedWeapon == null || weaponLevelUp == null) return;

        if (weaponLevelUp.TryUpgradeWeapon(SelectedWeapon.weaponId))
        {
            weaponStatus.ApplyAllWeaponStats();

            var save = saveService.Load();
            if (save.equippedWeaponId == SelectedWeapon.weaponId)
            {
                var equippedData = weaponStatus.FindWeaponDataById(SelectedWeapon.weaponId);
                var owned = save.ownedWeapons.Find(w => w.weaponId == SelectedWeapon.weaponId);
                weaponEquip.Equip(equippedData, owned.level);
            }

            Display(SelectedWeapon);
            coinUI?.UpdateCoinUI();
        }
    }

    public void OnClickEquip()
    {
        if (SelectedWeapon == null || saveService == null || weaponEquip == null) return;

        var save = saveService.Load();
        var owned = save.ownedWeapons.Find(w => w.weaponId == SelectedWeapon.weaponId);
        if (owned == null) return;

        weaponEquip.Equip(SelectedWeapon, owned.level);
    }
}
