using UnityEngine;

public class WeaponLevelUp : MonoBehaviour
{
    [Header("���׷��̵� ��� ����")]
    [SerializeField] private int baseUpgradeCost = 10;
    [SerializeField] private float costMultiplier = 1.5f;

    private ISaveService saveService;

    public void Initialize(ISaveService saveService)
    {
        this.saveService = saveService;
    }

    public bool TryUpgradeWeapon(int weaponId)
    {
        if (saveService == null)
        {
            Debug.LogError("[WeaponLevelUp] SaveService�� �ʱ�ȭ���� �ʾҽ��ϴ�.");
            return false;
        }

        var save = saveService.Load();
        var owned = save.ownedWeapons.Find(w => w.weaponId == weaponId);
        if (owned == null)
        {
            Debug.LogWarning("�ش� ���⸦ �����ϰ� ���� �ʽ��ϴ�.");
            return false;
        }

        var weaponData = WeaponStatus.Instance.FindWeaponDataById(weaponId);
        if (weaponData == null)
        {
            Debug.LogWarning("���� �����͸� ã�� �� �����ϴ�.");
            return false;
        }

        if (owned.level >= weaponData.maxLevel)
        {
            Debug.Log("�̹� �ִ� �����Դϴ�.");
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

        Debug.Log($"[WeaponLevelUp] ���� ID {weaponId} �� Lv.{owned.level} (Cost: {upgradeCost})");
        return true;
    }

    public int CalculateUpgradeCost(int currentLevel)
    {
        return Mathf.RoundToInt(baseUpgradeCost * Mathf.Pow(costMultiplier, currentLevel - 1));
    }
}
