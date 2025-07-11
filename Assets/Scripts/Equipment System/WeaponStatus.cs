using UnityEngine;

public class WeaponStatus : MonoBehaviour
{
    [SerializeField] private WeaponData[] allWeapons;
    [SerializeField] private PlayerStatus playerStatus;

    private ISaveService saveService;

    public void Initialize(ISaveService saveService, PlayerStatus playerStatus)
    {
        this.saveService = saveService;
        this.playerStatus = playerStatus;
    }

    public bool IsUnlocked(int weaponId)
    {
        var save = saveService?.ReloadFromFile();
        return save?.ownedWeapons.Exists(w => w.weaponId == weaponId) ?? false;
    }

    public int GetWeaponLevel(int weaponId)
    {
        var save = saveService?.Load();
        return save?.ownedWeapons.Find(w => w.weaponId == weaponId)?.level ?? 1;
    }

    public WeaponData FindWeaponDataById(int id)
    {
        if (allWeapons == null) return null;
        return System.Array.Find(allWeapons, w => w.weaponId == id);
    }

    public void ApplyAllWeaponStats() // ����, ������ ��� ����
    {
        if (saveService == null || allWeapons == null || playerStatus == null)
        {
            Debug.LogWarning("WeaponStatus: �ʼ� �������� �����Ǿ����ϴ�.");
            return;
        }

        var save = saveService.ReloadFromFile();

        playerStatus.ResetToBaseStats();

        foreach (var weapon in allWeapons)
        {
            var owned = save.ownedWeapons.Find(w => w.weaponId == weapon.weaponId);
            if (owned != null)
            {
                Debug.Log($"���� ���� �����: {weapon.weaponId} / ����: {owned.level}");

                playerStatus.ApplyWeaponStats(weapon.ownedStats, owned.level);

                if (save.equippedWeaponId == weapon.weaponId)
                {
                    Debug.Log($"���� ���� ȿ�� �����: {weapon.weaponId}");
                    playerStatus.ApplyWeaponStats(weapon.equippedStats, owned.level);
                }
            }
        }
    }
    public WeaponData[] GetAllWeapons()
    {
        return allWeapons;
    }
}