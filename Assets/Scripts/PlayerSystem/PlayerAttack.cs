using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerAttack : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float attackDelayAfterStop = 0.2f;

    private Animator animator;
    private PlayerMovement movement;
    private PlayerDetection detection;
    private PlayerStatus status;
    private WeaponEquip weaponEquip;

    private float lastMoveTime;
    private float lastAttackTime;
    private Transform currentTarget;
    private bool isAttacking;

    public bool IsAttacking => isAttacking;

    private static class AnimatorParams
    {
        public const string Attack = "Attack";
        public const string Direction = "Direction";
    }

    public void Initialize(WeaponEquip weaponEquip)
    {
        this.weaponEquip = weaponEquip;
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        movement = GetComponent<PlayerMovement>();
        status = GetComponent<PlayerStatus>();
        detection = new PlayerDetection(status);
    }

    private void Update()
    {
        if (movement.IsMoving) // �����϶� ���� �Ұ�
        {
            lastMoveTime = Time.time;
            if (isAttacking)
                ForceCancel();
            return;
        }

        if (isAttacking) return;
        if (Time.time - lastMoveTime < attackDelayAfterStop) return;
        if (Time.time - lastAttackTime < status.attackCooldown) return;

        TryAttack();
    }

    private void TryAttack()
    {
        currentTarget = detection.FindNearestEnemy(transform.position, AliveEnemyManager.Enemies);
        if (currentTarget == null || !detection.IsInRange(transform.position, currentTarget)) return; // ���� �ȿ� ���� ��쿡�� �����ؾ���.

        Vector2 dir = (currentTarget.position - transform.position).normalized;
        StartAttack(dir);
    }

    private void StartAttack(Vector2 direction)
    {
        var equippedWeapon = weaponEquip?.EquippedWeapon; // ��� �߻�ü�� ���� SO�� ����� Prefab�� �߻���
        if (equippedWeapon == null || equippedWeapon.projectilePrefab == null)
        {
            return;
        }

        isAttacking = true;
        lastAttackTime = Time.time;

        int dirCode = detection.GetDirectionCode(direction);

        animator.SetLayerWeight(0, 0f); // Animation ����ġ
        animator.SetLayerWeight(1, 1f);
        animator.SetInteger(AnimatorParams.Attack, dirCode);
        animator.SetInteger(AnimatorParams.Direction, dirCode);

        Vector3 offset = (Vector3)(-direction * 0.3f); // ȭ�� ��ġ ����
        GameObject arrow = Instantiate(equippedWeapon.projectilePrefab, transform.position + offset, Quaternion.identity);

        if (arrow.TryGetComponent(out Arrow arrowScript)) // SO�κ��� �߻�ü Prefab ��������
        {
            arrowScript.Shoot(direction, status.arrowDamage, status.critChance, status.critDamage);
        }

        StartCoroutine(ResetAttack(0.4f));
    }

    private IEnumerator ResetAttack(float delay) // ������ �� �� �� �ڿ� �������� ���ϴ� �ڵ�
    {
        yield return new WaitForSeconds(delay);

        animator.SetInteger(AnimatorParams.Attack, 0);
        animator.SetLayerWeight(1, 0f);
        animator.SetLayerWeight(0, 1f);
        isAttacking = false;
    }

    public void ForceCancel() // ������ �� �������� �ʱ� ���� ���� �ڵ�
    {
        StopAllCoroutines();
        animator.SetInteger(AnimatorParams.Attack, 0);
        animator.SetLayerWeight(1, 0f);
        animator.SetLayerWeight(0, 1f);
        isAttacking = false;
    }
}
