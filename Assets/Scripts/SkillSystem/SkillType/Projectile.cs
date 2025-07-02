using System;
using UnityEngine;

/// <summary>
/// Ư�� �������� �̵��ϸ�, �浹 ��� ���ظ� �ָ� ������� ����ü�� ����ϴ� ������Ʈ.
/// ������ ó���� ���� �� ������ ������� �ʰ� ����. (�ʿ� �� �߰� ����)
/// </summary>
[RequireComponent (typeof(Collider2D))]
public class Projectile : MonoBehaviour
{
    private Vector2 direction;      // ����ü�� ����
    private float speed;            // ����ü�� �ӵ�
    private float damage;           // ����ü�� ���ط�
    private float lifetime;         // ����ü�� �ִ� ���� �ð� (�������� ��� ���ŵǾ�� �ϱ� ����)
    private float cooltime;         // ��ų�� ��Ÿ��
    private LayerMask targetLayer;  // Enemy Layer�� �����ϱ� ����

    private void Update()
    {
        // �� �����Ӹ��� ������ �������� �̵�
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
    }

    // �ʱ�ȭ
    public void Initialize(Vector2 direction, float speed, float damage, float lifetime, float cooldown, LayerMask targetLayer)
    {
        this.direction = direction.normalized;
        this.speed = speed;
        this.damage = damage;
        this.lifetime = lifetime;
        this.cooltime = cooldown;
        this.targetLayer = targetLayer;
        

        // ������ ���, ���� �����ϸ� ���� ���� lifetime ���� �ڵ����� ���ŵǴ� ����
        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // other�� ������Ʈ Layer�� targetLayer�� ���ԵǾ� �ִ��� üũ
        // ���ԵǾ� ���� �ʴٸ� ������ ó���� ���� �ʰ� ��ȯ
        if (((1 << other.gameObject.layer) & targetLayer) == 0)
            return;

        // �浹�� ����� ���ظ� ���� �� �ִ� ������Ʈ(ex. �÷��̾� ����) �� ��, �������� ����.
        if (other.TryGetComponent<IDamageable>(out var target))
        {
            target.TakeDamage((int)damage);
        }

        // ����ü�� �ӹ��� �������� �ڱ� �ڽ� ����
        Destroy(gameObject);
    }
}
