using UnityEngine;

/// <summary>
/// �÷��̾ �����Ǿ� ���������� ���� ���ظ� ������ ���� ��ų�� ������Ʈ
/// </summary>
[RequireComponent(typeof(Collider2D))]
public class Aura : MonoBehaviour
{
    private float damage;               // ���ط�
    private float radius;               // ������ ����
    private float duration;             // ���� ������ ���ӽð� (���� ���ӵ� ��� �ؾ� ��)
    private float tickInterval;         // ������ �ֱ�
    private float cooltime;             // ��ų�� ��Ÿ��

    private float elapsedTime = 0f;     // ���� �ֱ⸦ ����ϱ� ����
    private float tickTimer = 0f;       // ���� �ֱ⸦ ����ϱ� ����

    private Transform casterTransform;  // �÷��̾��� Transform
    private LayerMask targetLayer;      // Enemy Layer�� �����ϱ� ����

    public void Initialize(float damage, float radius, float duration, float tickInterval, float cooldown, Transform caster, LayerMask targetLayer)
    {
        this.damage = damage;
        this.radius = radius;
        this.duration = duration;
        this.tickInterval = tickInterval;
        this.cooltime = cooldown;
        this.casterTransform = caster;
        this.targetLayer = targetLayer;

        transform.SetParent(caster);    // ���� �÷��̾ ����ٴϵ��� ����
        transform.localPosition = Vector3.zero;
    }

    private void Update()
    {
        // �÷��̾ ������ٸ� ���� ���� �����ؾ� ��
        if (casterTransform == null)
        {
            Destroy(gameObject);
            return;
        }

        elapsedTime += Time.deltaTime;
        tickTimer += Time.deltaTime;

        // ���� �ֱ� ���
        if (tickTimer >= tickInterval)
        {
            tickTimer = 0f;
            ApplyDamage();
        }

        // ���ӽð��� �� �����ٸ� ���� �����ؾ� �� (���� ������ ��� duration�� 0�̳� ������ ���� ����)
        if (duration > 0f && elapsedTime >= duration)
        {
            Destroy(gameObject);
        }
    }

    private void ApplyDamage()
    {
        // radius�� ���������� �ϴ� ���� �������� �浹 ����
        var hits = Physics2D.OverlapCircleAll(transform.position, radius, targetLayer);

        foreach (var hit in hits)
        {
            // �÷��̾ �ǰݵȴٸ� ������. (���ظ� ������ �ȵ�)
            if (hit.transform == casterTransform)
                continue;

            if (hit.TryGetComponent<IDamageable>(out var target))
            {
                target.TakeDamage((int)damage);
            }
        }
    }
}
