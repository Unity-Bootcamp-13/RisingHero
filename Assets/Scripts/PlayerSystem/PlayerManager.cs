using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [Header("플레이어 관련 컴포넌트")]
    [SerializeField] private PlayerAttack playerAttack;
    [SerializeField] private WeaponEquip weaponEquip;

    [Header("플레이어 컴포넌트")]
    [SerializeField] private CharacterHealth characterHealth;
    [SerializeField] private PlayerMana playerMana;

    [Header("UI")]
    [SerializeField] private PlayerUI playerUI;

    private void Awake()
    {
        // 필요한 의존성 주입
        playerAttack?.Initialize(weaponEquip);
        playerUI.Initialize(characterHealth, playerMana);
    }

    public CharacterHealth GetHealth() => characterHealth;
    public PlayerMana GetMana() => playerMana;
}
