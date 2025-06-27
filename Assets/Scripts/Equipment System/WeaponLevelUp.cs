using UnityEngine;

public class WeaponLevelUp : MonoBehaviour
{
    [Header("���׷��̵� ��� ����")]
    [SerializeField] private int baseUpgradeCost = 10;
    [SerializeField] private float costMultiplier = 1.5f;

    private ISaveService saveService;
    private WeaponStatus weaponStatus;
    private WeaponEquip weaponEquip;

    public void Initialize(ISaveService saveService, WeaponStatus weaponStatus, WeaponEquip weaponEquip)
    {
        this.saveService = saveService;
        this.weaponStatus = weaponStatus;
        this.weaponEquip = weaponEquip;
    }

    public bool TryUpgradeWeapon(int weaponId)
    {
        if (saveService == null || weaponStatus == null)
        {
            Debug.LogError("[WeaponLevelUp] SaveService �Ǵ� WeaponStatus�� �ʱ�ȭ���� �ʾҽ��ϴ�.");
            return false;
        }

        var save = saveService.Load();
        var owned = save.ownedWeapons.Find(w => w.weaponId == weaponId);
        if (owned == null)
        {
            Debug.LogWarning("�ش� ���⸦ �����ϰ� ���� �ʽ��ϴ�.");
            return false;
        }

        var weaponData = weaponStatus.FindWeaponDataById(weaponId);
        if (weaponData == null)
        {
            Debug.LogWarning("���� �����͸� ã�� �� �����ϴ�.");
            return false;
        }

        if (owned.level >= weaponData.maxLevel)
        {
            Debug.Log("�̹� �ִ� ������ �����߽��ϴ�.");
            return false;
        }

        int upgradeCost = CalculateUpgradeCost(owned.level);
        if (save.coin < upgradeCost)
        {
            Debug.Log("������ �����մϴ�.");
            return false;
        }

        save.coin -= upgradeCost;
        owned.level++;
        saveService.Save(save);
        Debug.Log("[WeaponLevelUp] ���� �Ϸ� - ����: " + save.coin + ", ���� ����: " + owned.level);

        weaponStatus.ApplyAllWeaponStats();

        if (save.equippedWeaponId == weaponId)
        {
            var equippedData = weaponStatus.FindWeaponDataById(weaponId);
            weaponEquip?.Equip(equippedData, owned.level);
        }

        CoinUI coinUI = Object.FindFirstObjectByType<CoinUI>();
        coinUI?.UpdateCoinUI();

        Debug.Log($"[WeaponUpgrade] ���� {weaponData.weaponName} ��ȭ �Ϸ� �� Lv.{owned.level} (�ڽ�Ʈ: {upgradeCost})");
        return true;
    }


    public int CalculateUpgradeCost(int currentLevel)
    {
        return Mathf.RoundToInt(baseUpgradeCost * Mathf.Pow(costMultiplier, currentLevel - 1));
    }
}
