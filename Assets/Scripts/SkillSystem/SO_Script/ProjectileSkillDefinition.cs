using System.Collections.Generic;
using UnityEngine;

// 투사체 스킬 데이터 정의 + ExecuteSkill 오버라이드하여 투사체 생성 로직 포함

// Assets/Create/Skills/Projectile Skill Definition
[CreateAssetMenu(fileName = "NewProjectileSkill", menuName = "Skills/Projectile Skill Definition", order = 1)]
public class ProjectileSkillDefinition : SkillDefinition
{
    [Header("Projectile Specifics")]
    public GameObject ProjectilePrefab;     // 투사체 프리팹
    public float ProjectileSpeed = 10f;     // 투사체 속도(임시 설정)
    public float ProjectileLifetime = 2f;   // 미적중 발사체 유지 시간
    public bool PierceTargets = false;      // 타겟 관통 여부
    public float ProjectileRange = 15f;     // 투사체 사거리(임시 설정)

    public override bool CanCastSkill(CharacterStats casterStats, Dictionary<SkillDefinition, float> skillCooldowns)
    {
        if (!base.CanCastSkill(casterStats, skillCooldowns))
            return false;

        // 투사체 스킬은 타겟이 필요할 수 있으므로, 여기서 필요 시 타겟 유효성 추가 검사
        // 일단 임시로 SkillCaster에서 타겟 유무 체크
        return true;
    }

    // 투사체 스킬의 실행 로직
    public override void ExecuteSkill(SkillCaster caster, Targetable123 target, Vector3? castPosition = null)
    {
        if (ProjectilePrefab == null)
        {
            Debug.LogError($"투사체 프리팹이 {SkillName}에 세팅 되어 있지 않습니다!");
            return;
        }

        Vector3 spawnPosition = caster.transform.position;  // 투사체의 발사 시작 좌표
        Vector2 direction;      // 투사체의 방향

        // 타겟이 소멸했거나 타겟이 지정되지 않은 경우
        if (target == null)
        {
            // 예외 처리 : 타겟 소멸 시, 시전자의 현재 정면 방향으로 발사
            direction = new Vector2(caster.transform.forward.x, caster.transform.forward.y).normalized;
        }

        else
        {
            // 위치 및 방향 계산
            direction = (new Vector2(target.transform.position.x, target.transform.position.y) - new Vector2(spawnPosition.x, spawnPosition.y)).normalized;
        }

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(0, 0, angle); // Rotate around Z-axis

        // 발사체 생성 및 초기화 (풀링 적용 예정)
        GameObject projectileGO = Instantiate(ProjectilePrefab, spawnPosition, Quaternion.LookRotation(direction));
        Projectile projectile = projectileGO.GetComponent<Projectile>();

        if (projectile != null)
        {
            // 스킬 레벨에 따른 피해량 계산
            int currentSkillLevel = caster.GetSkillLevel(this);     // SkillCaster에서 스킬 레벨 받아옴
            float calculatedDamage = GetCalculatedDamage(currentSkillLevel);

            projectile.Initialize(
                caster: caster,
                speed: ProjectileSpeed,
                damage: calculatedDamage,
                lifetime: ProjectileLifetime,
                hitEffect: HitEffect,
                direction: direction,
                target: target, // 타겟 정보를 넘겨주어 유도탄 등에 활용 가능
                pierce: PierceTargets
            );
        }

        else
            Debug.LogError($"투사체 프리팹 {ProjectilePrefab.name}이 투사체 컴포넌트가 없습니다!");
    }
}
