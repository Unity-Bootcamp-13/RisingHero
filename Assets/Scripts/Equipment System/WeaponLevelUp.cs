using UnityEngine;
using static UnityEditor.PlayerSettings;

public class WeaponLevelUp : MonoBehaviour
{
    [Header("���׷��̵� ��� ����")]
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

        var weaponData = weaponStatus.FindWeaponDataById(weaponId);
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

        int cost = CalculateUpgradeCost(owned.level);
        if (save.coin < cost)
        {
            Debug.Log("������ �����մϴ�.");
            return false;
        }

        // ���� ó��
        save.coin -= cost;
        owned.level++;

        if (save.currentQuestId == 2)
        {
            SetQuest(questManager.CurrentQuest);
            quest.AddProgress(1);
        }

        saveService.Save(save);
        Debug.Log($"[WeaponLevelUp] ���� ID {weaponId} �� Lv.{owned.level} (Cost: {cost})");

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
