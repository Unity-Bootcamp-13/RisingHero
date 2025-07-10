using UnityEngine;


public class WeaponLevelUp : MonoBehaviour
{
    [Header("업그레이드 비용 설정")]
    [SerializeField] private int baseUpgradeCost = 10;
    [SerializeField] private float costMultiplier = 1.5f;

    [SerializeField] private QuestManager questManager;
    [SerializeField] private Quest quest;

    private ISaveService saveService;
    private WeaponStatus weaponStatus;

    public void Initialize(ISaveService saveService, WeaponStatus weaponStatus)
    {
        this.saveService = saveService;
        this.weaponStatus = weaponStatus;
    }

    public bool TryUpgradeWeapon(int weaponId)
    {
        if (saveService == null)
        {
            return false;
        }

        var save = saveService.Load();
        var owned = save.ownedWeapons.Find(w => w.weaponId == weaponId);
        if (owned == null)
        {
            return false;
        }

        var weaponData = weaponStatus.FindWeaponDataById(weaponId);
        if (weaponData == null)
        {
            return false;
        }

        if (owned.level >= weaponData.maxLevel)
        {
            return false;
        }

        int cost = CalculateUpgradeCost(owned.level);
        if (save.coin < cost)
        {
            return false;
        }

        // 실제 처리
        save.coin -= cost;
        owned.level++;

        if (save.currentQuestId == 2)
        {
            SetQuest(questManager.CurrentQuest);
            quest.AddProgress(1);
        }

        saveService.Save(save);

        return true;
    }


    public int CalculateUpgradeCost(int currentLevel)
    {
        return Mathf.RoundToInt(baseUpgradeCost * Mathf.Pow(costMultiplier, currentLevel - 1));
    }

    public void SetQuest(Quest quest)
    {
        this.quest = quest;
    }
}
