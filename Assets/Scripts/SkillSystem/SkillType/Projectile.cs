using System;
using UnityEngine;

/// <summary>
/// 특정 방향으로 이동하며, 충돌 즉시 피해를 주며 사라지는 투사체를 담당하는 컴포넌트.
/// 데미지 처리는 현재 이 곳에서 담당하지 않고 있음. (필요 시 추가 예정)
/// </summary>
[RequireComponent (typeof(Collider2D))]
public class Projectile : MonoBehaviour
{
    private Vector2 direction;      // 투사체의 방향
    private float speed;            // 투사체의 속도
    private float damage;           // 투사체의 피해량
    private float lifetime;         // 투사체의 최대 유지 시간 (빗나갔을 경우 제거되어야 하기 때문)
    private float cooltime;         // 스킬의 쿨타임
    private LayerMask targetLayer;  // Enemy Layer만 감지하기 위함

    private void Update()
    {
        // 매 프레임마다 지정한 방향으로 이동
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
    }

    // 초기화
    public void Initialize(Vector2 direction, float speed, float damage, float lifetime, float cooldown, LayerMask targetLayer)
    {
        this.direction = direction.normalized;
        this.speed = speed;
        this.damage = damage;
        this.lifetime = lifetime;
        this.cooltime = cooldown;
        this.targetLayer = targetLayer;
        

        // 빗나간 경우, 연산 과부하를 막기 위해 lifetime 이후 자동으로 제거되는 로직
        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // other의 오브젝트 Layer가 targetLayer에 포함되어 있는지 체크
        // 포함되어 있지 않다면 데미지 처리를 하지 않고 반환
        if (((1 << other.gameObject.layer) & targetLayer) == 0)
            return;

        // 충돌한 대상이 피해를 입힐 수 있는 오브젝트(ex. 플레이어 제외) 일 시, 데미지를 입힘.
        if (other.TryGetComponent<IDamageable>(out var target))
        {
            target.TakeDamage((int)damage);
        }

        // 투사체의 임무를 마쳤으니 자기 자신 제거
        Destroy(gameObject);
    }
}
