using UnityEngine;

/// <summary>
/// ���� ������ ��ų�� ������ ������Ʈ
/// </summary>
[RequireComponent(typeof(Collider2D))]
public class AOE : MonoBehaviour
{
    private float damage;               // ���ط�
    private float radius;               // ������ ����
    private float duration;             // ������ ���� �ð�
    private float tickInterval;         // ���� �ֱ�
    private LayerMask targetLayer;      // Enemy Layer�� �����ϱ� ����

    private float elapsedTime = 0f;     // �ð� ��� ����
    private float tickTimer = 0f;       // ���� �ֱ� ����

    private void Update()
    {
        elapsedTime += Time.deltaTime;
        tickTimer += Time.deltaTime;

        // �ð��� ���� �ֱ⸸ŭ ��� �ȴٸ�
        if (tickTimer >= tickInterval)
        {
            // Ÿ�̸Ӹ� �ʱ�ȭ �ϰ� ���ظ� ������.
            tickTimer = 0f;
            ApplyDamage();
        }

        // �ð��� ������ ���� �ð����� ���� �帥�ٸ�
        if (elapsedTime >= duration)
        {
            // �ش� ������ ��� �ִ� ��ų ������Ʈ�� ���� �Ѵ�.
            Destroy(gameObject);
        }
    }

    // �ʱ�ȭ
    public void Initialize(float damage, float radius, float duration, float tickInterval, LayerMask targetLayer)
    {
        this.damage = damage;
        this.radius = radius;
        this.duration = duration;
        this.tickInterval = tickInterval;
        this.targetLayer = targetLayer;
    }

    // ���� ������ ����
    private void ApplyDamage()
    {
        // ��ų ������Ʈ�� ���� ���� �����ϴ� ��� �ݶ��̴��� Ž���Ѵ�.
        var hits = Physics2D.OverlapCircleAll(transform.position, radius, targetLayer);
        foreach (var hit in hits)
        {
            // ���ظ� ���� �� �ִ� ������Ʈ(ex. �÷��̾� ������ ����)��� ���ظ� ������.
            if (hit.TryGetComponent<IDamageable>(out var target))
            {
                target.TakeDamage((int)damage);
            }
        }
    }

    // ������ �󿡼� ������ �ð�ȭ �ϴ� �뵵 (��� ��)
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
