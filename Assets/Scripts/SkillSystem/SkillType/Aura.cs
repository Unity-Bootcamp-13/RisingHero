using UnityEngine;

/// <summary>
/// 플레이어에 부착되어 지속적으로 범위 피해를 입히는 오라 스킬의 컴포넌트
/// </summary>
[RequireComponent(typeof(Collider2D))]
public class Aura : MonoBehaviour
{
    private float damage;               // 피해량
    private float radius;               // 오라의 범위
    private float duration;             // 피해 범위의 지속시간 (영구 지속도 고려 해야 함)
    private float tickInterval;         // 피해의 주기
    private float cooltime;             // 스킬의 쿨타임

    private float elapsedTime = 0f;     // 피해 주기를 계산하기 위함
    private float tickTimer = 0f;       // 피해 주기를 계산하기 위함

    private Transform casterTransform;  // 플레이어의 Transform
    private LayerMask targetLayer;      // Enemy Layer만 감지하기 위함

    public void Initialize(float damage, float radius, float duration, float tickInterval, float cooldown, Transform caster, LayerMask targetLayer)
    {
        this.damage = damage;
        this.radius = radius;
        this.duration = duration;
        this.tickInterval = tickInterval;
        this.cooltime = cooldown;
        this.casterTransform = caster;
        this.targetLayer = targetLayer;

        transform.SetParent(caster);    // 오라가 플레이어를 따라다니도록 설정
        transform.localPosition = Vector3.zero;
    }

    private void Update()
    {
        // 플레이어가 사라졌다면 오라 역시 제거해야 함
        if (casterTransform == null)
        {
            Destroy(gameObject);
            return;
        }

        elapsedTime += Time.deltaTime;
        tickTimer += Time.deltaTime;

        // 피해 주기 계산
        if (tickTimer >= tickInterval)
        {
            tickTimer = 0f;
            ApplyDamage();
        }

        // 지속시간이 다 끝났다면 오라를 제거해야 함 (무한 지속일 경우 duration을 0이나 음수로 설정 예정)
        if (duration > 0f && elapsedTime >= duration)
        {
            Destroy(gameObject);
        }
    }

    private void ApplyDamage()
    {
        // radius를 반지름으로 하는 원의 영역에서 충돌 감지
        var hits = Physics2D.OverlapCircleAll(transform.position, radius, targetLayer);

        foreach (var hit in hits)
        {
            // 플레이어가 피격된다면 무시함. (피해를 입히면 안됨)
            if (hit.transform == casterTransform)
                continue;

            if (hit.TryGetComponent<IDamageable>(out var target))
            {
                target.TakeDamage((int)damage);
            }
        }
    }
}
