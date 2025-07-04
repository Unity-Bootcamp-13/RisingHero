using UnityEngine;

public class WeaponStatus : MonoBehaviour
{
    public static WeaponStatus Instance { get; private set; }

    [Header("���� ���")]
    [SerializeField] private WeaponData[] allWeapons; // ��� ���� ���, �� �־�� �ϳĸ� ����ȿ���� �����ؾ��ؼ� �־�߸� �ϴ� �ڵ�

    [Header("���� ���� ���")]
    [SerializeField] private PlayerStatus playerStatus;

    private ISaveService saveService;

    public void Initialize(ISaveService saveService, PlayerStatus playerStatus)
    {
        this.saveService = saveService;
        this.playerStatus = playerStatus;
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void ApplyAllWeaponStats() // �̰� ���� ������ ȿ���� �����ϴ� �ڵ�
    {
        if (saveService == null || allWeapons == null || playerStatus == null)
        {
            Debug.LogError("[WeaponStatus] �ʱ�ȭ���� ���� ���� ��Ұ� �ֽ��ϴ�.");
            return;
        }

        var save = saveService.Load();
        playerStatus.ResetToBaseStats();

        foreach (var weapon in allWeapons)
        {
            var owned = save.ownedWeapons.Find(w => w.weaponId == weapon.weaponId);
            if (owned != null)
            {
                playerStatus.ApplyWeaponStats(weapon.ownedStats, owned.level);

                if (save.equippedWeaponId == weapon.weaponId)
                {
                    playerStatus.ApplyWeaponStats(weapon.equippedStats, owned.level);
                }
            }
        }
    }
    public WeaponData FindWeaponDataById(int id)
    {
        if (allWeapons == null) return null;
        return System.Array.Find(allWeapons, w => w.weaponId == id);
    }
}
