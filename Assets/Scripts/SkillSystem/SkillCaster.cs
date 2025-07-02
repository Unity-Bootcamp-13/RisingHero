using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SkillCaster : MonoBehaviour
{
    public List<SkillSlot> equippedSkills = new();

    // 스킬 실행 시 SkillData에 정의된 데이터를 불러옴
    public void CastSkill(SkillData data)
    {
        SkillSlot slot = FindSlot(data);

        // 스킬이 해당 슬롯에 장착되어 있지 않음
        if (slot == null)
        {
            Debug.LogWarning($"Skill [{data.Name}] 은 장착되어 있지 않습니다.");
            return;
        }

        // 스킬 쿨타임이 진행중임
        if (slot.IsOnCooldown)
        {
            Debug.Log($"[{data.Name}] 쿨타임 진행 중: {slot.CurrentCooldown:F1}s");
            return;
        }

        // 스킬 팩토리에서 정의된 스킬의 데이터를 호출
        var behaviour = SkillBehaviourFactory.GetSkillBehaviour(data.Type);
        // 호출된 데이터에 맞는 스킬을 실행함.
        behaviour.Execute(data, transform);

        StartCoroutine(Cor_Cooldown(slot));
    }

    private SkillSlot FindSlot(SkillData data)
    {
        for (int i = 0; i < equippedSkills.Count; i++)
        {
            if (equippedSkills[i].Skill == data)
                return equippedSkills[i];
        }

        return null;
    }

    private IEnumerator Cor_Cooldown(SkillSlot slot)
    {
        slot.CurrentCooldown = slot.Skill.CooldownTime;

        while (slot.CurrentCooldown > 0f)
        {
            slot.CurrentCooldown -= Time.deltaTime;
            yield return null;
        }

        slot.CurrentCooldown = 0f;
        // 쿨타임 종료
        Debug.Log($"[{slot.Skill.Name}] 쿨타임 종료");
    }
}