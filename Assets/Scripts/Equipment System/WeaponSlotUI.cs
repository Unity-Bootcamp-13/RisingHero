using UnityEngine;
using UnityEngine.UI;

public class WeaponSlotUI : MonoBehaviour
{
    [Header("UI ����")]
    [SerializeField] private Image background;
    [SerializeField] private Image icon;
    [SerializeField] private GameObject lockIcon;

    [Header("������ Asset")]
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
            Debug.LogWarning($"{gameObject.name} ���Կ� weaponData�� �������� �ʾҽ��ϴ�.");
            return;
        }

        if (saveService == null)
        {
            Debug.LogError("[WeaponSlotUI] SaveService�� �ʱ�ȭ���� �ʾҽ��ϴ�.");
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
