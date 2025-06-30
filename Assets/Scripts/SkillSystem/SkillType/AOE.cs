using UnityEngine;

/// <summary>
/// 범위 지정형 스킬에 장착할 컴포넌트
/// </summary>
[RequireComponent(typeof(Collider2D))]
public class AOE : MonoBehaviour
{
    private float damage;               // 피해량
    private float radius;               // 지정된 범위
    private float duration;             // 범위의 지속 시간
    private float tickInterval;         // 피해 주기
    private LayerMask targetLayer;      // Enemy Layer만 감지하기 위함

    private float elapsedTime = 0f;     // 시간 경과 계산용
    private float tickTimer = 0f;       // 피해 주기 계산용

    private void Update()
    {
        elapsedTime += Time.deltaTime;
        tickTimer += Time.deltaTime;

        // 시간이 피해 주기만큼 경과 된다면
        if (tickTimer >= tickInterval)
        {
            // 타이머를 초기화 하고 피해를 입힌다.
            tickTimer = 0f;
            ApplyDamage();
        }

        // 시간이 범위의 지속 시간보다 많이 흐른다면
        if (elapsedTime >= duration)
        {
            // 해당 범위에 깔려 있는 스킬 오브젝트를 제거 한다.
            Destroy(gameObject);
        }
    }

    // 초기화
    public void Initialize(float damage, float radius, float duration, float tickInterval, LayerMask targetLayer)
    {
        this.damage = damage;
        this.radius = radius;
        this.duration = duration;
        this.tickInterval = tickInterval;
        this.targetLayer = targetLayer;
    }

    // 피해 입히는 로직
    private void ApplyDamage()
    {
        // 스킬 오브젝트의 범위 내에 존재하는 모든 콜라이더를 탐색한다.
        var hits = Physics2D.OverlapCircleAll(transform.position, radius, targetLayer);
        foreach (var hit in hits)
        {
            // 피해를 입힐 수 있는 오브젝트(ex. 플레이어 본인은 제외)라면 피해를 입힌다.
            if (hit.TryGetComponent<IDamageable>(out var target))
            {
                target.TakeDamage((int)damage);
            }
        }
    }

    // 에디터 상에서 범위를 시각화 하는 용도 (없어도 됨)
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
