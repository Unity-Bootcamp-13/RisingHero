using UnityEngine;

public class WeaponEquip : MonoBehaviour
{
    [Header("�⺻ ����")]
    [SerializeField] private WeaponData defaultWeapon;

    public WeaponData EquippedWeapon { get; private set; } // �����ؼ� ã�� ���� �ִµ�, �̷��� �ϸ� ���ɰ� ������ ��� ���� �� �ֽ��ϴ�.

    private ISaveService saveService;
    private WeaponStatus weaponStatus;

    public void Initialize(ISaveService saveService, WeaponStatus weaponStatus)
    {
        this.saveService = saveService;
        this.weaponStatus = weaponStatus;
    }

    private void Start()
    {
        if (saveService == null || weaponStatus == null) // �� �α� �߸� Manager�� �����ϼ���
        {
            Debug.LogError("[WeaponEquip] �������� �ʱ�ȭ���� �ʾҽ��ϴ�.");
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

        weaponStatus.ApplyAllWeaponStats(); // ���� ȿ�� ����
    }

    public void Equip(WeaponData weapon, int level)
    {
        if (saveService == null || weaponStatus == null)
        {
            Debug.LogError("[WeaponEquip] �������� �ʱ�ȭ���� �ʾҽ��ϴ�.");
            return;
        }

        var save = saveService.Load();
        save.equippedWeaponId = weapon.weaponId;
        saveService.Save(save);

        EquippedWeapon = weapon;

        weaponStatus.ApplyAllWeaponStats();
        Debug.Log($"[WeaponEquip] ���� ������: {weapon.weaponName} (���� {level})");
    }
}
