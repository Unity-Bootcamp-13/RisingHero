using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [Header("플레이어 관련 컴포넌트")]
    [SerializeField] private PlayerAttack playerAttack;
    [SerializeField] private WeaponEquip weaponEquip;

    private void Awake()
    {
        // 필요한 의존성 주입
        playerAttack?.Initialize(weaponEquip);
    }
}
