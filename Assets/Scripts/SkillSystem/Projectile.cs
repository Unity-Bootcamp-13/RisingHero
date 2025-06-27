using System;
using UnityEngine;

/// <summary>
/// 투사체의 이동 및 충돌 감지를 담당하는 컴포넌트.
/// 데미지 처리는 이 곳에서 담당하지 않고 있음.
/// </summary>
[RequireComponent (typeof(Collider2D))]
public class Projectile : MonoBehaviour
{
    private Vector2 direction;      // 투사체의 방향
    private float speed;            // 투사체의 속도
    private float lifetime;         // 투사체의 최대 유지 시간 (빗나갔을 경우 제거되야 하기 때문)
    private float damage;           // 투사체의 피해량

    // Update is called once per frame
    private void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
    }

    public void Initialize(Vector2 direction, float speed, float damage, float lifetime)
    {
        this.direction = direction.normalized;
        this.speed = speed;
        this.damage = damage;
        this.lifetime = lifetime;

        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<IDamageable>(out var target))
        {
            target.TakeDamage((int)damage);
        }

        Destroy(gameObject);
    }
}
