using UnityEngine;

public class WeaponEquip : MonoBehaviour
{
    [Header("�⺻ ����")]
    [SerializeField] private WeaponData defaultWeapon; // ��Ÿ�� Ž������ ����.

    public WeaponData EquippedWeapon { get; private set; }

    private ISaveService saveService;
    private WeaponStatus weaponStatus;

    public void Initialize(ISaveService saveService, WeaponStatus weaponStatus)
    {
        this.saveService = saveService;
        this.weaponStatus = weaponStatus;
    }

    private void Start()
    {
        if (saveService == null || weaponStatus == null)
        {
            return;
        }

        var saveData = saveService.Load();

        if (!saveData.ownedWeapons.Exists(w => w.weaponId == defaultWeapon.weaponId)) // ���� ���Ⱑ ������� ù��° ���⸦ �ر����ִ� �ڵ�
        {
            saveData.ownedWeapons.Add(new OwnedWeapon(defaultWeapon.weaponId, 1));
            saveService.Save(saveData);
        }

        if (weaponStatus.FindWeaponDataById(saveData.equippedWeaponId) == null) // ���� ���⸦ �������� �ʾ��� ��� ù��° ���⸦ �����ϴ� �ڵ�
        {
            saveData.equippedWeaponId = defaultWeapon.weaponId;
            saveService.Save(saveData);
        }

        // �������� ���⸦ ã��, ������ ���⸦ ã��
        var equippedWeaponData = weaponStatus.FindWeaponDataById(saveData.equippedWeaponId);
        var ownedWeapon = saveData.ownedWeapons.Find(w => w.weaponId == saveData.equippedWeaponId);

        if (equippedWeaponData != null && ownedWeapon != null) // ���� ����
        {
            EquippedWeapon = equippedWeaponData;
        }

        weaponStatus.ApplyAllWeaponStats(); // ���� ȿ�� ����
    }

    public void Equip(WeaponData weapon, int level) // ���� ����, �����ϸ� �����ϰ�, ���� ȿ���� �����ϴ� �ڵ�
    {
        if (saveService == null || weaponStatus == null)
        {
            return;
        }

        var save = saveService.Load();
        save.equippedWeaponId = weapon.weaponId;
        saveService.Save(save);

        EquippedWeapon = weapon;

        weaponStatus.ApplyAllWeaponStats();
    }
}
