using UnityEngine;
using UnityEngine.UI;

public class WeaponSlotUI : MonoBehaviour
{
    [Header("UI 연결")]
    [SerializeField] private Image background;
    [SerializeField] private Image icon;
    [SerializeField] private GameObject lockIcon;

    [Header("데이터 Asset")]
    [SerializeField] private WeaponData weaponData;

    private bool isUnlocked;

    private ISaveService saveService;
    private WeaponEquip weaponEquip;
    private WeaponInfoUI weaponInfoUI;

    public void Initialize(ISaveService saveService, WeaponEquip weaponEquip, WeaponInfoUI weaponInfoUI)
    {
        this.saveService = saveService;
        this.weaponEquip = weaponEquip;
        this.weaponInfoUI = weaponInfoUI;
    }

    private void Start()
    {
        if (weaponData == null)
        {
            Debug.LogWarning($"{gameObject.name} 슬롯에 weaponData가 설정되지 않았습니다.");
            return;
        }

        if (saveService == null)
        {
            Debug.LogError("[WeaponSlotUI] SaveService가 초기화되지 않았습니다.");
            return;
        }

        var saveData = saveService.Load();
        isUnlocked = saveData.ownedWeapons.Exists(w => w.weaponId == weaponData.weaponId);

        SetData(weaponData, !isUnlocked);
    }

    public void SetData(WeaponData data, bool locked)
    {
        weaponData = data;

        if (weaponData != null)
        {
            icon.sprite = weaponData.icon;
            background.color = WeaponData.GetColorByRarity(weaponData.rarity);
            icon.enabled = true;
        }
        else
        {
            icon.enabled = false;
            background.color = Color.black;
        }

        lockIcon.SetActive(locked);
    }

    public void OnClickEquip()
    {
        if (!isUnlocked || weaponData == null || saveService == null || weaponEquip == null)
            return;

        var saveData = saveService.Load();
        var owned = saveData.ownedWeapons.Find(w => w.weaponId == weaponData.weaponId);
        if (owned == null) return;

        weaponEquip.Equip(weaponData, owned.level);
    }

    public void OnClickSelect()
    {
        if (weaponData == null || weaponInfoUI == null) return;

        weaponInfoUI.Display(weaponData);
    }
}
