using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float Speed { get; set; }
    public float Damage { get; set; }
    public float Lifetime { get; set; }     // �� �ð� ���� �������� �ʴ´ٸ� ����
    public GameObject HitEffect { get; set; }
    public SkillCaster Caster { get; set; }
    public bool PierceTargets { get; set; }

    private Vector3 _direction;
    private Targetable _target;     // ����ź �Ǵ� Ÿ�� ���� �� ��� ����
    private Vector3 _initialPosition;

    public void Initialize(SkillCaster caster, float speed, float damage, float lifetime, GameObject hitEffect, Vector3 direction, Targetable target = null, bool pierce = false)
    {
        Caster = caster;
        Speed = speed;
        Damage = damage;
        Lifetime = lifetime;
        HitEffect = hitEffect;
        _direction = direction.normalized;
        _target = target;
        PierceTargets = pierce;
        _initialPosition = transform.position;

        Destroy(gameObject, Lifetime);  // ������ �ð� �� �ڵ����� ����
    }

    // Update is called once per frame
    private void Update()
    {
        transform.position += _direction * Speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        // ��ȿ Ÿ������ Ȯ���ϴ� ���� �ʿ��ϸ� �߰��� ����
        Targetable hitTarget = other.GetComponent<Targetable>();

        // �÷��̾� ������ ���� ��쵵 Ȥ�� �𸣴� ����
        if (hitTarget != null && hitTarget.gameObject != Caster.gameObject)
        {
            // �浹 ����: �߻�ü�� Ÿ�ٿ��� ���� ��, �ش� Ÿ�ٿ��� ��ų ������ ���� �������� �ο��ȴ�.
            // Ÿ�� ü�� ���� : �߻�ü�� ���ߴ��� Ÿ���� ü���� �߻�ü�� ���ط���ŭ ���ҽ�Ų��.
            hitTarget.TakeDamage(Damage);

            // ������ �ؽ�Ʈ ���

            if (!PierceTargets)
                Destroy(gameObject);    // �������� �ƴ� ���, Ÿ�ٿ� ���������� ����
        }
    }
}
