using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillCaster : MonoBehaviour
{
    private SkillEquip skillEquip;
    private SkillLevelManager skillLevelManager;

    private Dictionary<int, float> cooldownTimers = new();

    [SerializeField] private PlayerMana playerMana; // ����ȭ�� ����

    public void Initialize(SkillEquip skillEquip, SkillLevelManager skillLevelManager)
    {
        this.skillEquip = skillEquip;
        this.skillLevelManager = skillLevelManager;
    }

    public bool CanCast(SkillData skillData)
    {
        return (!cooldownTimers.TryGetValue(skillData.ID, out float remaining) || remaining <= 0f)
            && playerMana != null
            && playerMana.HasEnoughMana(skillData.ManaCost);
    }

    public void CastSkill(SkillData skillData, System.Action<float> onCooldownTriggered = null)
    {
        if (skillData == null || skillEquip == null || playerMana == null) return;

        int level = skillLevelManager.GetLevel(skillData.ID);

        if (cooldownTimers.TryGetValue(skillData.ID, out float remainingTime) && remainingTime > 0f)
        {
            return;
        }

        if (!playerMana.HasEnoughMana(skillData.ManaCost))
        {
            return;
        }

        // ���� �Ҹ�
        playerMana.ConsumeMana(skillData.ManaCost);

        // ��ų ����
        var behavior = SkillBehaviorFactory.GetSkillBehavior(skillData.Type);
        behavior.Execute(skillData, transform, level);

        // ��Ÿ�� ����
        cooldownTimers[skillData.ID] = skillData.Cooldown;
        StartCoroutine(CooldownRoutine(skillData.ID));

        // UI Ʈ����
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
