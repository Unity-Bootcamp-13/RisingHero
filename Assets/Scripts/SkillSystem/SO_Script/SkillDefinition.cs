using UnityEngine;
using System.Collections.Generic;
using System;

[CreateAssetMenu(fileName = "NewSkillDefinition", menuName = "Skills/Base Skill Definition")]
public abstract class SkillDefinition : ScriptableObject
{
    [Header("Skill ID")]
    public int SkillID;     // 스킬의 고유 ID. 에디터에서 수동 할당도 되고, 자동으로 설정도 가능.

    [Header("Basic Skill Info")]
    public string SkillName = "New Skill";
    public Sprite SkillIcon;
    [TextArea] public string Description = "Skill Description";

    [Header("Core Stats")]
    // Skill Level은 SkillCaster에서 가져올 예정이므로, 여기선 기본값만 임시로 설정.
    public float BaseDamage = 10f;      // 기본 피해량
    public float DamagePerLevel = 2f;   // 레벨 상승 시 오르는 피해량
    public float CoolDown = 5f;         // 쿨타임
    public int ManaCost = 10;           // 마나 소모량
    public float CastTime = 0f;         // 스킬 시전시간 (0이면 즉시 시전)

    [Header("Effects")]
    public GameObject HitEffect;        // 타겟에게 피격 시 발생하는 이펙트

    public float GetCalculatedDamage(int currentSkillLevel)
    {
        return BaseDamage + (currentSkillLevel - 1) * DamagePerLevel;
    }

    /// <summary>
    /// 스킬 발동 전, 스킬 사용에 필요한 조건을 검사
    /// </summary>
    public virtual bool CanCastSkill(CharacterStats casterStats, Dictionary<SkillDefinition, float> skillCooldowns)
    {
        // 플레이어의 마나가 부족한 경우
        if (casterStats.CurrentMana < ManaCost)
        {
            Debug.Log($"마나가 부족합니다. 필요 마나 : {ManaCost}, 현재 마나 : {casterStats.CurrentMana}");
            return false;
        }

        if (skillCooldowns.ContainsKey(this) && skillCooldowns[this] > 0)
        {
            Debug.Log($"{SkillName}은 재사용 대기중입니다. 남은 시간 : {skillCooldowns[this]:F2}초");
            return false;
        }

        return true;
    }

    /// <summary>
    /// 스킬의 실행 로직
    /// </summary>
    public abstract void ExecuteSkill(SkillCaster caster, Targetable123 target, Vector3? castPosition = null);

    internal void ExecuteSkill(SkillCaster123 skillCaster123, Targetable123 target, Vector3? castPosition)
    {
        throw new NotImplementedException();
    }

    internal void ExecuteSkill(SkillCaster123 skillCaster123, Targetable123 targetable123)
    {
        throw new NotImplementedException();
    }

    internal bool CanCastSkill(CharacterStats123 characterStats, Dictionary<SkillDefinition, float> skillCooldowns)
    {
        throw new NotImplementedException();
    }
}
