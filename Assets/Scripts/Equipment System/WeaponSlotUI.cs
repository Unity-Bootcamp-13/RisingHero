
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �Ұ����ϰ� å���� �������µ� ȣ�⸸�ϱ� ������ �����ٰ� �Ǵ�.
/// </summary>
public class WeaponSlotUI : MonoBehaviour
{
    [Header("UI ����")]
    [SerializeField] private Image background;
    [SerializeField] private Image icon;
    [SerializeField] private GameObject lockIcon;

    [Header("������ Asset")]
    [SerializeField] private WeaponData weaponData;
    public WeaponData WeaponData => weaponData; // �б� ���� ������Ƽ

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
