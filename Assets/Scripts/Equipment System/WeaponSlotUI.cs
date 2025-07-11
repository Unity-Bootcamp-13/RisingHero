using UnityEngine;

public class WeaponSlotUI : MonoBehaviour
{
    [Header("���� ������")]
    [SerializeField] private GameObject weaponSlotPrefab;
    [SerializeField] private Transform slotParent;

    [Header("�ʿ��� ������")]
    [SerializeField] private WeaponStatus weaponStatus;
    [SerializeField] private WeaponEquip weaponEquip;
    [SerializeField] private WeaponInfoUI weaponInfoUI;

    private ISaveService saveService;

    public void Initialize(ISaveService saveService)
    {
        this.saveService = saveService;

        foreach (Transform child in slotParent)
        {
            Destroy(child.gameObject);
        }

        var weapons = weaponStatus.GetAllWeapons();

        foreach (var weapon in weapons)
        {
            GameObject instance = Instantiate(weaponSlotPrefab, slotParent);
            var slot = instance.GetComponent<WeaponSlotItem>();

            bool isUnlocked = weaponStatus.IsUnlocked(weapon.weaponId);
            int level = weaponStatus.GetWeaponLevel(weapon.weaponId);

            slot.Initialize(weapon, isUnlocked, level, weaponEquip, weaponInfoUI);
        }
    }
}
