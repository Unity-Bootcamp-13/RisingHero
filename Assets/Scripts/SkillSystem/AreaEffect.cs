using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AreaEffect : MonoBehaviour
{
    public SkillCaster Caster {  get; private set; }
    public float Damage { get; private set; }
    public float Radius { get; private set; }
    public float Duration { get; private set; }
    public float DamageTickInterval { get; private set; }
    public GameObject HitEffectPrefab { get; private set; }

    private SphereCollider areaCollider;
    private HashSet<Targetable> targetsInArea = new HashSet<Targetable>();
    private float tickTimer;

    public void Initialize(SkillCaster caster, float damage, float radius, float duration, float damageTickInterval, GameObject hitEffect)
    {
        Caster = caster;
        Damage = damage;
        Radius = radius;
        Duration = duration;
        DamageTickInterval = damageTickInterval;
        HitEffectPrefab = hitEffect;

        areaCollider = GetComponent<SphereCollider>();

        if (areaCollider == null)
            areaCollider = gameObject.AddComponent<SphereCollider>();

        areaCollider.isTrigger = true;
        areaCollider.radius = Radius;

        // 범위형 스킬의 시각적 범위 파티클로 표현
        transform.localScale = Vector3.one * Radius * 2;    // 스케일 조정

        tickTimer = 0f;

        StartCoroutine(AreaEffectLifetime());
        
        if (DamageTickInterval > 0f)
            StartCoroutine(ApplyDamageOverTime());

        else
            ApplyDamageOnce();
    }

    private IEnumerator AreaEffectLifetime()
    {
        yield return new WaitForSeconds(Duration);
        Destroy(gameObject);    // 범위 지속 시간 종료 시 제거 (풀링 고려)
    }

    private void OnTriggerEnter(Collider other)
    {
        Targetable hitTarget = other.GetComponent<Targetable>();

        // 타겟에 시전자 본인도 제외해야 함
        if (hitTarget != null && hitTarget.gameObject != Caster.gameObject)
            targetsInArea.Add(hitTarget);
    }

    private void OnTriggerExit(Collider other)
    {
        Targetable hitTarget = other.GetComponent<Targetable>();

        if (hitTarget != null)
            targetsInArea.Remove(hitTarget);
    }

    private void ApplyDamageOnce()
    {
        // Collider.OverlapSphere 또는 Physics.SphereCastAll을 사용하여 즉시 범위 내 모든 타겟에게 피해 적용
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, Radius, LayerMask.GetMask("Enemy"));
        
        foreach (Collider hitCollider in hitColliders)
        {
            Targetable target = hitCollider.GetComponent<Targetable>();

            if (target != null && target.gameObject != Caster.gameObject)
                ApplyDamageToTarget(target);
        }
    }

    private IEnumerator ApplyDamageOverTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(DamageTickInterval);

            // 범위 내 모든 타겟에게 피해 적용
            foreach (Targetable target in new List<Targetable>(targetsInArea))
            {
                if (target != null)
                    ApplyDamageToTarget(target);
            }
        }
    }

    private void ApplyDamageToTarget(Targetable target)
    {
        target.TakeDamage(Damage);

        // 피격 이펙트 출력
        if (HitEffectPrefab != null)
            Instantiate(HitEffectPrefab, target.transform.position, Quaternion.identity);

        // DamageTextManager.Instance?.ShowDamage(target.transform.position, (int)Damage);
    }
}
