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
        weaponLevelUp?.Initialize(saveService, weaponStatus, weaponEquip);
        weaponInfoUI?.Initialize(saveService, weaponEquip, weaponLevelUp, coinUI);

        foreach (var slot in weaponSlotUIs)
        {
            slot?.Initialize(saveService, weaponEquip, weaponInfoUI);
        }
    }

    private void OnApplicationQuit()
    {
        var save = saveService.Load();
        saveService.Save(save);
    }
}
