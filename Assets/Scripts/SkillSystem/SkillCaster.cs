using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SkillCaster : MonoBehaviour
{
    private SkillEquip skillEquip;
    private SkillLevelManager skillLevelManager;

    // ��Ÿ�� ������
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

        // ��Ÿ�� üũ
        if (cooldownTimers.TryGetValue(skillData.ID, out float remainingTime) && remainingTime > 0f)
        {
            Debug.Log($"[SkillCaster] {skillData.Name} ��ų�� ��Ÿ�� {remainingTime:F1}�� ����");
            return;
        }

        var behavior = SkillBehaviorFactory.GetSkillBehavior(skillData.Type);
        behavior.Execute(skillData, transform, level);

        // ��Ÿ�� ����
        cooldownTimers[skillData.ID] = skillData.Cooldown;
        StartCoroutine(CooldownRoutine(skillData.ID));

        // UI ��ٿ� Ʈ����
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
