using UnityEngine;
using UnityEngine.UI;

public class WeaponSlotItem : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private GameObject lockIcon;

    private WeaponData weaponData;
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
            return;
        }

        icon.sprite = weaponData.icon;
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
