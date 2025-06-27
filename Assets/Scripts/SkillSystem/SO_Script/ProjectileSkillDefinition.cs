using System.Collections.Generic;
using UnityEngine;

// ����ü ��ų ������ ���� + ExecuteSkill �������̵��Ͽ� ����ü ���� ���� ����

// Assets/Create/Skills/Projectile Skill Definition
[CreateAssetMenu(fileName = "NewProjectileSkill", menuName = "Skills/Projectile Skill Definition", order = 1)]
public class ProjectileSkillDefinition : SkillDefinition
{
    [Header("Projectile Specifics")]
    public GameObject ProjectilePrefab;     // ����ü ������
    public float ProjectileSpeed = 10f;     // ����ü �ӵ�(�ӽ� ����)
    public float ProjectileLifetime = 2f;   // ������ �߻�ü ���� �ð�
    public bool PierceTargets = false;      // Ÿ�� ���� ����
    public float ProjectileRange = 15f;     // ����ü ��Ÿ�(�ӽ� ����)

    public override bool CanCastSkill(CharacterStats casterStats, Dictionary<SkillDefinition, float> skillCooldowns)
    {
        if (!base.CanCastSkill(casterStats, skillCooldowns))
            return false;

        // ����ü ��ų�� Ÿ���� �ʿ��� �� �����Ƿ�, ���⼭ �ʿ� �� Ÿ�� ��ȿ�� �߰� �˻�
        // �ϴ� �ӽ÷� SkillCaster���� Ÿ�� ���� üũ
        return true;
    }

    // ����ü ��ų�� ���� ����
    public override void ExecuteSkill(SkillCaster caster, Targetable123 target, Vector3? castPosition = null)
    {
        if (ProjectilePrefab == null)
        {
            Debug.LogError($"����ü �������� {SkillName}�� ���� �Ǿ� ���� �ʽ��ϴ�!");
            return;
        }

        Vector3 spawnPosition = caster.transform.position;  // ����ü�� �߻� ���� ��ǥ
        Vector2 direction;      // ����ü�� ����

        // Ÿ���� �Ҹ��߰ų� Ÿ���� �������� ���� ���
        if (target == null)
        {
            // ���� ó�� : Ÿ�� �Ҹ� ��, �������� ���� ���� �������� �߻�
            direction = new Vector2(caster.transform.forward.x, caster.transform.forward.y).normalized;
        }

        else
        {
            // ��ġ �� ���� ���
            direction = (new Vector2(target.transform.position.x, target.transform.position.y) - new Vector2(spawnPosition.x, spawnPosition.y)).normalized;
        }

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(0, 0, angle); // Rotate around Z-axis

        // �߻�ü ���� �� �ʱ�ȭ (Ǯ�� ���� ����)
        GameObject projectileGO = Instantiate(ProjectilePrefab, spawnPosition, Quaternion.LookRotation(direction));
        Projectile projectile = projectileGO.GetComponent<Projectile>();

        if (projectile != null)
        {
            // ��ų ������ ���� ���ط� ���
            int currentSkillLevel = caster.GetSkillLevel(this);     // SkillCaster���� ��ų ���� �޾ƿ�
            float calculatedDamage = GetCalculatedDamage(currentSkillLevel);

            projectile.Initialize(
                caster: caster,
                speed: ProjectileSpeed,
                damage: calculatedDamage,
                lifetime: ProjectileLifetime,
                hitEffect: HitEffect,
                direction: direction,
                target: target, // Ÿ�� ������ �Ѱ��־� ����ź � Ȱ�� ����
                pierce: PierceTargets
            );
        }

        else
            Debug.LogError($"����ü ������ {ProjectilePrefab.name}�� ����ü ������Ʈ�� �����ϴ�!");
    }
}
