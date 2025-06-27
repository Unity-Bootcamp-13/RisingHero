using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class WeaponInfoUI : MonoBehaviour
{
    public WeaponData SelectedWeapon { get; private set; }

    [Header("UI 연결")]
    [SerializeField] private TMP_Text weaponName;
    [SerializeField] private TMP_Text weaponLevel;
    [SerializeField] private Image weaponIcon;

    [SerializeField] private TMP_Text ownedEffect1Label;
    [SerializeField] private TMP_Text ownedEffect1Before;
    [SerializeField] private TMP_Text ownedEffect1After;

    [SerializeField] private TMP_Text ownedEffect2Label;
    [SerializeField] private TMP_Text ownedEffect2Before;
    [SerializeField] private TMP_Text ownedEffect2After;

    [SerializeField] private TMP_Text equippedEffect1Label;
    [SerializeField] private TMP_Text equippedEffect1Before;
    [SerializeField] private TMP_Text equippedEffect1After;

    [SerializeField] private TMP_Text equippedEffect2Label;
    [SerializeField] private TMP_Text equippedEffect2Before;
    [SerializeField] private TMP_Text equippedEffect2After;

    private ISaveService saveService;
    private WeaponEquip weaponEquip;
    private WeaponLevelUp weaponLevelUp;
    private CoinUI coinUI;

    public void Initialize(ISaveService saveService, WeaponEquip weaponEquip, WeaponLevelUp weaponLevelUp, CoinUI coinUI)
    {
        this.saveService = saveService;
        this.weaponEquip = weaponEquip;
        this.weaponLevelUp = weaponLevelUp;
        this.coinUI = coinUI;
    }

    public void Display(WeaponData data)
    {
        if (saveService == null)
        {
            Debug.LogError("[WeaponInfoUI] SaveService가 초기화되지 않았습니다.");
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

        UpdateEffectChange(data.ownedStats, currentLevel, nextLevel, ownedEffect1Label, ownedEffect1Before, ownedEffect1After, 0);
        UpdateEffectChange(data.ownedStats, currentLevel, nextLevel, ownedEffect2Label, ownedEffect2Before, ownedEffect2After, 1);

        UpdateEffectChange(data.equippedStats, currentLevel, nextLevel, equippedEffect1Label, equippedEffect1Before, equippedEffect1After, 0);
        UpdateEffectChange(data.equippedStats, currentLevel, nextLevel, equippedEffect2Label, equippedEffect2Before, equippedEffect2After, 1);
    }

    private void UpdateEffectChange(List<WeaponStatEntry> stats, int curLevel, int nextLevel,
        TMP_Text label, TMP_Text before, TMP_Text after, int index)
    {
        if (index < stats.Count)
        {
            var stat = stats[index];
            float valueBefore = stat.baseValue + stat.growthPerLevel * (curLevel - 1);
            float valueAfter = stat.baseValue + stat.growthPerLevel * (nextLevel - 1);

            label.text = stat.statType.ToString();
            before.text = valueBefore.ToString("0.##");
            after.text = valueAfter.ToString("0.##");

            SetAlpha(label, 1); SetAlpha(before, 1); SetAlpha(after, 1);
        }
        else
        {
            HideText(label, before, after);
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
        SetAlpha(label, 0); SetAlpha(before, 0); SetAlpha(after, 0);
    }

    public void OnClickLevelUp()
    {
        if (SelectedWeapon == null || weaponLevelUp == null) return;

        if (weaponLevelUp.TryUpgradeWeapon(SelectedWeapon.weaponId))
        {
            WeaponStatus.Instance.ApplyAllWeaponStats();

            var save = saveService.Load();
            if (save.equippedWeaponId == SelectedWeapon.weaponId)
            {
                var equippedData = WeaponStatus.Instance.FindWeaponDataById(SelectedWeapon.weaponId);
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
