using UnityEngine;

public class WeaponLevelUp : MonoBehaviour
{
    [Header("업그레이드 비용 설정")]
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
            Debug.LogError("[WeaponLevelUp] SaveService가 초기화되지 않았습니다.");
            return false;
        }

        var save = saveService.Load();
        var owned = save.ownedWeapons.Find(w => w.weaponId == weaponId);
        if (owned == null)
        {
            Debug.LogWarning("해당 무기를 보유하고 있지 않습니다.");
            return false;
        }

        var weaponData = WeaponStatus.Instance.FindWeaponDataById(weaponId);
        if (weaponData == null)
        {
            Debug.LogWarning("무기 데이터를 찾을 수 없습니다.");
            return false;
        }

        if (owned.level >= weaponData.maxLevel)
        {
            Debug.Log("이미 최대 레벨입니다.");
            return false;
        }

        int upgradeCost = CalculateUpgradeCost(owned.level);
        if (save.coin < upgradeCost)
        {
            Debug.Log("코인이 부족합니다.");
            return false;
        }

        save.coin -= upgradeCost;
        owned.level++;
        saveService.Save(save);

        Debug.Log($"[WeaponLevelUp] 무기 ID {weaponId} → Lv.{owned.level} (Cost: {upgradeCost})");
        return true;
    }

    public int CalculateUpgradeCost(int currentLevel)
    {
        return Mathf.RoundToInt(baseUpgradeCost * Mathf.Pow(costMultiplier, currentLevel - 1));
    }
}
