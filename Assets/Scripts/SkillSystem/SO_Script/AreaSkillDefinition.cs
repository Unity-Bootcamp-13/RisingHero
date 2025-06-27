using UnityEngine;
using System.Collections.Generic;

// Assets/Create/Skills/Area Skill Definition
[CreateAssetMenu(fileName = "NewAreaSkill", menuName = "Skills/Area Skill Definition", order = 2)]
public class AreaSkillDefinition : SkillDefinition
{
    [Header("Area Specifics")]
    public GameObject AreaEffectPrefab;     // 범위 스킬 이펙트 프리팹
    public float AreaRadius = 5f;           // 범위 반경 (임시)
    public float AreaDuration = 2f;         // 범위 지속 시간 (임시)
    public float DamageTickInterval = 0.5f; // 범위 내 적에게 피해 입히는 주기 (임시)

    public override bool CanCastSkill(CharacterStats casterStats, Dictionary<SkillDefinition, float> skillCooldowns)
    {
        if (!base.CanCastSkill(casterStats, skillCooldowns))
            return false;

        return true;
    }

    public override void ExecuteSkill(SkillCaster caster, Targetable123 target, Vector3? castPosition = null)
    {
        if (AreaEffectPrefab == null)
        {
            Debug.LogError($"범위스킬 {SkillName}에 이펙트 설정이 되어 있지 않습니다!");
            return;
        }

        Vector3 spawnPosition = castPosition ?? caster.transform.position;  // 캐스팅 위치 or 시전자의 위치

        // 범위 이펙트 생성 (풀링 적용 예정)
        GameObject areaGO = Instantiate(AreaEffectPrefab, spawnPosition, Quaternion.identity);
        AreaEffect areaEffect = areaGO.GetComponent<AreaEffect>();

        if (areaEffect != null)
        {
            // 스킬 레벨에 따른 피해량 계산
            int currentSkillLevel = caster.GetSkillLevel(this); // SkillCaster에서 현재 스킬레벨 받아오기
            float calculatedDamage = GetCalculatedDamage(currentSkillLevel);

            areaEffect.Initialize(
                caster: caster,
                damage: calculatedDamage,
                radius: AreaRadius,
                duration: AreaDuration,
                damageTickInterval: DamageTickInterval,
                hitEffect: HitEffect
            );
        }

        else
            Debug.LogError($"범위 스킬 이펙트 {areaEffect.name}가 컴포넌트가 없습니다!");
    }
}
