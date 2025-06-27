using UnityEngine;

[CreateAssetMenu(menuName = "Skill/Projectile")]
public class ProjectileSkill : SkillBase
{
    public GameObject projectilePrefab; // 발사체 프리팹
    public float projectileSpeed;       // 발사체 속도
    public float projectileLifetime;    // 발사체 유지 시간 (적중 실패 시 스스로 제거하기 위함.)

    public override void Activate(GameObject caster, Vector2 direction)
    {
        var projectile = Instantiate(projectilePrefab, caster.transform.position, Quaternion.identity);
        projectile.GetComponent<Projectile>().Initialize(direction, projectileSpeed, damage, projectileLifetime);
    }
}
