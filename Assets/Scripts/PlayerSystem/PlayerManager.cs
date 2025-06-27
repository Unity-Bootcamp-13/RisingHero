using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [Header("�÷��̾� ���� ������Ʈ")]
    [SerializeField] private PlayerAttack playerAttack;
    [SerializeField] private WeaponEquip weaponEquip;

    private void Awake()
    {
        // �ʿ��� ������ ����
        playerAttack?.Initialize(weaponEquip);
    }
}
