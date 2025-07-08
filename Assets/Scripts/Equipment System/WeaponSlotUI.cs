
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 불가피하게 책임이 높아졌는데 호출만하기 때문에 괜찮다고 판단.
/// </summary>
public class WeaponSlotUI : MonoBehaviour
{
    [Header("UI 연결")]
    [SerializeField] private Image background;
    [SerializeField] private Image icon;
    [SerializeField] private GameObject lockIcon;

    [Header("데이터 Asset")]
    [SerializeField] private WeaponData weaponData;
    public WeaponData WeaponData => weaponData; // 읽기 전용 프로퍼티

    private bool isUnlocked;
    private WeaponEquip weaponEquip;
    private WeaponInfoUI weaponInfoUI;
    private int weaponLevel;

    public void Initialize(
        WeaponData data,
        bool isUnlocked,
        int weaponLevel,
        WeaponEquip equip,
        WeaponInfoUI infoUI)
    {
        this.weaponData = data;
        this.isUnlocked = isUnlocked;
        this.weaponLevel = weaponLevel;
        this.weaponEquip = equip;
        this.weaponInfoUI = infoUI;

        UpdateUI();
    }

    private void UpdateUI()
    {
        if (weaponData == null)
        {
            icon.enabled = false;
            background.color = Color.black;
            return;
        }

        icon.sprite = weaponData.icon;
        background.color = WeaponData.GetColorByRarity(weaponData.rarity);
        icon.enabled = true;

        lockIcon.SetActive(!isUnlocked);
    }

    public void OnClickEquip()
    {
        if (!isUnlocked || weaponData == null || weaponEquip == null)
            return;

        weaponEquip.Equip(weaponData, weaponLevel);
    }

    public void OnClickSelect()
    {
        if (weaponData == null || weaponInfoUI == null)
            return;

        weaponInfoUI.Display(weaponData);
    }
}
