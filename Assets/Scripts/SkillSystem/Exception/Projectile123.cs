using UnityEngine;

public class Projectile123 : MonoBehaviour
{
    public float Speed { get; set; }
    public float Damage { get; set; }
    public float Lifetime { get; set; }     // 이 시간 내에 적중하지 않는다면 제거
    public GameObject HitEffect { get; set; }
    public SkillCaster Caster { get; set; }
    public bool PierceTargets { get; set; }

    private Vector2 _direction;
    private Targetable123 _target;     // 유도탄 또는 타겟 추적 시 사용 가능
    private Vector3 _initialPosition;

    public void Initialize(SkillCaster caster, float speed, float damage, float lifetime, GameObject hitEffect, Vector3 direction, Targetable123 target = null, bool pierce = false)
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

        Destroy(gameObject, Lifetime);  // 지정한 시간 후 자동으로 제거
    }

    // Update is called once per frame
    private void Update()
    {
        transform.position += (Vector3)_direction * Speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 유효 타겟인지 확인하는 로직 필요하면 추가할 예정
        Targetable123 hitTarget = other.GetComponent<Targetable123>();

        // 플레이어 본인이 맞을 경우도 혹시 모르니 제외
        if (hitTarget != null && hitTarget.gameObject != Caster.gameObject)
        {
            // 충돌 판정: 발사체가 타겟에게 적중 시, 해당 타겟에게 스킬 레벨에 따른 데미지가 부여된다.
            // 타겟 체력 감소 : 발사체를 적중당한 타겟의 체력을 발사체의 피해량만큼 감소시킨다.
            hitTarget.TakeDamage(Damage);

            // 데미지 텍스트 출력

            if (!PierceTargets)
                Destroy(gameObject);    // 관통형이 아닐 경우, 타겟에 적중했으면 제거
        }
    }
}
