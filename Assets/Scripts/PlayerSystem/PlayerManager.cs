using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [Header("�÷��̾� ���� ������Ʈ")]
    [SerializeField] private PlayerAttack playerAttack;
    [SerializeField] private WeaponEquip weaponEquip;

    [Header("�÷��̾� ������Ʈ")]
    [SerializeField] private PlayerHealth characterHealth;
    [SerializeField] private PlayerMana playerMana;

    [Header("UI")]
    [SerializeField] private PlayerUI playerUI;

    private void Awake()
    {
        // �ʿ��� ������ ����
        playerAttack?.Initialize(weaponEquip);
        playerUI.Initialize(characterHealth, playerMana);
    }

    public PlayerHealth GetHealth() => characterHealth;
    public PlayerMana GetMana() => playerMana;
}
