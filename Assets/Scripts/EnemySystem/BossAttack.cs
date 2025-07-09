using UnityEngine;
using System.Collections;

/// <summary>
/// Handles boss projectile attack toward player's current position.
/// Includes warning display, projectile movement, impact effect, and damage.
/// </summary>
public class BossAttack : MonoBehaviour
{
    [Header("Attack 설정")]
    [SerializeField] private Transform playerTransform;
    [SerializeField] private GameObject bossAttackPrefab;
    [SerializeField] private ParticleSystem bossImpactParticle;
    [SerializeField] private float attackDelay = 3f;

    [Header("공격 속도")]
    public float bossAttackSpeed = 5f;

    [Header("예고 범위")]
    [SerializeField] private GameObject bossWarningPrefab;
    [SerializeField] private float warningDuration = 1f;

    [Header("공격 범위 및 대미지")]
    [SerializeField] private float bossAttackRadius = 1.5f;
    public int damage = 30;

    [SerializeField] private LayerMask playerLayer;

    private Coroutine attackRoutine;

    private void Start()
    {
        attackRoutine = StartCoroutine(AutoAttack());
    }

    public void ExecuteAttack()
    {
        if (playerTransform == null || bossAttackPrefab == null || bossImpactParticle == null)
        {
            Debug.LogWarning("BossAttack component is missing references.");
            return;
        }

        Vector3 targetPosition = playerTransform.position;

        if (bossWarningPrefab != null)
        {
            GameObject warning = Instantiate(bossWarningPrefab, targetPosition, Quaternion.identity);
            Destroy(warning, warningDuration);
        }

        StartCoroutine(DelayedAttack(targetPosition, warningDuration));
    }

    private IEnumerator DelayedAttack(Vector3 targetPosition, float delay)
    {
        yield return new WaitForSeconds(delay);

        GameObject projectile = Instantiate(bossAttackPrefab, transform.position, Quaternion.identity);
        StartCoroutine(MoveToTarget(projectile, targetPosition));
    }

    private IEnumerator MoveToTarget(GameObject projectile, Vector3 target)
    {
        while (projectile != null && Vector3.Distance(projectile.transform.position, target) > 0.1f)
        {
            projectile.transform.position = Vector3.MoveTowards(
                projectile.transform.position,
                target,
                bossAttackSpeed * Time.deltaTime
            );
            yield return null;
        }

        if (projectile != null)
            Destroy(projectile);

        TryDamageAt(target);

        ParticleSystem impact = Instantiate(bossImpactParticle, target, Quaternion.identity);
        impact.Play();
        Destroy(impact.gameObject, impact.main.duration);
    }

    private void TryDamageAt(Vector3 center)
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(center, bossAttackRadius, playerLayer);

        foreach (var hit in hits)
        {
            if (!hit.CompareTag("Player")) continue;

            if (hit.TryGetComponent(out IDamageable target))
            {
                target.TakeDamage(damage);
            }
        }
    }

    private IEnumerator AutoAttack()
    {
        while (true)
        {
            ExecuteAttack();
            yield return new WaitForSeconds(attackDelay);
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        if (playerTransform != null)
        {
            Gizmos.DrawWireSphere(playerTransform.position, bossAttackRadius);
        }
    }
#endif
}
