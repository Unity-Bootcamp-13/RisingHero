using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [Header("Weapon System ����")]
    [SerializeField] private WeaponStatus weaponStatus;
    [SerializeField] private PlayerStatus playerStatus;
    [SerializeField] private CoinUI coinUI;

    [Header("������ ������Ʈ")]
    [SerializeField] private WeaponEquip weaponEquip;
    [SerializeField] private WeaponLevelUp weaponLevelUp;
    [SerializeField] private WeaponInfoUI weaponInfoUI;
    [SerializeField] private WeaponSlotUI weaponSlotUI;

    private ISaveService saveService;

    private void Awake()
    {
        saveService = new JsonSaveService();

        weaponStatus?.Initialize(saveService, playerStatus);
        weaponEquip?.Initialize(saveService, weaponStatus);
        weaponLevelUp?.Initialize(saveService, weaponStatus);
        weaponInfoUI?.Initialize(saveService, weaponEquip, weaponLevelUp, coinUI, weaponStatus);

        weaponSlotUI?.Initialize(saveService);
    }
}
