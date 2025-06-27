using System;
using UnityEngine;

/// <summary>
/// ����ü�� �̵� �� �浹 ������ ����ϴ� ������Ʈ.
/// ������ ó���� �� ������ ������� �ʰ� ����.
/// </summary>
[RequireComponent (typeof(Collider2D))]
public class Projectile : MonoBehaviour
{
    private Vector2 direction;      // ����ü�� ����
    private float speed;            // ����ü�� �ӵ�
    private float lifetime;         // ����ü�� �ִ� ���� �ð� (�������� ��� ���ŵǾ� �ϱ� ����)
    private float damage;           // ����ü�� ���ط�

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
