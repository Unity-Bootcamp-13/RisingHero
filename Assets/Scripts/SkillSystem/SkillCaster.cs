using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SkillCaster : MonoBehaviour
{
    private SkillEquip skillEquip;
    private SkillLevelManager skillLevelManager;

    // 쿨타임 관리용
    private Dictionary<int, float> cooldownTimers = new();

    public void Initialize(SkillEquip skillEquip, SkillLevelManager skillLevelManager)
    {
        this.skillEquip = skillEquip;
        this.skillLevelManager = skillLevelManager;
    }

    public bool CanCast(SkillData skillData)
    {
        return !cooldownTimers.TryGetValue(skillData.ID, out float remaining) || remaining <= 0f;
    }

    public void CastSkill(SkillData skillData, System.Action<float> onCooldownTriggered = null)
    {
        if (skillData == null || skillEquip == null) return;

        int level = skillLevelManager.GetLevel(skillData.ID);

        // 쿨타임 체크
        if (cooldownTimers.TryGetValue(skillData.ID, out float remainingTime) && remainingTime > 0f)
        {
            Debug.Log($"[SkillCaster] {skillData.Name} 스킬은 쿨타임 {remainingTime:F1}초 남음");
            return;
        }

        var behavior = SkillBehaviorFactory.GetSkillBehavior(skillData.Type);
        behavior.Execute(skillData, transform, level);

        // 쿨타임 시작
        cooldownTimers[skillData.ID] = skillData.Cooldown;
        StartCoroutine(CooldownRoutine(skillData.ID));

        // UI 쿨다운 트리거
        onCooldownTriggered?.Invoke(skillData.Cooldown);
    }

    private IEnumerator CooldownRoutine(int skillId)
    {
        while (cooldownTimers[skillId] > 0f)
        {
            cooldownTimers[skillId] -= Time.deltaTime;
            yield return null;
        }

        cooldownTimers[skillId] = 0f;
    }
}
