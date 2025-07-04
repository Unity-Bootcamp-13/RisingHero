using UnityEngine;

public class WeaponEquip : MonoBehaviour
{
    [Header("기본 무기")]
    [SerializeField] private WeaponData defaultWeapon; // 참조해서 찾을 수도 있는데, 이렇게 하면 성능과 의존성 모두 잡을 수 있습니다.

    public WeaponData EquippedWeapon { get; private set; } 

    private ISaveService saveService;
    private WeaponStatus weaponStatus;

    public void Initialize(ISaveService saveService, WeaponStatus weaponStatus) // 이게 의존성을 낮추기 위한 의존성 주입 방식임. 즉 DI
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

        if (!saveData.ownedWeapons.Exists(w => w.weaponId == defaultWeapon.weaponId)) // 만약 무기가 없을경우 첫번째 무기를 해금해주는 코드
        {
            saveData.ownedWeapons.Add(new OwnedWeapon(defaultWeapon.weaponId, 1));
            saveService.Save(saveData);
        }

        if (weaponStatus.FindWeaponDataById(saveData.equippedWeaponId) == null) // 만약 무기를 장착하지 않았을 경우 첫번째 무기를 장착하는 코드
        {
            saveData.equippedWeaponId = defaultWeapon.weaponId;
            saveService.Save(saveData);
        }
        
        // 장착중인 무기를 찾고, 보유한 무기를 찾음
        var equippedWeaponData = weaponStatus.FindWeaponDataById(saveData.equippedWeaponId);
        var ownedWeapon = saveData.ownedWeapons.Find(w => w.weaponId == saveData.equippedWeaponId);

        if (equippedWeaponData != null && ownedWeapon != null) // 무기 장착
        {
            EquippedWeapon = equippedWeaponData;
        }

        weaponStatus.ApplyAllWeaponStats(); // 보유 효과 적용
    }

    public void Equip(WeaponData weapon, int level) // 장착 로직, 장착하면 저장하고, 장착 효과를 적용하는 코드
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
