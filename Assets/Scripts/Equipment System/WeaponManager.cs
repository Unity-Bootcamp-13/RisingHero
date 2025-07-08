using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [Header("Weapon System 의존")]
    [SerializeField] private WeaponStatus weaponStatus;
    [SerializeField] private PlayerStatus playerStatus;
    [SerializeField] private CoinUI coinUI;

    [Header("연결할 컴포넌트")]
    [SerializeField] private WeaponEquip weaponEquip;
    [SerializeField] private WeaponLevelUp weaponLevelUp;
    [SerializeField] private WeaponInfoUI weaponInfoUI;
    [SerializeField] private WeaponSlotUI[] weaponSlotUIs;

    private ISaveService saveService;

    private void Awake()
    {
        saveService = new JsonSaveService();

        weaponStatus?.Initialize(saveService, playerStatus);
        weaponEquip?.Initialize(saveService, weaponStatus);
        weaponLevelUp?.Initialize(saveService, weaponStatus);
        weaponInfoUI?.Initialize(saveService, weaponEquip, weaponLevelUp, coinUI, weaponStatus);

        foreach (var slot in weaponSlotUIs)
        {
            if (slot == null || slot.WeaponData == null) continue;

            var data = slot.WeaponData;
            bool isUnlocked = weaponStatus.IsUnlocked(data.weaponId);
            int level = weaponStatus.GetWeaponLevel(data.weaponId);

            slot.Initialize(data, isUnlocked, level, weaponEquip, weaponInfoUI);
        }
    }
}
