using UnityEngine;

public class WeaponEquip : MonoBehaviour
{
    [Header("기본 무기")]
    [SerializeField] private WeaponData defaultWeapon;

    public WeaponData EquippedWeapon { get; private set; } // 참조해서 찾을 수도 있는데, 이렇게 하면 성능과 의존성 모두 잡을 수 있습니다.

    private ISaveService saveService;
    private WeaponStatus weaponStatus;

    public void Initialize(ISaveService saveService, WeaponStatus weaponStatus)
    {
        this.saveService = saveService;
        this.weaponStatus = weaponStatus;
    }

    private void Start()
    {
        if (saveService == null || weaponStatus == null) // 이 로그 뜨면 Manager에 연결하세요
        {
            Debug.LogError("[WeaponEquip] 의존성이 초기화되지 않았습니다.");
            return;
        }

        var saveData = saveService.Load();

        if (!saveData.ownedWeapons.Exists(w => w.weaponId == defaultWeapon.weaponId))
        {
            saveData.ownedWeapons.Add(new OwnedWeapon(defaultWeapon.weaponId, 1));
            saveService.Save(saveData);
        }

        if (weaponStatus.FindWeaponDataById(saveData.equippedWeaponId) == null)
        {
            saveData.equippedWeaponId = defaultWeapon.weaponId;
            saveService.Save(saveData);
        }

        var equippedWeaponData = weaponStatus.FindWeaponDataById(saveData.equippedWeaponId);
        var ownedWeapon = saveData.ownedWeapons.Find(w => w.weaponId == saveData.equippedWeaponId);

        if (equippedWeaponData != null && ownedWeapon != null)
        {
            EquippedWeapon = equippedWeaponData;
        }

        weaponStatus.ApplyAllWeaponStats(); // 보유 효과 적용
    }

    public void Equip(WeaponData weapon, int level)
    {
        if (saveService == null || weaponStatus == null)
        {
            Debug.LogError("[WeaponEquip] 의존성이 초기화되지 않았습니다.");
            return;
        }

        var save = saveService.Load();
        save.equippedWeaponId = weapon.weaponId;
        saveService.Save(save);

        EquippedWeapon = weapon;

        weaponStatus.ApplyAllWeaponStats();
        Debug.Log($"[WeaponEquip] 무기 장착됨: {weapon.weaponName} (레벨 {level})");
    }
}
