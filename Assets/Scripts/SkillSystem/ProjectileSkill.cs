using UnityEngine;

[CreateAssetMenu(menuName = "Skill/Projectile")]
public class ProjectileSkill : SkillBase
{
    public GameObject projectilePrefab; // �߻�ü ������
    public float projectileSpeed;       // �߻�ü �ӵ�
    public float projectileLifetime;    // �߻�ü ���� �ð� (���� ���� �� ������ �����ϱ� ����.)

    public override void Activate(GameObject caster, Vector2 direction)
    {
        var projectile = Instantiate(projectilePrefab, caster.transform.position, Quaternion.identity);
        projectile.GetComponent<Projectile>().Initialize(direction, projectileSpeed, damage, projectileLifetime);
    }
}
