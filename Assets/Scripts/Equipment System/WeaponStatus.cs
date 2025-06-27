using UnityEngine;

public class WeaponStatus : MonoBehaviour
{
    public static WeaponStatus Instance { get; private set; }

    [Header("무기 목록")]
    [SerializeField] private WeaponData[] allWeapons;

    [Header("스탯 적용 대상")]
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

    public void ApplyAllWeaponStats()
    {
        if (saveService == null || allWeapons == null || playerStatus == null)
        {
            Debug.LogError("[WeaponStatus] 초기화되지 않은 구성 요소가 있습니다.");
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
